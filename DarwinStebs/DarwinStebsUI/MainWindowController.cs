﻿
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using DarwinStebs;

namespace DarwinStebsUI
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{
		bool cpuFinished = false;
		int stepCounter = 0;

		Memory memory;
		CentralProcessingUnit cpu;
		NSColorableTableView memoryView;

		MemoryViewItemController lastInstructionPoint;
		MemoryViewController memControl;

		#region Constructors

		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public MainWindowController () : base ("MainWindow")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			//init object
			InitDarwinStebs ();

			InitGui ();
		}

		public void InitDarwinStebs()
		{
			memory = new Memory ();

			//setup memory by hand
			byte p = 0x00;

			//set AL to 10
			memory.Write (p++, 0xD0);
			memory.Write (p++, 0x00);
			memory.Write (p++, 0xFE);

			//increment AL
			memory.Write (p++, 0xA4);
			memory.Write (p++, 0x00);

			//jump back before AL if not ZERO
			memory.Write (p++, 0xC2);
			memory.Write (p++, 0xFC);

			//compare AL true
			memory.Write (p++, 0xD0);
			memory.Write (p++, 0x00);
			memory.Write (p++, 0x11);

			//compare AL false
			memory.Write (p++, 0xDB);
			memory.Write (p++, 0x00);
			memory.Write (p++, 0x12);

			//write AL to 30 in memory
			memory.Write (p++, 0xD2);
			memory.Write (p++, 0x30);
			memory.Write (p++, 0x00);

			//end
			memory.Write (p++, 0x00);

			//init cpu
			cpu = new CentralProcessingUnit ();
			cpu.DefaultMemory = memory;

			cpuFinished = false;
			this.statusLabel.StringValue = "-";
			stepCounter = 0;
		}

		public void InitGui()
		{
			memoryView = new NSColorableTableView ();
			//tvMemoryView.PreviousKeyView.ReplaceSubviewWith (tvMemoryView, memoryView);

			memControl = new MemoryViewController ();
			tvMemoryView.PreviousKeyView.ReplaceSubviewWith (tvMemoryView, memControl.View);
			memControl.Memory = memory;
			memControl.Init ();

			//init memory view
			var indexColumn = new NSTableColumn("index");
			indexColumn.HeaderCell.StringValue = string.Empty;
			indexColumn.Editable = false;
			memoryView.AddColumn(indexColumn);

			//add columns
			for (int y = 0; y < memory.Data.GetLength (0); y++) {

				var column = new NSTableColumn(y.ToString("X"));
				column.HeaderCell.StringValue = y.ToString ("X");

				memoryView.AddColumn(column);
			}

			memoryView.DataSource = new MemoryDataSource (memory);
			memoryView.SelectionHighlightStyle = NSTableViewSelectionHighlightStyle.SourceList;
			memoryView.AllowsMultipleSelection = false;
			memoryView.SizeToFit ();

			//init register view
			RegisterDataSource registerSource = new RegisterDataSource (cpu.RegisterBank);
			tvRegisterView.DataSource = registerSource;
			tvRegisterView.SizeToFit ();

			DrawInstructionPoint ();
			memControl.UpdateData();
		}

		public void UpdateTableViews()
		{
			tvRegisterView.ReloadData ();
			memoryView.ReloadData ();
		}

		partial void btnRun_Clicked (MonoMac.AppKit.NSButton sender)
		{
			if(RunNextStep())
			{
				DrawInstructionPoint();
			}

			UpdateTableViews();
			memControl.UpdateData();

			//paint current instruction pointer (doesn't work)
			/*
			var a = memory.AddressToPoint (cpu.InstructionPointer);
			var cell = (NSTextFieldCell)memoryView.GetCell (a.X + 1, a.Y);
			cell.BackgroundColor = NSColor.Red;
			cell.DrawsBackground = true;
			*/
		}

		private void DrawInstructionPoint()
		{
			if(lastInstructionPoint != null)
			{
				//rest color
				lastInstructionPoint.BackgroundColor = null;
			}

			var a = memory.AddressToPoint (cpu.InstructionPointer);
			lastInstructionPoint = memControl.GetItem(a.X, a.Y);
			lastInstructionPoint.BackgroundColor = NSColor.Red;
		}

		partial void btnReset_Clicked (MonoMac.AppKit.NSButton sender)
		{
			InitDarwinStebs();
			UpdateTableViews();
		}

		public bool RunNextStep()
		{
			if (!cpuFinished) {
				int opcode = cpu.NextStep ();
				this.statusLabel.StringValue = "cpu step " + stepCounter;
				stepCounter++;

				if (opcode == 0x00) {
					cpuFinished = true;
					this.statusLabel.StringValue = "cpu finished!";
					return false;
				}

				return true;
			}

			return false;
		}
	}
}

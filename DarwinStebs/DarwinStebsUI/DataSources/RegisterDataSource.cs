using System;
using DarwinStebs;
using System.Collections.Generic;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace DarwinStebsUI
{
	public class RegisterDataSource : NSTableViewDataSource
	{
		public List<Register> Registers{ get; set; }

		public RegisterDataSource (List<Register> registers)
		{
			Registers = new List<Register> ();
			Registers.AddRange(registers);
		}

		public override int GetRowCount (NSTableView tableView)
		{
			return Registers.Count;
		}

		public override NSObject GetObjectValue (NSTableView tableView, NSTableColumn tableColumn, int row)
		{
			var txtCell = new NSTextFieldCell ();

			switch (tableColumn.HeaderCell.StringValue) {
			case "Name":
				txtCell.StringValue = Registers [row].Name;
				break;
			
			case "Hex":
				txtCell.StringValue = Registers [row].Value.ToString ("X2");
				break;
			case "Bin":
				txtCell.StringValue = Convert.ToString (Registers [row].Value, 2).PadLeft (8, '0');
				break;
			}

			return txtCell;
		}
	}
}


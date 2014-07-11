using System;
using MonoMac.AppKit;
using DarwinStebs;
using MonoMac.Foundation;

namespace DarwinStebsUI
{
	public class MemoryDataSource : NSTableViewDataSource
	{
		public Memory Memory{ get; set;}

		public MemoryDataSource (Memory memory)
		{
			Memory = memory;
		}

		public override int GetRowCount (NSTableView tableView)
		{
			return Memory.Data.GetLength (0);
		}
			
		public override NSObject GetObjectValue (NSTableView tableView, NSTableColumn tableColumn, int row)
		{
			var txtCell = new NSTextFieldCell ();

			if (tableColumn.HeaderCell.StringValue != string.Empty) {
				int columnIndex = int.Parse (tableColumn.HeaderCell.StringValue, System.Globalization.NumberStyles.HexNumber);
				txtCell.StringValue = Memory.Data [columnIndex, row].ToString ("X2");

				if (txtCell.StringValue == "00") {
					txtCell.TextColor = NSColor.LightGray;
				}

			} else {
				txtCell.StringValue = row.ToString ("X");
				txtCell.TextColor = NSColor.DarkGray;
			}

			return txtCell;
		}
	}
}


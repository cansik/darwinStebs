// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace DarwinStebsUI
{
	[Register ("MemoryViewItemController")]
	partial class MemoryViewItemController
	{
		[Outlet]
		MonoMac.AppKit.NSTextField valueText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (valueText != null) {
				valueText.Dispose ();
				valueText = null;
			}
		}
	}

	[Register ("MemoryViewItem")]
	partial class MemoryViewItem
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}

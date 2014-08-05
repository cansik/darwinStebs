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
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSTextField statusLabel { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField textCode { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTableView tvMemoryView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTableView tvRegisterView { get; set; }

		[Action ("btnCompileClicked:")]
		partial void btnCompileClicked (MonoMac.Foundation.NSObject sender);

		[Action ("btnReset_Clicked:")]
		partial void btnReset_Clicked (MonoMac.AppKit.NSButton sender);

		[Action ("btnRun_Clicked:")]
		partial void btnRun_Clicked (MonoMac.AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (statusLabel != null) {
				statusLabel.Dispose ();
				statusLabel = null;
			}

			if (tvMemoryView != null) {
				tvMemoryView.Dispose ();
				tvMemoryView = null;
			}

			if (tvRegisterView != null) {
				tvRegisterView.Dispose ();
				tvRegisterView = null;
			}

			if (textCode != null) {
				textCode.Dispose ();
				textCode = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}

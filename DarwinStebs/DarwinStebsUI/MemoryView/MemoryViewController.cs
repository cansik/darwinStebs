
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace DarwinStebsUI
{
	public partial class MemoryViewController : MonoMac.AppKit.NSViewController
	{
		#region Constructors

		// Called when created from unmanaged code
		public MemoryViewController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MemoryViewController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public MemoryViewController () : base ("MemoryView", NSBundle.MainBundle)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		//strongly typed view accessor
		public new MemoryView View {
			get {
				return (MemoryView)base.View;
			}
		}
	}
}


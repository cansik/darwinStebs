
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace DarwinStebsUI
{
	public partial class MemoryViewItemController : MonoMac.AppKit.NSViewController
	{
		public string StringValue
		{
			get {
				return valueText.StringValue;
			}
			set{
				valueText.StringValue = value;
			}
		}

		public NSColor ForegroundColor{
			get{
				return valueText.TextColor;
			}
			set {
				valueText.TextColor = value;
			}
		}

		public NSColor BackgroundColor{
			get{
				return valueText.BackgroundColor;
			}
			set {

				if (value != null) {
					valueText.DrawsBackground = true;
					valueText.BackgroundColor = value;
				} else {
					valueText.DrawsBackground = false;
				}
			}
		}

		#region Constructors

		// Called when created from unmanaged code
		public MemoryViewItemController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MemoryViewItemController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public MemoryViewItemController () : base ("MemoryViewItem", NSBundle.MainBundle)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		//strongly typed view accessor
		public new MemoryViewItem View {
			get {
				return (MemoryViewItem)base.View;
			}
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
		}
	}
}


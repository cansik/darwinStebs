
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using DarwinStebs;
using System.Drawing;

namespace DarwinStebsUI
{
	public partial class MemoryViewController : MonoMac.AppKit.NSViewController
	{
		public Memory Memory { get; set;}

		List<List<MemoryViewItemController>> Items{ get; set;}

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

		public void UpdateData()
		{
			for (int y = 0; y < Memory.Data.GetLength (0); y++) {
				for (int x = 0; x < Memory.Data.GetLength (1); x++) {
					GetItem (x, y).StringValue = Memory.Data [x, y].ToString ("X2");
				}
			}
		}

		public MemoryViewItemController GetItem(int x, int y)
		{
			return Items [y] [x];
		}

		public MemoryViewItemController GetItem(byte address)
		{
			var a = Memory.AddressToPoint (address);
			return GetItem (a.X, a.Y);
		}

		public void Init()
		{
			Items = new List<List<MemoryViewItemController>> ();

			for (int y = 0; y < Memory.Data.GetLength (0); y++) {
				var row = new List<MemoryViewItemController> ();
				Items.Add (row);

				for (int x = 0; x < Memory.Data.GetLength (1); x++) {
					//create item
					var i = CreateItem (x, y);
					row.Add (i);

					View.AddSubview (i.View);

					//create left header
					if (x == 0 || y == 0) {
						int ox = 0, oy = 0;

						if (y == 0) {
							oy = (int)i.View.Bounds.Height;
							ox = (int)i.View.Bounds.Width;

							if(x == y)
								View.AddSubview(CreateHeader (x, y, 0, 0).View);
						}
							
						var h = CreateHeader (x, y, ox, oy);
						View.AddSubview (h.View);
					}
				}
			}
		}

		private MemoryViewItemController CreateItem(int x, int y)
		{
			var i = new MemoryViewItemController ();
			i.View.SetFrameOrigin (new PointF (
				i.View.Bounds.Width * x + i.View.Bounds.Width,
				i.View.Bounds.Height * (Memory.Data.GetLength (0) - y)));
			i.StringValue = "00";
			return i;
		}

		private MemoryViewItemController CreateHeader(int x, int y, int offsetx, int offsety)
		{
			var i = new MemoryViewItemController ();
			i.View.SetFrameOrigin (new PointF (
				i.View.Bounds.Width * x + offsetx,
				i.View.Bounds.Height * (Memory.Data.GetLength (0) - y) + offsety));
			i.StringValue = x.ToString("X") + "" + y.ToString("X");
			i.ForegroundColor = NSColor.Blue;
			return i;
		}
	}
}


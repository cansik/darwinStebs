using System;
using System.Text;

namespace DarwinStebs
{
	public class Memory
	{
		public byte[,] Data {get; private set;}

		public Memory() : this(0x10, 0x10)
		{
		}

		public Memory (byte width, byte height)
		{
			Data = new byte[width, height];
		}

		public byte Read(byte address)
		{
			//Read first and second nibble of byte
			int y = address >> 4;
			int x = address & 0x0f;

			return Data [x, y];
		}

		public void Write(byte address, byte value)
		{
			//Read first and second nibble of byte
			int y = address >> 4;
			int x = address & 0x0f;

			Data [x, y] = value;
		}

		public override string ToString ()
		{
			var b = new StringBuilder ();
		
			for (int y = 0; y < Data.GetLength(0); y++) {
				for (int x = 0; x < Data.GetLength (1); x++) {
					b.Append (Data[x, y].ToString ("X2") + " ");
				}
				b.AppendLine ();
			}

			return b.ToString ();
		}
	}
}


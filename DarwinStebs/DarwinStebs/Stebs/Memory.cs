using System;
using System.Text;

namespace DarwinStebs
{
	public class Memory
	{
		readonly int[,] data;

		public Memory (int width, int height)
		{
			data = new int[width, height];
		}

		public int Read(int address)
		{
			//Read first and second nibble of byte
			int y = address >> 4;
			int x = address & 0x0f;

			return data [x, y];
		}

		public void Write(int address, int value)
		{
			//Read first and second nibble of byte
			int y = address >> 4;
			int x = address & 0x0f;

			data [x, y] = value;
		}

		public override string ToString ()
		{
			StringBuilder b = new StringBuilder ();
		
			for (int y = 0; y < data.GetLength(0); y++) {
				for (int x = 0; x < data.GetLength (1); x++) {
					b.Append (data[x, y].ToString ("X2") + " ");
				}
				b.AppendLine ();
			}

			return b.ToString ();
		}
	}
}


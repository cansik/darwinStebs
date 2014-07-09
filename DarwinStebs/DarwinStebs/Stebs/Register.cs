using System;

namespace DarwinStebs
{
	public class Register
	{
		public string Name{ get; set; }
		public byte Address { get; set; }
		public byte Value { get; set; }

		public Register (String name, byte address)
		{
			Name = name;
			Address = address;
		}

		public override string ToString ()
		{
			return Name + " (" + Address.ToString ("X2") + ")\t" + Value.ToString ("X2")
				+ "\t" + Convert.ToString(Value, 2).PadLeft(8, '0'); 
		}
	}
}


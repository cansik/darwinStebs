using System;

namespace DarwinStebs
{
	public class Register
	{
		public string Name{ get; set; }
		public int Address { get; set; }
		public int Value { get; set; }

		public Register (String name, int address)
		{
			Name = name;
			Address = address;
		}

		public override string ToString ()
		{
			return Name + " (" + Address.ToString ("X") + ")\t" + Value.ToString ("X"); 
		}
	}
}


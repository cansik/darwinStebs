using System;
using System.Text.RegularExpressions;

namespace DarwinStebs
{
	public class CommandParameter
	{
		public ASMParameterType type { get; set; }
		public byte value { get; set; }

		public CommandParameter (string param)
		{
			type = getParamType (param);
			value = getParamValue (param);
		}

		//TODO: make static registers for the standard registers, also affects cpu
		public static ASMParameterType getParamType(String param) 
		{
			if ( Regex.Match (param, @"^(AL|BL|CL|DL)$").Success ) {
				return ASMParameterType.Register;
			} else if ( Regex.Match (param, @"^\w{2}$").Success /* && is allowed constant */) {
				return ASMParameterType.Constant;
			} else if ( Regex.Match (param, @"^\[\w{2}\]$").Success /* && is allowed address */) {
				return ASMParameterType.Address;
			} else {
				throw new ParseException ("Param '" + param + "' does not match an address, constant or a register.");
			}
		}

		//TODO: make static registers for the standard registers, also affects cpu
		private byte getParamValue(string param) {
			var value = new byte();

			switch (type) {
			case ASMParameterType.Constant:
				value = Convert.ToByte(param.Substring(0, 2), 16);
				break;
			case ASMParameterType.Register:
				if (param.Equals ("AL")) {
					value = 0x00;
				} else if (param.Equals ("BL")) {
					value = 0x01;
				} else if (param.Equals ("CL")) {
					value = 0x02;
				} else if (param.Equals ("DL")) {
					value = 0x03;
				} else {
					throw new ParseException ("Invalid register '" + param);
				}
				break;
			case ASMParameterType.Address:
				value = Convert.ToByte(param.Substring(1, 3), 16); //TODO: handle use case [AL] --> register as address instead of constant
				break;
			}

			return value;
		}

	}
}


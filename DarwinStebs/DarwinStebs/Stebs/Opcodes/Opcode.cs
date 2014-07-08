using System;
using System.ComponentModel;

namespace DarwinStebs
{
	[Flags]
	public enum Opcode
	{
		[Description("MOV")]
		MOVRegConst = 0xD0,
		MOVRegAddr = 0xD1,
		MOVAddrReg = 0xD2,
		MOVRegAReg = 0xD3,
		MOVARegReg = 0xD4,
		MOVRegReg = 0xD5,

		[Description("HALT")]
		HALT = 0x00,

		[Description("END")]
		END = 0x00
	}
}


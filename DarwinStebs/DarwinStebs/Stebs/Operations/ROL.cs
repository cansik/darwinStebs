using System;

namespace DarwinStebs
{
	public class ROL : BaseOperation
	{
		public ROL (CentralProcessingUnit cpu) : base(cpu)
		{
		}

		public override void Execute (byte opcode, byte param1, byte param2)
		{
			int val = cpu.GetRegister (param1).Value;
			int msb = val & 0x80;
			msb = msb >> 7;
			val = val << 1;

			cpu.GetRegister (param1).Value = (byte)(val | msb);
		}
	}
}


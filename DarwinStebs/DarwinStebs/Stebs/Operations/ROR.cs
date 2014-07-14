using System;

namespace DarwinStebs
{
	public class ROR : BaseOperation
	{
		public ROR (CentralProcessingUnit cpu) : base(cpu)
		{
		}

		public override void Execute (byte opcode, byte param1, byte param2)
		{
			int val = cpu.GetRegister (param1).Value;
			int msb = val & 0x01;
			msb = msb << 7;
			val = val >> 1;

			cpu.GetRegister (param1).Value = (byte)(val | msb);
		}
	}
}


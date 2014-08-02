using System;

namespace DarwinStebs
{
	public class SHL : BaseOperation
	{
		public SHL (CentralProcessingUnit cpu) : base(cpu)
		{
		}

		public override void Execute (byte opcode, byte param1, byte param2)
		{
			cpu.GetRegister (param1).Value <<= 1;
		}
	}
}


using System;

namespace DarwinStebs
{
	public class INC : BaseOperation
	{
		public INC (CentralProcessingUnit cpu) : base(cpu)
		{
		}

		public override void Execute (byte opcode, byte param1, byte param2)
		{
			int res = ++cpu.GetRegister (param1).Value;
			cpu.StatusRegister.SetByArithmeticResult(res);
		}
	}
}


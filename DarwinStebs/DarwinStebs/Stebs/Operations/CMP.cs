using System;

namespace DarwinStebs
{
	public class CMP : BaseOperation
	{
		public CMP (CentralProcessingUnit cpu) : base(cpu)
		{
		}

		public override void Execute (byte opcode, byte param1, byte param2)
		{
			int res = 0;

			switch (opcode) {
			case 0xDA:
				res = cpu.GetRegister (param1).Value - cpu.GetRegister (param2).Value;
				break;

			case 0xDB:
				res = cpu.GetRegister (param1).Value - param2;
				break;

			case 0xDC:
				res = cpu.GetRegister (param1).Value - cpu.DefaultMemory.Read (param2);
				break;
			}

			cpu.StatusRegister.SetByArithmeticResult(res);
		}
	}
}


using System;

namespace DarwinStebs
{
	public class JMP : BaseOperation
	{
		public JMP (CentralProcessingUnit cpu) : base(cpu)
		{
		}

		public override void Execute (byte opcode, byte param1, byte param2)
		{
			cpu.InstructionPointer += param1;
		}
	}
}


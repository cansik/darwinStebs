using System;

namespace DarwinStebs
{
	public class JZ : BaseOperation
	{
		public JZ (CentralProcessingUnit cpu) : base(cpu)
		{
		}

		public override void Execute (byte opcode, byte param1, byte param2)
		{
			if(cpu.StatusRegister.ZeroFlag)
				cpu.InstructionPointer += param1;
		}
	}
}


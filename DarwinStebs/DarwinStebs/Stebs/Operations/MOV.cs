using System;

namespace DarwinStebs
{
	public class MOV : BaseOperation
	{
		public MOV (CentralProcessingUnit cpu) : base(cpu)
		{
		}

		public override void Execute (int opcode, int param1, int param2)
		{
			base.Execute (opcode, param1, param2);

			switch (opcode) {

			case 0xD0:
				//2 params D0
				cpu.GetRegister (param1).Value = param2;
				break;

			case 0xD1:
				//2 params D1
				cpu.GetRegister (param1).Value = cpu.DefaultMemory.Read (param2);
				break;

			case 0xD2:
				//2 params D2
				cpu.DefaultMemory.Write (param1, cpu.GetRegister (param2).Value);
				break;

			case 0xD3:
				//2 params D3
				cpu.GetRegister (param1).Value = cpu.DefaultMemory.Read(cpu.GetRegister (param2).Value);
				break;

			case 0xD4:
				//2 params D4
				cpu.DefaultMemory.Write(cpu.GetRegister(param1).Value, cpu.GetRegister(param2).Value);
				break;

			case 0xD5:
				//2 params D5
				cpu.GetRegister(param1).Value = cpu.GetRegister(param2).Value;
				break;

			}
		}
	}
}


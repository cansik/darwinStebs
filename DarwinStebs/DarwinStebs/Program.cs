using System;
using System.Text;

namespace DarwinStebs
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			/*StringBuilder asmSourceCode = new StringBuilder ();
			asmSourceCode.AppendLine ("MOV AL,50");
			asmSourceCode.AppendLine ("MOV AL,50");

			cpu.InstructionRegister.Add (new AssemblyCommand () {
				Opcode = Opcode.MOV,
				Address = 0x30,
				InstructionType = 0xA0
			});*/

			//setup cpu
			var mem = new Memory (0xF, 0xF);

			var cpu = new CentralProcessingUnit ();
			cpu.DefaultMemory = mem;

			//setup memory by hand
			byte p = 0x00;

			//set AL to 3B
			mem.Write (p++, 0xD0);
			mem.Write (p++, 0x00);
			mem.Write (p++, 0x3b);

			//write AL to 30 in memory
			mem.Write (p++, 0xD2);
			mem.Write (p++, 0x30);
			mem.Write (p++, 0x00);

			//end
			mem.Write (p++, 0x00);

			//run cpu
			cpu.Run ();

			//show result
			Console.WriteLine (cpu.DefaultMemory);

			foreach (Register r in cpu.RegisterBank) {
				Console.WriteLine (r);
			}
		}
	}
}

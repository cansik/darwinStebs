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

			//set AL to 10
			mem.Write (p++, 0xD0);
			mem.Write (p++, 0x00);
			mem.Write (p++, 0x10);

			//increment AL
			mem.Write (p++, 0xA4);
			mem.Write (p++, 0x00);

			//jump back before AL
			mem.Write (p++, 0xC0);
			mem.Write(p++, 0xFC);

			//write AL to 30 in memory
			mem.Write (p++, 0xD2);
			mem.Write (p++, 0x30);
			mem.Write (p++, 0x00);

			//end
			mem.Write (p++, 0x00);

			//run cpu
			do {
				PrintCPUState (cpu);
				Console.ReadKey (true);
			} while(!cpu.NextStep ().Equals (0x00));

			Console.WriteLine ();
			Console.WriteLine ("cpu finished!");
		}

		private static void PrintCPUState(CentralProcessingUnit cpu)
		{
			Console.Clear ();
			Console.WriteLine ("Memory:");

			for (int y = 0; y < cpu.DefaultMemory.Data.GetLength(0); y++) {
				for (int x = 0; x < cpu.DefaultMemory.Data.GetLength (1); x++) {
					byte value = cpu.DefaultMemory.Data[x, y];

					Console.ResetColor ();

					if (value == 0x00)
						Console.ForegroundColor = ConsoleColor.Gray;

					//calc current x,y
					int cy = cpu.InstructionPointer >> 4;
					int cx = cpu.InstructionPointer & 0x0f;

					if (cx == x && cy == y) {
						Console.BackgroundColor = ConsoleColor.Red;
						Console.ForegroundColor = ConsoleColor.White;
					}

					Console.Write(cpu.DefaultMemory.Data[x, y].ToString ("X2") + " ");
				}
				Console.WriteLine ();
			}

			Console.ResetColor ();
			Console.WriteLine ();
			Console.WriteLine ("Register:");
			foreach (Register r in cpu.RegisterBank) {
				Console.WriteLine (r);
			}
		}
	}
}

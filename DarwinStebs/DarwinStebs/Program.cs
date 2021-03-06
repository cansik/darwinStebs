﻿using System;
using System.Text;

namespace DarwinStebs
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.Title = "DarwinStebs";

			StringBuilder asmSourceCode = new StringBuilder ();

			asmSourceCode.AppendLine ("MOV AL,0F");
			asmSourceCode.AppendLine ("MOV BL,50 ;just a comment");
			asmSourceCode.AppendLine ("ADD AL,BL");

			var compilerMemory = new Memory (0xF, 0xF);
			var compiler = new StebsCompiler (compilerMemory);

			compiler.Parse (asmSourceCode.ToString());
			var newMem = compiler.memory;

			//setup cpu
			var mem = new Memory ();

			var cpu = new CentralProcessingUnit ();
			cpu.DefaultMemory = mem;

			//setup memory by hand
			byte p = 0x00;

			//set AL to 10
			mem.Write (p++, 0xD0);
			mem.Write (p++, 0x00);
			mem.Write (p++, 0xFE);

			//increment AL
			mem.Write (p++, 0xA4);
			mem.Write (p++, 0x00);

			//jump back before AL if not ZERO
			mem.Write (p++, 0xC2);
			mem.Write (p++, 0xFC);

			//compare AL true
			mem.Write (p++, 0xDB);
			mem.Write (p++, 0x00);
			mem.Write (p++, 0x11);

			//compare AL false
			mem.Write (p++, 0xDB);
			mem.Write (p++, 0x00);
			mem.Write (p++, 0x12);

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

					if (value == 0x00)
						Console.ForegroundColor = ConsoleColor.Gray;

					//calc current x,y
					int cy = cpu.InstructionPointer >> 4;
					int cx = cpu.InstructionPointer & 0x0f;

					if (cx == x && cy == y) {
						Console.BackgroundColor = ConsoleColor.Red;
						Console.ForegroundColor = ConsoleColor.White;
					}

					Console.Write(cpu.DefaultMemory.Data[x, y].ToString ("X2"));
					Console.ResetColor ();
					Console.Write (" ");
				}
				Console.WriteLine ();
			}

			Console.ResetColor ();
			Console.WriteLine ();
			Console.WriteLine ("Register:");
			foreach (Register r in cpu.RegisterBank)
				Console.WriteLine (r);

			Console.WriteLine (cpu.StatusRegister);

		}
	}
}

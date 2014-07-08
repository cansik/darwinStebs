using System;
using System.Linq;
using System.Collections.Generic;

namespace DarwinStebs
{
	public class CentralProcessingUnit
	{
		public List<Register> RegisterBank { get; set;}
		public int InstructionPointer { get; set; }

		//public List<AssemblyCommand> InstructionRegister { get; set;}
		public Memory DefaultMemory { get; set; }

		public CentralProcessingUnit ()
		{
			RegisterBank = new List<Register> ();

			//Add default registers
			RegisterBank.Add (new Register ("AL", 0x00));
			RegisterBank.Add (new Register ("BL", 0x01));
			RegisterBank.Add (new Register ("CL", 0x02));
			RegisterBank.Add (new Register ("DL", 0x03));
		}

		private Register GetRegister(int address)
		{
			return RegisterBank.Single (r => r.Address.Equals (address));
		}

		public void Run()
		{
			while (DefaultMemory.Read (InstructionPointer) != (int)Opcode.END) {
				int value = DefaultMemory.Read (InstructionPointer++);
				int p1, p2;

				switch (value) {

				case (int)Opcode.MOVRegConst:
					//2 params D0
					p1 = DefaultMemory.Read (InstructionPointer++);
					p2 = DefaultMemory.Read (InstructionPointer++);
					GetRegister (p1).Value = p2;
					break;

				case (int)Opcode.MOVRegAddr:
					//2 params D1
					p1 = DefaultMemory.Read (InstructionPointer++);
					p2 = DefaultMemory.Read (InstructionPointer++);
					GetRegister (p1).Value = DefaultMemory.Read (p2);
					break;

				case (int)Opcode.MOVAddrReg:
						//2 params D2
					p1 = DefaultMemory.Read (InstructionPointer++);
					p2 = DefaultMemory.Read (InstructionPointer++);
					DefaultMemory.Write (p1, GetRegister (p2).Value);
					break;

				case (int)Opcode.MOVRegAReg:
					//2 params D3
					p1 = DefaultMemory.Read (InstructionPointer++);
					p2 = DefaultMemory.Read (InstructionPointer++);
					GetRegister (p1).Value = DefaultMemory.Read (GetRegister (p2).Value);
					break;

				case (int)Opcode.MOVARegReg:
					//2 params D4
					p1 = DefaultMemory.Read (InstructionPointer++);
					p2 = DefaultMemory.Read (InstructionPointer++);
					DefaultMemory.Write(GetRegister(p1).Value, GetRegister(p2).Value);
					break;

				case (int)Opcode.MOVRegReg:
					//2 params D4
					p1 = DefaultMemory.Read (InstructionPointer++);
					p2 = DefaultMemory.Read (InstructionPointer++);
					GetRegister(p1).Value = GetRegister(p2).Value;
					break;
				
				}
			}
		}
	}
}


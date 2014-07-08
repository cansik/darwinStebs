using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace DarwinStebs
{
	public class CentralProcessingUnit
	{
		readonly DecoderTable decoder = new DecoderTable();

		public List<Register> RegisterBank { get; set;}
		public int InstructionPointer { get; set; }

		public Memory DefaultMemory { get; set; }

		public CentralProcessingUnit ()
		{
			RegisterBank = new List<Register> ();

			//Add default registers
			RegisterBank.Add (new Register ("AL", 0x00));
			RegisterBank.Add (new Register ("BL", 0x01));
			RegisterBank.Add (new Register ("CL", 0x02));
			RegisterBank.Add (new Register ("DL", 0x03));
			RegisterBank.Add (new Register ("SP", 0x04));
		}

		public Register GetRegister(int address)
		{
			return RegisterBank.Single (r => r.Address.Equals (address));
		}

		public void Run()
		{
			while (DefaultMemory.Read (InstructionPointer) != 0x00) {
				int value = DefaultMemory.Read (InstructionPointer++);
				var operation = decoder.GetByOpcode (value);

				//dynamic load params
				/*
				List<int> parameters = new List<int> ();
				for (int i = 0; i < operation.Parameter.Count; i++) {
					parameters.Add (DefaultMemory.Read (InstructionPointer++));
				}
				*/

				int param1 = 0, param2 = 0;

				//read params classic
				if (operation.Parameter.Count > 0) {
					param1 = DefaultMemory.Read (InstructionPointer++);

					if (operation.Parameter.Count > 1) {
						param2 = DefaultMemory.Read (InstructionPointer++);
					}
				}


				Assembly current = Assembly.GetExecutingAssembly ();
				var type = current.GetTypes ().Single (p => p.Name.Equals (operation.Name));

				//create operation and execute
				var classe = Activator.CreateInstance (type, new object[]{ this }, null);
				MethodInfo method = type.GetMethod ("Execute");
				method.Invoke (classe, new object[]{ operation.OpCode, param1, param2 });
			}
		}
	}
}


using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace DarwinStebs
{
	public class CentralProcessingUnit
	{
		readonly DecoderTable decoder = new DecoderTable();

		//Public Register
		public List<Register> RegisterBank { get; set;}

		//Internal Register
		public byte InstructionPointer { get; set; }
		public byte StackPointer { get; set; }
		public StatusRegister StatusRegister { get; set; }

		//Memory Bank
		public Memory DefaultMemory { get; set; }

		public CentralProcessingUnit ()
		{
			RegisterBank = new List<Register> ();
			StatusRegister = new StatusRegister ();

			//Add default registers
			RegisterBank.Add (new Register ("AL", 0x00));
			RegisterBank.Add (new Register ("BL", 0x01));
			RegisterBank.Add (new Register ("CL", 0x02));
			RegisterBank.Add (new Register ("DL", 0x03));
		}

		public Register GetRegister(byte address)
		{
			return RegisterBank.Single (r => r.Address.Equals (address));
		}

		public byte NextStep()
		{
			byte value = DefaultMemory.Read (InstructionPointer++);
			var operation = decoder.GetByOpcode (value);

			byte param1 = 0, param2 = 0;

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

			return operation.OpCode;
		}
	}
}


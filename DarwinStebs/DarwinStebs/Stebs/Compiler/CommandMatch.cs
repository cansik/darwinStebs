using System;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DarwinStebs
{
	public class CommandMatch
	{
		public byte opCode { get; set; }
		public string name { get; set; }
		public List<CommandParameter> parameters { get; set; } 

		public CommandMatch(byte opCode, string name)
		{
			this.opCode = opCode;
			this.name = name;
			this.parameters = new List<CommandParameter>();
		}

		public CommandMatch(byte opCode, string name, string param1) : this(opCode, name)
		{
			parameters.Add(new CommandParameter(param1));
		}

		public CommandMatch(byte opCode, string name, string param1, string param2) : this(opCode, name, param1)
		{
			parameters.Add(new CommandParameter(param2));
		}

		public void writeToMemory (Memory memory, byte instructionPointer)  
		{
			memory.Write (instructionPointer++, opCode);

			if (parameters.Count > 0)
				memory.Write (instructionPointer++, parameters[0].value);

			if (parameters.Count > 1)
				memory.Write (instructionPointer++, parameters[1].value);
		}

	}
}


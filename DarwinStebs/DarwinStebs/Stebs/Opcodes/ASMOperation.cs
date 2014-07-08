using System;
using System.Collections.Generic;

namespace DarwinStebs
{
	public class ASMOperation
	{
		public int OpCode{ get; set;}
		public string Name { get; set;}

		public List<ASMParameterType> Parameter {get; set;}

		public ASMOperation ()
		{
			Parameter = new List<ASMParameterType> ();
		}

		public ASMOperation(int opCode, string name) : this()
		{
			OpCode = opCode;
			Name = name;
		}

		public ASMOperation(int opCode, string name, ASMParameterType param1)
			: this(opCode, name)
		{
			Parameter.Add (param1);
		}

		public ASMOperation(int opCode, string name, ASMParameterType param1, ASMParameterType param2)
			: this(opCode, name, param1)
		{
			Parameter.Add (param2);
		}
	}
}


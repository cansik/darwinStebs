using System;

namespace DarwinStebs
{
	public class AssemblyCommand
	{
		public Opcode Opcode { get; set;}
		public int Address { get; set;}  
		public int InstructionType { get; set;}

		public AssemblyCommand ()
		{
		}
	}
}


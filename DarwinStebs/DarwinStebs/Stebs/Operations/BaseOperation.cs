using System;

namespace DarwinStebs
{
	public class BaseOperation
	{
		internal CentralProcessingUnit cpu;

		public BaseOperation (CentralProcessingUnit cpu)
		{
			this.cpu = cpu;
		}

		public virtual void Execute(byte opcode, byte param1, byte param2)
		{

		}
	}
}


using System;

namespace DarwinStebs
{
	public class BaseOperation
	{
		protected CentralProcessingUnit cpu;

		public BaseOperation (CentralProcessingUnit cpu)
		{
			this.cpu = cpu;
		}

		public virtual void Execute(byte opcode, byte param1, byte param2)
		{

		}
	}
}


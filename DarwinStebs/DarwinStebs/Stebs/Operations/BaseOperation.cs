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

		public virtual void Execute(int opcode, int param1, int param2)
		{

		}
	}
}


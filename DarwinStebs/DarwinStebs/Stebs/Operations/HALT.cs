using System;

namespace DarwinStebs
{
	public class HALT : BaseOperation
	{
		public HALT (CentralProcessingUnit cpu) : base(cpu)
		{
		}

		public override void Execute (byte opcode, byte param1, byte param2)
		{
			//do nothing;
		}
	}
}


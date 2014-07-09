using System;

namespace DarwinStebs
{
	public class NOP : BaseOperation
	{
		public NOP (CentralProcessingUnit cpu) : base(cpu)
		{
		}

		public override void Execute (byte opcode, byte param1, byte param2)
		{
			return;
		}
}

}
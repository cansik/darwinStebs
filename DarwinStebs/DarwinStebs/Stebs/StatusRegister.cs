using System;

namespace DarwinStebs
{
	public class StatusRegister : Register
	{
		private const int ZeroFlagPosition = 0x01;
		private const int OverflowFlagPosition = 0x02;
		private const int SignedFlagPosition = 0x03;
		private const int InterruptFlagPosition = 0x04;

		public StatusRegister () : base("SL", 0xFF)
		{
		}

		bool IsFlagSet(int flagPosition)
		{
			return (Value & (1 << flagPosition-1)) != 0;
		}

		void SetFlag(int flagPosition, bool value)
		{
			byte mask = (byte)(1 << flagPosition);

			if(value)
				Value |= mask;
			else
				Value &= (byte)~mask;
		}

		public bool ZeroFlag {
			get {
				return IsFlagSet (ZeroFlagPosition);
			}
			set {
				SetFlag (ZeroFlagPosition, value);
			}
		}

		public bool OverflowFlag {
			get {
				return IsFlagSet (OverflowFlagPosition);
			}
			set {
				SetFlag (OverflowFlagPosition, value);
			}
		}

		public bool SignedFlag {
			get {
				return IsFlagSet (SignedFlagPosition);
			}
			set {
				SetFlag (SignedFlagPosition, value);
			}
		}

		public bool InterruptFlag {
			get {
				return IsFlagSet (InterruptFlagPosition);
			}
			set {
				SetFlag (InterruptFlagPosition, value);
			}
		}
	}
}


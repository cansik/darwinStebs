using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace DarwinStebs
{
	public class StebsCompiler
	{
		protected DecoderTable decoder = new DecoderTable();
		public Memory memory { get; set; }
		char[] delimiters = new [] { ',', ' ' };

		public StebsCompiler (Memory memory)
		{
			this.memory = memory;
		}

		public Memory Parse(string sourceCode)
		{
			foreach (String line in sourceCode.Split(Environment.NewLine.ToCharArray())) {
				var commandSequence = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

				foreach (String sign in commandSequence) {
					Console.Write (sign);
				}
			}

			return memory;
		}
	}
}


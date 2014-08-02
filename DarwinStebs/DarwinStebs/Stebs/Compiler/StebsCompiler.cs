using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace DarwinStebs
{
	public class StebsCompiler
	{
		private Memory memory;
		private Tokenizer tokenizer;
		private bool success { get; set; }

		public StebsCompiler (Memory memory)
		{
			this.memory = memory;
		}

		public Memory Parse(string sourceCode)
		{
			try {
				tokenizer = new Tokenizer(sourceCode);

				tokenizer.tokenize();
				tokenizer.writeToMemory(memory);

				success = true;
			} catch(ParseException e) {
				success = false;
				Console.WriteLine ("ParseError on line " + tokenizer.codeLine + ": " + e.Message);
			} catch(CompilerException e) {
				success = false;
				Console.WriteLine ("CompilerError: " + e.Message);
			}

			return memory;
		}
	}
}


using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace DarwinStebs
{
	public class StebsCompiler
	{
		private Tokenizer tokenizer;
		public Memory memory { get; set; }
		public bool success { get; set; }
		public String statusMessage { get; set; }

		public StebsCompiler (Memory memory)
		{
			this.memory = memory;
		}

		public void Parse(string sourceCode)
		{
			try {
				tokenizer = new Tokenizer(sourceCode);

				tokenizer.tokenize();
				tokenizer.writeToMemory(memory);

				success = true;
				statusMessage = "Success";
			} catch(ParseException e) {
				success = false;
				statusMessage = "ParseError on line " + tokenizer.codeLine + ": " + e.Message;
			} catch(CompilerException e) {
				success = false;
				statusMessage = "CompilerError: " + e.Message;
			}
		}
	}
}


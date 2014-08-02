using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DarwinStebs
{
	public class Tokenizer
	{
		public int codeLine { get; set; }
		private char[] delimiters = new [] { ',', ' ' };
		private string sourceCode;
		private DecoderTable decoder = new DecoderTable();
		private List<Token> tokenTree = new List<Token> (); 

		public Tokenizer (string sourceCode)
		{
			this.sourceCode = sourceCode;
		}

		public void tokenize () 
		{
			sourceCode = stripIrrelevantCode (sourceCode);

			foreach (String line in sourceCode.Split(Environment.NewLine.ToCharArray())) {
				codeLine++;
				tokenTree.Add (new Token (line, delimiters, decoder));
			}
		}

		public void writeToMemory(Memory memory) 
		{
			foreach (var token in tokenTree) {
				token.writeTokenToMemory (memory);
			}
		}

		private string stripIrrelevantCode (string sourceCode) 
		{
			return Regex.Replace(sourceCode, @";.*(\n|$)", "\n", RegexOptions.None); 
		}
	}
}


using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DarwinStebs
{
	public class Token
	{
		private ASMOperation asmOperation;
		private char[] delimiters;
		private DecoderTable decoder;
		private int MAX_ARGUMENTS = 3;

		public Token (String lineOfCode, char[] delimiters, DecoderTable decoder)
		{
			this.delimiters 	= delimiters;
			this.decoder 		= decoder;
			this.asmOperation 	= this.createASMOperationFromString (lineOfCode);
		}

		private ASMOperation createASMOperationFromString(String lineOfCode) 
		{
			var commandSequence = getValidCommandSequence (lineOfCode);
			if ( commandSequence.Length == 0 ) return null;
			var op = new ASMOperation ();
			op.Name = commandSequence [0];

			if ( commandSequence.Length > 1 ) {
				op.Parameter = new List<ASMParameterType> ();
				op.Parameter.Add ( getParamType(commandSequence [1]) );
			}

			if ( commandSequence.Length > 2 ) 
				op.Parameter.Add ( getParamType(commandSequence [2]) );

			if(decoder.CommandMatchExists(op)) {
				var match = decoder.GetCommandMatch (op);
			} else {
				throw new ParseException ("invalid command" + lineOfCode);
			}

			return op;
		}

		private String[] getValidCommandSequence (String lineOfCode) 
		{
			var commandSequence = lineOfCode.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

			if (commandSequence.Length > MAX_ARGUMENTS)
				throw new ParseException ("To many arguments in: " + lineOfCode); 
			//if (commandSequence.Length == 0)
			//throw new ParseException ("shit"); //handle case, best thing todo is filter empty lines

			return commandSequence;
		}

		public ASMParameterType getParamType(String param) 
		{
			if ( Regex.Match (param, @"\w{2}\b").Success /* && is allowed register */) {
				return ASMParameterType.Register;
			} else if ( Regex.Match (param, @"\d{2}\b").Success /* && is allowed constant */) {
				return ASMParameterType.Constant;
			} else if ( Regex.Match (param, @"\[[\w\d]{2}\]\b").Success /* && is allowed address */) {
				return ASMParameterType.Address;
			} else {
				throw new ParseException ("Param '" + param + "' does not match an address, constant or a register.");
			}
		}

		public void writeTokenToMemory(Memory memory) 
		{
			
		}

	}
}


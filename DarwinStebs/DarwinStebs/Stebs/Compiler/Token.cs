using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DarwinStebs
{
	public class Token
	{
		private CommandMatch command;
		private char[] delimiters;
		private DecoderTable decoder;
		private const int MAX_ARGUMENTS = 3;

		public Token (String lineOfCode, char[] delimiters, DecoderTable decoder)
		{
			this.delimiters 	= delimiters;
			this.decoder 		= decoder;
			this.command 		= this.createCommandFromString (lineOfCode);
		}

		private CommandMatch createCommandFromString(String lineOfCode) 
		{
			CommandMatch commandMatch = null;
			var commandSequence = getValidCommandSequence (lineOfCode);
			if ( commandSequence.Length == 0 ) return null;

			var opTest = new ASMOperation ();
			opTest.Name = commandSequence [0];

			if ( commandSequence.Length > 1 ) opTest.Parameter.Add ( CommandParameter.getParamType(commandSequence [1]) );
			if ( commandSequence.Length > 2 ) opTest.Parameter.Add ( CommandParameter.getParamType(commandSequence [2]) );

			if( !decoder.CommandMatchExists(opTest) ) throw new ParseException ("invalid command " + lineOfCode);

			var match = decoder.GetCommandMatch (opTest);

			if ( commandSequence.Length == 1 ) {
				commandMatch = new CommandMatch (match.OpCode, match.Name);
			} else if ( commandSequence.Length == 2 ) {
				commandMatch = new CommandMatch (match.OpCode, match.Name, commandSequence [1]);
			} else if ( commandSequence.Length == 3 ) {
				commandMatch = new CommandMatch (match.OpCode, match.Name, commandSequence [1], commandSequence [2]);
			}

			return commandMatch;
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

		public void writeTokenToMemory(Memory memory, byte instructionPointer) 
		{
			if ( command is CommandMatch ) {
				command.writeToMemory (memory, instructionPointer);
			}
		}

		//TODO: make memory internal instruction pointer with auto increment ??
		public byte getInstructionLength() 
		{
			if (command == null)
				return new byte ();

			return BitConverter.GetBytes(1 + command.parameters.Count)[0];
		}

	}
}


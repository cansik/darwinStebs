using System;
using System.Linq;
using System.Collections.Generic;

namespace DarwinStebs
{
	public class DecoderTable : List<ASMOperation>
	{
		public DecoderTable () : base()
		{
			Add(new ASMOperation(0x00, "HALT"));
			Add(new ASMOperation(0x9A, "ROL", ASMParameterType.Register));
			Add(new ASMOperation(0x9B, "ROR", ASMParameterType.Register));
			Add(new ASMOperation(0x9C, "SHL", ASMParameterType.Register));
			Add(new ASMOperation(0x9D, "SHR", ASMParameterType.Register));
			Add(new ASMOperation(0xA0, "ADD", ASMParameterType.Register, ASMParameterType.Register));
			Add(new ASMOperation(0xA1, "SUB", ASMParameterType.Register, ASMParameterType.Register));
			Add(new ASMOperation(0xA2, "MUL", ASMParameterType.Register, ASMParameterType.Register));
			Add(new ASMOperation(0xA3, "DIV", ASMParameterType.Register, ASMParameterType.Register));
			Add(new ASMOperation(0xA4, "INC", ASMParameterType.Register));
			Add(new ASMOperation(0xA5, "DEC", ASMParameterType.Register));
			Add(new ASMOperation(0xA6, "MOD", ASMParameterType.Register, ASMParameterType.Register));
			Add(new ASMOperation(0xAA, "AND", ASMParameterType.Register, ASMParameterType.Register));
			Add(new ASMOperation(0xAB, "OR", ASMParameterType.Register, ASMParameterType.Register));
			Add(new ASMOperation(0xAC, "XOR", ASMParameterType.Register, ASMParameterType.Register));
			Add(new ASMOperation(0xAD, "NOT", ASMParameterType.Register));
			Add(new ASMOperation(0xB0, "ADD", ASMParameterType.Register, ASMParameterType.Constant));
			Add(new ASMOperation(0xB1, "SUB", ASMParameterType.Register, ASMParameterType.Constant));
			Add(new ASMOperation(0xB2, "MUL", ASMParameterType.Register, ASMParameterType.Constant));
			Add(new ASMOperation(0xB3, "DIV", ASMParameterType.Register, ASMParameterType.Constant));
			Add(new ASMOperation(0xB6, "MOD", ASMParameterType.Register, ASMParameterType.Constant));
			Add(new ASMOperation(0xBA, "AND", ASMParameterType.Register, ASMParameterType.Constant));
			Add(new ASMOperation(0xBB, "OR", ASMParameterType.Register, ASMParameterType.Constant));
			Add(new ASMOperation(0xBC, "XOR", ASMParameterType.Register, ASMParameterType.Constant));
			Add(new ASMOperation(0xC0, "JMP", ASMParameterType.Constant));
			Add(new ASMOperation(0xC1, "JZ", ASMParameterType.Constant));
			Add(new ASMOperation(0xC2, "JNZ", ASMParameterType.Constant));
			Add(new ASMOperation(0xC3, "JS", ASMParameterType.Constant));
			Add(new ASMOperation(0xC4, "JNS", ASMParameterType.Constant));
			Add(new ASMOperation(0xC5, "JO", ASMParameterType.Constant));
			Add(new ASMOperation(0xC6, "JNO", ASMParameterType.Constant));
			Add(new ASMOperation(0xCA, "CALL", ASMParameterType.Address));
			Add(new ASMOperation(0xCB, "RET"));
			Add(new ASMOperation(0xCC, "INT", ASMParameterType.Address));
			Add(new ASMOperation(0xCD, "IRET"));
			Add(new ASMOperation(0xD0, "MOV", ASMParameterType.Register, ASMParameterType.Constant));
			Add(new ASMOperation(0xD1, "MOV", ASMParameterType.Register, ASMParameterType.Address));
			Add(new ASMOperation(0xD2, "MOV", ASMParameterType.Address, ASMParameterType.Register));
			Add(new ASMOperation(0xD3, "MOV", ASMParameterType.Register, ASMParameterType.Address));
			Add(new ASMOperation(0xD4, "MOV", ASMParameterType.Address, ASMParameterType.Register));
			Add(new ASMOperation(0xD5, "MOV", ASMParameterType.Register, ASMParameterType.Register));
			Add(new ASMOperation(0xDA, "CMP", ASMParameterType.Register, ASMParameterType.Register));
			Add(new ASMOperation(0xDB, "CMP", ASMParameterType.Register, ASMParameterType.Constant));
			Add(new ASMOperation(0xDC, "CMP", ASMParameterType.Register, ASMParameterType.Address));
			Add(new ASMOperation(0xE0, "PUSH", ASMParameterType.Register));
			Add(new ASMOperation(0xE1, "POP", ASMParameterType.Register));
			Add(new ASMOperation(0xEA, "PUSHF"));
			Add(new ASMOperation(0xEB, "POPF"));
			Add(new ASMOperation(0xF0, "IN", ASMParameterType.Address));
			Add(new ASMOperation(0xF1, "OUT", ASMParameterType.Address));
			Add(new ASMOperation(0xFC, "STI"));
			Add(new ASMOperation(0xFD, "CLI"));
			Add(new ASMOperation(0xFF, "NOP"));
		}

		public ASMOperation GetByOpcode(int opcode)
		{
			return this.Single (o => o.OpCode.Equals (opcode));
		}
	}
}


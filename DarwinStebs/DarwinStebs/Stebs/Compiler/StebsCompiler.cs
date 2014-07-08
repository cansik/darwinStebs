using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace DarwinStebs
{
	public class StebsCompiler
	{
		//Regex: ^(?<opcode>\w+)(?:\s+(?<param1>[\w\d\[\]]+))?(?:\s*,\s*(?<param2>[\w\d\[\]]+))?$
		//Regex Tester: http://derekslager.com/blog/posts/2007/09/a-better-dotnet-regular-expression-tester.ashx
		readonly Regex sourceRegex = new Regex(@"^(?<opcode>\w+)(?:\s+(?<param1>[\w\d\[\]]+))?(?:\s*,\s*(?<param2>[\w\d\[\]]+))?$");

		public StebsCompiler ()
		{
		}

		public List<CommandMatch> Parse(string sourceCode)
		{
			var commandMatches = new List<CommandMatch> ();
			var matches = sourceRegex.Matches (sourceCode);

			foreach (Match m in matches) {
				var cm = new CommandMatch(m.Groups);

			}

			return commandMatches;
		}
	}
}


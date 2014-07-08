using System;
using System.Text.RegularExpressions;

namespace DarwinStebs
{
	public class CommandMatch
	{
		public GroupCollection Groups{ get; set; }

		public CommandMatch(GroupCollection groups)
		{
			this.Groups = groups;
		}

		public bool IsParamRegister (int param)
		{
			return (Groups [param].Success && Regex.Match (Groups [param].Value, @"\w{2}").Success);
		}

		public bool IsParamConstant (int param)
		{
			return (Groups [param].Success && Regex.Match (Groups [param].Value, @"\d{2}").Success);
		}

		public bool IsParamAddress (int param)
		{
			return (Groups [param].Success && Regex.Match (Groups [param].Value, @"\[[\w\d]{2}\]").Success);
		}
	}
}


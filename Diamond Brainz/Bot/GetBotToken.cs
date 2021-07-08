using Diamond.Brainz.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Diamond.Brainz
{
	public partial class Bot
	{
		public string GetBotToken()
		{
			string token = null;

			DataTable dt = GlobalData.DB.ExecuteSQL("SELECT Value FROM Configs WHERE Config = 'BotToken'");
			if (dt.Rows.Count > 0)
			{
				token = dt.Rows[0][0].ToString();
			}

			return token;
		}
	}
}

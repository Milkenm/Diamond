using Diamond.Brainz.Data;

namespace Diamond.Brainz
{
	public partial class Bot
	{
		public void SetBotToken(string token)
		{
			if (token != null)
			{
				if (this.GetBotToken() != null)
				{
					GlobalData.DB.ExecuteSQL($"UPDATE Configs SET Value='{token}' WHERE Config='BotToken'");
				}
				else
				{
					GlobalData.DB.ExecuteSQL($"INSERT INTO Configs (Config,Value) VALUES ('BotToken','{token}')");
				}
			}
		}
	}
}

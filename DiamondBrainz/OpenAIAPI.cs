using Diamond.API.Data;

namespace Diamond.API
{
	public class OpenAIAPI : OpenAI_API.OpenAIAPI
	{
		public OpenAIAPI(DiamondDatabase database) : base(Utils.GetSetting(database, "OpenaiApiKey")) { }
	}
}

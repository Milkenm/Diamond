using Diamond.API.Data;

using static Diamond.API.Data.DiamondContext;

namespace Diamond.API.APIs
{
	public class OpenAIAPI : OpenAI_API.OpenAIAPI
	{
		public OpenAIAPI() : base(new DiamondContext().GetSetting(ConfigSetting.OpenAI_API_Key)) { }
	}
}

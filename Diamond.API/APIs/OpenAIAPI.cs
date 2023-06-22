using Diamond.Data;
using Diamond.Data.Enums;

namespace Diamond.API.APIs
{
	public class OpenAIAPI : OpenAI_API.OpenAIAPI
	{
		public OpenAIAPI() : base(new DiamondContext().GetSetting(ConfigSetting.OpenAI_API_Key)) { }
	}
}

using Diamond.API.Data;

using static Diamond.API.Data.DiamondDatabase;

namespace Diamond.API.APIs
{
    public class OpenAIAPI : OpenAI_API.OpenAIAPI
    {
        public OpenAIAPI() : base(new DiamondDatabase().GetSetting(ConfigSetting.OpenAI_API_Key)) { }
    }
}

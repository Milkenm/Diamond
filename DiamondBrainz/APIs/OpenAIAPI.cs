using Diamond.API.Data;

using static Diamond.API.Data.DiamondDatabase;

namespace Diamond.API.APIs
{
    public class OpenAIAPI : OpenAI_API.OpenAIAPI
    {
        public OpenAIAPI(DiamondDatabase database) : base(database.GetSetting(ConfigSetting.OpenAI_API_Key)) { }
    }
}

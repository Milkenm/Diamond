using Diamond.API.Data;

namespace Diamond.API.APIs
{
    public class OpenAIAPI : OpenAI_API.OpenAIAPI
    {
        public OpenAIAPI(DiamondDatabase database) : base(database.GetSetting("OpenaiApiKey")) { }
    }
}

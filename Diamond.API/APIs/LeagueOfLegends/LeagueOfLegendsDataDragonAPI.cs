using System.Collections.Generic;

using Diamond.API.Schemes.LolDataDragon;

using ScriptsLibV2.Util;

namespace Diamond.API.APIs.LeagueOfLegends
{
    public class LeagueOfLegendsDataDragonAPI
    {
        private const string DDRAGON_HOST_URL = "https://ddragon.leagueoflegends.com";
        private const string DDRAGON_VERSION_URL = DDRAGON_HOST_URL + "/realms/euw.json";
        // {0}: DDragon version
        private const string DDRAGON_CHAMPIONS_URL = DDRAGON_HOST_URL + "/cdn/{0}/data/en_US/championFull.json";
        // {0}: Champion full image name
        private const string DDRAGON_CHAMPION_IMAGE_SPLASH_URL = DDRAGON_HOST_URL + "/cdn/img/champion/splash/{0}";
        // {0}: Champion full image name
        private const string DDRAGON_CHAMPION_IMAGE_CENTERED_URL = DDRAGON_HOST_URL + "/cdn/img/champion/centered/{0}";
        // {0}: Champion full image name
        private const string DDRAGON_CHAMPION_IMAGE_LOADING_URL = DDRAGON_HOST_URL + "/cdn/img/champion/loading/{0}";
        // {0}: DDragon version | {1}: Champion full image name
        private const string DDRAGON_CHAMPION_IMAGE_SQUARE_URL = DDRAGON_HOST_URL + "/cdn/{0}/img/champion/{1}";
        // {0}: Champion name
        private const string FANDOM_WIKI_URL = "https://leagueoflegends.fandom.com/wiki/{0}/LoL";
        // {0}: Champion name
        private const string UNIVERSE_URL = "https://universe.leagueoflegends.com/en_GB/champion/{0}/";

        public LolVersion DdragonVersion { get; private set; }
        public LolChampionData DdragonChampionData { get; private set; }

        public enum ChampionClass
        {
            Assassin,
            Fighter,
            Mage,
            Marksman,
            Support,
            Tank,
        }

        public LeagueOfLegendsDataDragonAPI()
        {
            UpdateDdragon();
        }

        private void UpdateDdragon()
        {
            UpdateDdragonVersion();
            UpdateDdragonChampionData();
        }

        public Dictionary<string, LolChampion> GetFixedChampionsMap()
        {
            Dictionary<string, LolChampion> championsMap = new Dictionary<string, LolChampion>();
            foreach (LolChampion champ in DdragonChampionData.ChampionsList.Values)
            {
                championsMap.Add(champ.ChampionName, champ);
            }
            return championsMap;
        }

        private void UpdateDdragonVersion() => DdragonVersion = RequestUtils.Get<LolVersion>(DDRAGON_VERSION_URL);

        private void UpdateDdragonChampionData() => DdragonChampionData = RequestUtils.Get<LolChampionData>(string.Format(DDRAGON_CHAMPIONS_URL, DdragonVersion.DdragonVersion));

        public string GetChampionSplashImageUrl(LolChampion champion) => string.Format(DDRAGON_CHAMPION_IMAGE_SPLASH_URL, champion.ImageInfo.FullImageName.Replace(".png", "_0.jpg"));

        public string GetChampionCenteredImageUrl(LolChampion champion) => string.Format(DDRAGON_CHAMPION_IMAGE_CENTERED_URL, champion.ImageInfo.FullImageName.Replace(".png", "_0.jpg"));

        public string GetChampionLoadingImageUrl(LolChampion champion) => string.Format(DDRAGON_CHAMPION_IMAGE_LOADING_URL, champion.ImageInfo.FullImageName.Replace(".png", "_0.jpg"));

        public string GetChampionSquareImageUrl(LolChampion champion) => string.Format(DDRAGON_CHAMPION_IMAGE_SQUARE_URL, DdragonVersion.DdragonVersion, champion.ImageInfo.FullImageName);

        public string GetChampionFandomWikiPageUrl(LolChampion champion) => string.Format(FANDOM_WIKI_URL, champion.ChampionName.Replace(" ", "_"));

        public string GetChampionUniversePage(LolChampion champ) => string.Format(UNIVERSE_URL, champ.InternalChampionName.ToLower());
    }
}
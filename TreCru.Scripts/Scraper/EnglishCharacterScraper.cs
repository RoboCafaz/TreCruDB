namespace TreCru.Scripts.Scraper
{
    public class EnglishCharacterScraper : PageScraper
    {
        public EnglishCharacterScraper()
            : base("http://onepiece-treasurecruise.com/en/c-{0}/", "characters", 0, 1000)
        {
        }
    }
}

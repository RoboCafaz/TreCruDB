using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreCru.Scripts.Scraper
{
    public class JapaneseCharacterScraper : PageScraper
    {
        public JapaneseCharacterScraper()
            : base("http://onepiece-treasurecruise.com/c-{0}/", "jp-char", 0, 1000)
        {

        }
    }
}

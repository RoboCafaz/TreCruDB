using System;
using System.IO;
using HtmlAgilityPack;

namespace TreCru.Scripts
{
    public abstract class PageScraper
    {
        protected readonly string _baseUrl;
        protected readonly string _writeDirectory;
        protected readonly int _min;
        protected readonly int _max;

        public PageScraper(string baseUrl, string writeDirectory, int min = 0, int max = 300)
        {
            _baseUrl = baseUrl;
            _writeDirectory = writeDirectory;
            _min = min;
            _max = max;
        }

        public void Scrape()
        {
            Console.WriteLine("Began Scraping " + _baseUrl + " > " + _writeDirectory);
            var path = Directory.GetCurrentDirectory() + "/" + _writeDirectory;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var web = new HtmlWeb();
            for (var i = _min; i < _max; i++)
            {
                Console.Write("#" + i + " ");
                var url = String.Format(_baseUrl, i);
                var doc = web.Load(url);
                var entry = doc.GetElementbyId("entry");
                if (entry != null && !entry.InnerText.Contains("404"))
                {
                    Console.Write(" Found");
                    System.IO.File.WriteAllText(String.Format("{0}/{1}.html", path, i), entry.InnerHtml);
                    Console.Write(" Written");
                }
                else
                {
                    Console.Write("Not Found");
                }
                Console.WriteLine();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace TreCru.Scripts.Parser
{
    public abstract class Parser<T>
        where T : class
    {
        protected readonly string _directory;

        public Parser(string directory)
        {
            _directory = directory;
        }

        public IEnumerable<T> Parse()
        {
            var list = new List<T>();
            var path = Directory.GetCurrentDirectory() + "/" + _directory;
            Console.WriteLine("Reading directory: " + path);
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                Console.WriteLine(" - Contained " + files.Count() + " Files");
                foreach (var file in files)
                {
                    Console.WriteLine(" - File " + file);
                    var html = File.ReadAllText(file);
                    if (!String.IsNullOrWhiteSpace(html))
                    {
                        Console.Write(" - Loaded File");
                        var document = new HtmlDocument();
                        document.LoadHtml(html);
                        var output = Parse(document);
                        if (output != null)
                        {
                            list.Add(output);
                            Console.WriteLine(" - Parsing Succesful");
                        }
                        else
                        {
                            Console.WriteLine(" - Parsing Failed");
                        }
                    }
                    else
                    {
                        Console.Write(" - Contained nothing...");
                    }
                }
            }
            else
            {
                Console.Write("Directory did not exist.");
            }
            Console.WriteLine();
            return list;
        }

        protected abstract T Parse(HtmlDocument document);

        public static int ParseInt(string text)
        {
            var value = 0;
            text = Regex.Replace(text, "[^0-9]", "");
            Int32.TryParse(text, out value);
            return value;
        }
    }
}

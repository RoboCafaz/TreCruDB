using System;
using System.Linq;
using TreCru.Scripts.Parser;
using TreCru.Web.Entity;

namespace TreCru.Scripts
{
    public class Runner
    {
        public static void Main()
        {
            var all = new CharacterParser().Parse();
            var groups = all.GroupBy(x => x.Id);
            var context = new TreasureCruiseEntities();
            context.Characters.AddRange(groups.Select(x=>x.First()));
            context.SaveChanges();

            var dupes = groups.Where(x => x.ToList().Count > 1);
            Console.WriteLine(all);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TreCru.Scripts.Parser;

namespace TreCru.Scripts
{
    public class Runner
    {
        public static void Main()
        {
            var header = String.Join("|", "id", "name", "desc", "type", "clazz", "rarity", "cost", "combo", "price", "level", "exp", "bHp", "bAtk", "bRcv", "mHp", "mAtk", "mRcv", "specName", "specDesc", "capName", "capDesc");
            var all = new CharacterParser().Parse();
            var output = String.Join("\n", all.ToArray());
            output = String.Join("\n", header, output);
            Console.Out.WriteLine(output);
        }
    }
}

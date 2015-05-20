using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace TreCru.Scripts.Parser
{
    public class CharacterParser : Parser<string>
    {
        private static readonly Regex REGEX_EXTRACT_ID = new Regex(@"No\.\d+");
        private static readonly Regex REGEX_EXTRACT_NUMBER = new Regex(@"\d+");

        public CharacterParser()
            : base("characters")
        {
        }

        protected override string Parse(HtmlDocument document)
        {
            int id;
            string name, imgUrl, desc;
            int type, clazz, rarity, cost, combo, price, level, exp;
            int bHp, bAtk, bRcv, mHp, mAtk, mRcv;
            string specName, specDesc;
            string capName, capDesc;
            var global = true;

            name = document.DocumentNode.Descendants("h1")
                .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Equals("monster_name"))
                .Select(x => x.InnerText).FirstOrDefault();

            var tables = document.DocumentNode.Descendants("table")
                .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Equals("detail b")).ToList();

            var descTable = tables[0];
            var idText = REGEX_EXTRACT_ID.Match(descTable.InnerText).Value;
            id = ParseInt(idText.Substring(idText.LastIndexOf(".") + 1));
            desc =
                descTable.Descendants("p").Select(x => x.InnerText).FirstOrDefault();
            imgUrl =
                descTable.Descendants("img")
                    .Where(x => x.Attributes.Contains("src"))
                    .Select(x => x.Attributes["src"].Value).FirstOrDefault();

            var infoTable = tables[1];
            var infoRow = infoTable.Descendants("tr").ToList()[1];
            var tcrVals =
                infoRow.Descendants("a")
                    .Where(x => x.Attributes.Contains("href")).Select(x => x.Attributes["href"].Value).Select(x => x.Substring(x.Length - 10)).Select(x => REGEX_EXTRACT_NUMBER.Match(x).Value).ToList();

            type = ParseInt(tcrVals[0]);
            clazz = ParseInt(tcrVals[1]);
            rarity = ParseInt(tcrVals[2]);

            var tds = infoRow.Descendants("td").Select(x => x.InnerText).ToList();
            cost = ParseInt(tds[3]);
            combo = ParseInt(tds[4]);
            price = ParseInt(tds[5]);

            var lvlTxt = tds[6];
            level = ParseInt(REGEX_EXTRACT_NUMBER.Match(lvlTxt).Value);
            exp = ParseInt(lvlTxt.Substring(lvlTxt.LastIndexOf(@"（")));

            var statsTable = tables[2];
            var rows = statsTable.Descendants("tr").ToList();
            var baseRow = rows[1].Descendants("td").Select(x => x.InnerText).ToList();
            bHp = ParseInt(baseRow[2]);
            bAtk = ParseInt(baseRow[3]);
            bRcv = ParseInt(baseRow[4]);

            var maxRow = rows[2].Descendants("td").Select(x => x.InnerText).ToList();
            mHp = ParseInt(maxRow[2]);
            mAtk = ParseInt(maxRow[3]);
            mRcv = ParseInt(maxRow[4]);

            var specialTable = tables[3];
            var specialCells = specialTable.Descendants("td").Select(x => x.InnerText).ToList();
            specName = specialCells[1];
            specDesc = specialCells[3];

            var captainTable = tables[4];
            var captainCells = captainTable.Descendants("td").Select(x => x.InnerText).ToList();
            capName = captainCells[1];
            capDesc = captainCells[3];

            var evoTable = tables[5];

            var str = String.Join("|", id, name, desc, type, clazz, rarity, cost, combo, price, level, exp, bHp, bAtk, bRcv, mHp, mAtk, mRcv, specName, specDesc, capName, capDesc);

            //int[] evo;
            //int[][] evoMats;

            //string[] tandemNames;
            //string[] tandemDescs;
            //int[][] tandemOrders;

            return str;
        }
    }
}

using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace XIVWikiParser.Parsers
{
    internal class RaidParser
    {
        private string url = "https://ffxiv.consolegameswiki.com/wiki/Raids";

        public RaidParser()
        {
        }

        public string ParseURL()
        {
            // Load wiki page
            var web = new HtmlWeb();
            var doc = web.Load(url);

            // key: search keywords
            // value: url
            Dictionary<string, string> lookuptable = new Dictionary<string, string>();

            // Get every table
            var tables = doc.DocumentNode.SelectNodes("//table/tbody");

            // Get only the last table
            var lastTable = tables.Last();

            foreach (HtmlNode row in tables.Last().SelectNodes("tr[position() > 1]"))
            {
                // Get all the data elements of this row
                var tds = row.SelectNodes("td");

                // There are spacer rows in the table we need to skip
                if (tds == null || tds.Count < 2)
                {
                    continue;
                }

                // table format is
                // (alliance/fullparty), list of raids

                // the second data element contains the raids
                // the a's contain the raid names and the hrefs
                var raids = tds[1].SelectNodes("a");


                foreach (var raid in raids)
                {
                    // the title is the mouseover text, which on the wiki is the correft full name of the raid
                    var raidName = raid.Attributes["title"].Value;

                    // href is the url of the page
                    var href = raid.Attributes["href"].Value;

                    if (!lookuptable.ContainsKey(raidName))
                    {
                        lookuptable.Add(raidName, href);
                    }
                }
            }

            return JsonConvert.SerializeObject(lookuptable);
        }
    }
}

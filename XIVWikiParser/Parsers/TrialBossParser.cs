using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Parsers.XIVWikiParser
{
    internal class TrialBossParser
    {
        private string url = "https://ffxiv.consolegameswiki.com/wiki/Trials";

        public TrialBossParser()
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

            // The last table is a summary table, iterate through all except the last table
            for (int i = 0; i < tables.Count - 1; ++i)
            {
                // for each row in the table, except the first row (which is the header)
                foreach (HtmlNode row in tables[i].SelectNodes("tr[position() > 1]"))
                {
                    // Get all the data elements of this row
                    var tds = row.SelectNodes("td");

                    // There will be 7 data elements per row
                    // Trial Name, lvl, iLvl, Party Size, Boss Name, Roulette, Tomestones, Unlock Quest

                    // We only want the boss field
                    var coreElement = tds[4];

                    // href contains the weburl of the link, it also has a "\n" at the end automatically, we need to strip this
                    var hrefAttribute = coreElement.FirstChild.Attributes["href"].Value.Replace(Environment.NewLine, "");

                    // search string is the name of the dungeon
                    var searchString = coreElement.InnerText.Replace("\n", "");


                    // key: search keywords
                    // value: url
                    if (!lookuptable.ContainsKey(searchString))
                    {
                        lookuptable.Add(searchString, hrefAttribute);
                    }
                }
            }

            return JsonConvert.SerializeObject(lookuptable);
        }
    }
}
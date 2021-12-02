using Newtonsoft.Json;
using Parsers.XIVWikiParser;
using XIVWikiParser.Parsers;

namespace XIVWikiParser
{
    internal class XIVWikiParser
    {

        public static void Main()
        {
            DungeonParser dungeonParser = new DungeonParser();
            TrialParser trialParser = new TrialParser();
            TrialBossParser trialBossParser = new TrialBossParser();
            RaidParser raidParser = new RaidParser();

            WriteJsonToFile(dungeonParser.ParseURL(), "dungeons.json");
            WriteJsonToFile(trialParser.ParseURL(), "trials.json");
            WriteJsonToFile(trialBossParser.ParseURL(), "trialbosses.json");
            WriteJsonToFile(raidParser.ParseURL(), "raids.json");
        }

        public static void WriteJsonToFile(object serializable, string filename)
        {
            string json = JsonConvert.SerializeObject(serializable);

            System.IO.File.WriteAllText(filename, json);
        }
    }
}

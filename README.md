### FFXIV Wiki Parser

Generates json files that contain dictionary information for my XIV Wiki plugin.

This tool will connect to "https://ffxiv.consolegameswiki.com/wiki/FF14_Wiki" and search the Dungeons, Trials, and Raids pages for instance names, and the associated URLs for those pages.

The datatype used is Dictionary<string,string>

Key represents the human readable name of the instance ( example. "The Fractal Continuum (Hard)" )

The Value is the url offset for that page. ( example. "/wiki/The_Fractal_Continuum_(Hard)" )

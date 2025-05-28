using System.Collections.Generic;
using System.Linq;

namespace Services.Languages
{
    public class LocalizationDataParser
    {
        public Dictionary<string, Dictionary<string, string>> Parse(string[] lines, string[] headers)
        {
            var localizations = new Dictionary<string, Dictionary<string, string>>();
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrEmpty(line)) continue;

                var localization = ParseLine(line, headers);
                localizations[localization.Key] = localization.Value;
            }

            return localizations;
        }

        private KeyValuePair<string, Dictionary<string, string>> ParseLine(string line, string[] headers)
        {
            var fields = line.Split(',');
            var key = fields[0];

            var localization = new Dictionary<string, string>();
            for (int i = 0; i < headers.Length; i++)
            {
                localization[headers[i]] = fields[i + 1];
            }
    
            return new KeyValuePair<string, Dictionary<string, string>>(key, localization);
        }
    }
}
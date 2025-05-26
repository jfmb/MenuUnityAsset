using System.Collections.Generic;

namespace Services.Languages
{
    public class Localization
    {
        private Dictionary<string, Dictionary<string, string>> _localizationData;

        public Dictionary<string, Dictionary<string, string>> LocalizationData
        {
            get => _localizationData;
            set => _localizationData = value;
        }
    }
}
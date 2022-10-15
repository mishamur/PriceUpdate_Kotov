using Interfaces;

namespace PriceUpdate.ConfigSettings
{
    public class Settings : ISettings
    {
        Dictionary<string, object> settings;
        public Settings()
        {
            settings = new Dictionary<string, object>();
        }

        public void ParseToSettigns(string[] settingsValue)
        {
            string[] settingArgs = string.Join(" ", settingsValue).Split('-');

            foreach(string setting in settingArgs)
            {
                string[] values = setting.Split(" ");
                if (values.Length == 2)
                {
                    string settingName = values[0];
                    string settingValue = values[1];

                    if (!string.IsNullOrEmpty(settingValue))
                    {
                        //проверить есть ли ключ,
                        if (!this.settings.ContainsKey(settingName))
                        {
                            this.settings.TryAdd(settingName, settingValue);
                        }
                        else
                        {
                            this.settings[settingName] = settingValue;
                        }
                    }
                }
            }
        }

        public void SetDefaultOutputDirectory(string settingValue)
        {
            string settingName = "outputDirectory";
            if (!this.settings.ContainsKey(settingName))
            {
                this.settings.TryAdd(settingName, settingValue);
            }
        }

        public object? GetValue(string key)
        {
            return this.settings.GetValueOrDefault(key);
        }
    }
}

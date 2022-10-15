using Interfaces;

namespace PriceUpdate.ConfigSettings
{
    public class SettingsLoader
    {
        private ISettings settings;
        private readonly string applicationFolderName = "PriceConfig";

        public SettingsLoader(ISettings settings)
        {
            this.settings = settings;
            SetDefaultValues();
        }

        public ISettings LoadSettings()
        {
            SearchInSystemDirectory();
            SearchingHomeDirectory();
            GetFromParams();
            return settings;
        }

        private void SetDefaultValues()
        {
            settings.SetDefaultOutputDirectory(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), applicationFolderName
                ));
        }

        private void SearchInSystemDirectory()
        {
            var pathToFolder = Path.Combine(Environment.GetFolderPath(
               Environment.SpecialFolder.CommonApplicationData), applicationFolderName);
            SearchInDirectory(pathToFolder);
        }

        private void SearchingHomeDirectory()
        {
            var pathToFolder = Path.Combine(Environment.GetFolderPath(
               Environment.SpecialFolder.ApplicationData), applicationFolderName);
            SearchInDirectory(pathToFolder);
        }

        private void SearchInDirectory(string pathToFolder)
        {
            var pathToFile = Path.Combine(pathToFolder, "priceConfig.txt");
            Directory.CreateDirectory(pathToFolder);

            //наполнить данными
            if (!File.Exists(pathToFile))
            {
                //File.Create(pathToFile);
                CreateAndFillConfigFile(pathToFile);
            }
            else
            {
                //считать, распарсить
                string[] settingValue = this.ReadSettingsFile(pathToFile);
                //распарсить
                this.settings.ParseToSettigns(settingValue);
            }
        }

        private void GetFromParams()
        {
            this.settings.ParseToSettigns(Environment.GetCommandLineArgs());
        }



        private string[] ReadSettingsFile(string pathToFile)
        {
            List<string> result = new List<string>();

            if (File.Exists(pathToFile))
            {
                try
                {
                    using(var streamReader = File.OpenText(pathToFile))
                    {
                        string valueLine;
                        while((valueLine = streamReader.ReadLine()) != null)
                        {
                            result.Add(valueLine);
                        }
                        
                    }
                }
                catch(Exception ex)
                {
                    //доббавить логику
                }
            }
            return result.ToArray();
        }

        private void CreateAndFillConfigFile(string pathToFile)
        {
            try
            {
                if (!File.Exists(pathToFile))
                {
                    using (var text = File.CreateText(pathToFile))
                    {
                        text.WriteLine("-pathToExcelFile {path}");
                    }
                }
            }
            catch(Exception ex)
            {
                //добавить логику
            }
        }
    }
}

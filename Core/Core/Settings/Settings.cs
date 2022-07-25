using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Core
{
    [DataContract]
    public class Settings
    {
        [DataMember]
        public int FieldSize { get; set; }

        [DataMember]
        public int DifficultyLevel { get; set; }

        public Settings()
        {
            var settings = GetSettings();
            if (settings != null)
            {
                FieldSize = settings.FieldSize;
                DifficultyLevel = settings.DifficultyLevel;
            }
        }

        public Settings(int fieldSize, int difficulty)
        {
            FieldSize = fieldSize;
            DifficultyLevel = difficulty;
        }

        public object? GetProperty(string propertyName)
        {
            var propValue = this.GetType().GetProperty(propertyName)?.GetValue(this);
            return propValue;
        }

        public void SetProperty(string propertyName, object value)
        {
            this.GetType().GetProperty(propertyName)?.SetValue(this, value);
            Record();
        }

        private Settings? GetSettings()
        {
            var name = @"Settings/Settings.json";
            var jsonformatter = new DataContractJsonSerializer(typeof(Settings));

            try
            {
                using (var file = new FileStream(name, FileMode.Open))
                {
                    var settings = jsonformatter.ReadObject(file) as Settings;
                    if (settings != null)
                    {
                        return settings;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void Record()
        {
            var name = @"Settings/Settings.json";
            var jsonformatter = new DataContractJsonSerializer(typeof(Settings));
            try
            {
                using (var file = new FileStream(name, FileMode.Truncate))
                {
                    jsonformatter.WriteObject(file, this);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

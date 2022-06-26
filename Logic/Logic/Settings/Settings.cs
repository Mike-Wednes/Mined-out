using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Core;

namespace Logic
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

        public Settings? GetSettings()
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
                        Console.WriteLine("ERROR Settings deserialized as null");
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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

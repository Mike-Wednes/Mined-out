using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Core;

namespace Logic
{
    [DataContract]
    public class Level
    {
        private bool isNew;

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public List<LogicCell> fieldCells { get; set; }

        [DataMember]
        public PlayerCell PlayerCell { get; set; }

        [DataMember]
        public Cell Size { get; set; }

        public Level()
            : this("new level", new List<LogicCell>(), new PlayerCell(), new Cell())
        {
            isNew = true;
        }

        public Level(string path)
        {
            var level = Get(path);
            this.name = level.name;
            this.fieldCells = level.fieldCells;
            this.PlayerCell = level.PlayerCell;
            this.Size = level.Size;
            isNew = false;
        }

        public Level(string name, List<LogicCell> fieldCells, PlayerCell playerCell, Cell size)
        {
            this.name = name;
            this.fieldCells = fieldCells;
            PlayerCell = playerCell;
            Size = size;
        }

        public Level Get(string path)
        {
            Level output;
            var jsonformatter = new DataContractJsonSerializer(this.GetType(), new Type[] {typeof(BorderCell)});
            using (var file = new FileStream(path, FileMode.OpenOrCreate))
            {
                var readed = jsonformatter.ReadObject(file) as Level;
                if (readed == null)
                {
                    throw new Exception("cant deserialize");
                }
                else
                {
                    output = readed;
                }
            }
            return output;
        }

        public void Save()
        {
            var jsonformatter = new DataContractJsonSerializer(this.GetType());
            FileStream file;
            string path = GetPath(name);
            if (isNew)
            {
                file = new FileStream(path, FileMode.Create);
            }
            else
            {
                file = new FileStream(path, FileMode.Truncate);
            }
            jsonformatter.WriteObject(file, this);
            file.Dispose();
        }

        private string GetPath(string name)
        {
            int index = 0;
            string suffix = "";
            string extension = ".json";
            while (isNew && File.Exists("Levels/" + name + suffix + extension))
            {
                index++;
                suffix = "(" + index.ToString() + ")";
                this.name = name + suffix;
            }
            return "Levels/" + name + suffix + extension;
        }
    }
}

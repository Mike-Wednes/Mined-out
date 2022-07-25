using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Core;

namespace Logic
{
    [DataContract]
    public class Level
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public List<LogicCell> Cells { get; set; }

        [DataMember]
        public PlayerCell? PlayerCell { get; set; }

        [DataMember]
        public Cell Size { get; set; }

        public Level()
            : this("new level", new List<LogicCell>(), null, new Cell())
        {
        }

        public Level(string path)
        {
            var level = Read(path);
            this.name = level.name;
            this.Cells = level.Cells;
            this.PlayerCell = level.PlayerCell;
            this.Size = level.Size;
        }

        public Level(string name, List<LogicCell> fieldCells, PlayerCell? playerCell, Cell size)
        {
            this.name = name;
            this.Cells = fieldCells;
            PlayerCell = playerCell;
            Size = size;
        }

        public Level Read(string path)
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
            SaveToDirectory();
        }

        public static string PathFromDirectory()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "Levels/";
                openFileDialog.Filter = "json files (*.json)| *.json";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }
            return "";
        }

        public void SaveToDirectory()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "Levels/";
                saveFileDialog.Filter = "json files (*.json)| *.json";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.DefaultExt = "json";
                saveFileDialog.FileName = adaptNameToPath();

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var jsonformatter = new DataContractJsonSerializer(this.GetType());
                    FileStream file = new FileStream(saveFileDialog.FileName, FileMode.Create);
                    jsonformatter.WriteObject(file, this);
                    file.Dispose();
                }
            }
        }

        private string adaptNameToPath()
        {
            int index = 0;
            string fullName = name;
            string suffix = "";
            string extension = ".json";
            while (File.Exists("Levels/" + fullName + extension))
            {
                index++;
                suffix = "(" + index.ToString() + ")";
                fullName = name + suffix;
            }
            return fullName + extension;
        }

        public Field ConvertToField()
        {
            if(PlayerCell == null)
            {
                throw new Exception("no player found");
            }
            LogicCell[,] cellArray = new LogicCell[Size.Y, Size.X];
            for (int j = 0; j < Size.Y; j++)
            {
                for(int i = 0; i < Size.X; i++)
                {
                    var finded = Get(i, j);
                    cellArray[j, i] = finded == null
                        ? new BasicSpaceCell(i, j)
                        : finded;
                }
            }
            return new Field(cellArray, PlayerCell);
        }

        public LogicCell? Get(Cell location)
        {
            return Get(location.X, location.Y);
        }

        public LogicCell? Get(int x, int y)
        {
            foreach(LogicCell cell in Cells)
            {
                if (cell.Equals(new Cell(x, y)))
                {
                    return cell;
                }
            }
            return null;
        }

        public bool TryRemove(Cell location)
        {
            var filtred = Cells.Where(g => g.Equals(new Cell(location)));
            if (filtred.Count() == 0)
            {
                return false;
            }
            else
            {
                Cells.Remove(filtred.First());
                return true;
            }
        }

        public void Add(LogicCell cell)
        {
            Cells.Add(cell);
        }
    }
}

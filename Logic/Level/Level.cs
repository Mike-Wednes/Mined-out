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

        public void Save(string name)
        {
            var jsonformatter = new DataContractJsonSerializer(this.GetType());
            FileStream file = new FileStream(name, FileMode.Create);
            jsonformatter.WriteObject(file, this);
            file.Dispose();
        }

        public string AdaptNameToPath()
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
            var cellArray = levelCellMatrix();
            addSpace(cellArray);
            return new Field(cellArray, PlayerCell);
        }

        private LogicCell[,] levelCellMatrix()
        {
            LogicCell[,] cellArray = new LogicCell[Size.Y, Size.X];
            foreach (LogicCell cell in Cells)
            {
                cellArray[cell.Y, cell.X] = cell;
            }
            return cellArray;
        }

        private void addSpace(LogicCell[,] cellArray)
        {
            for (int j = 0; j < Size.Y; j++)
            {
                for (int i = 0; i < Size.X; i++)
                {
                    if (cellArray[j, i] is null)
                    {
                        cellArray[j, i] = new BasicSpaceCell(i, j);
                    }
                }
            }
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

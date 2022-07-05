using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Minew_OUT
{
    internal class CellConverter
    {
        private readonly int cellScale;

        public CellConverter(int cellScale)
        {
            this.cellScale = cellScale;
        }

        public Bitmap Convert(LogicCell cell)
        {
            string path = "Grafics/";
            if (cell.GetType() == typeof(PlayerCell))
            {
                path += PlayerPath((PlayerCell)cell);
            }
            else if (cell.IsMarked)
            {
                path += "Mark";
            }
            else if (cell.IsVisited)
            {
                path += "Visited";
            }
            else if (cell.View == CellView.Invisible)
            {
                path += RandomSpace();
            }
            else
            {
                path += cell.GetType().Name;
            }
            return new Bitmap(path + ".bmp");
        }

        private string RandomSpace()
        {
            Random random = new Random();
            return "Space" + random.Next(1, 3).ToString();
        }

        private string PlayerPath(PlayerCell player)
        {
            return "Player/" + player.MinesAround.ToString();
        }
    }
}

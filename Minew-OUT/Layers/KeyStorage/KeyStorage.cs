using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Minew_OUT.Layers
{
    internal static class KeyStorage
    {
        internal static Dictionary<Keys, Direction> ArrowToDirection = new Dictionary<Keys, Direction>()
        {
            {Keys.Up, Direction.Up},
            {Keys.Down, Direction.Down},
            {Keys.Right, Direction.Right},
            {Keys.Left, Direction.Left},
        };

        internal static Dictionary<Keys, Direction> KeyToDirection = new Dictionary<Keys, Direction>()
        {
            {Keys.W, Direction.Up},
            {Keys.S, Direction.Down},
            {Keys.D, Direction.Right},
            {Keys.A, Direction.Left},
        };
    }
}

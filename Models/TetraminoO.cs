using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_avalonia.Models
{
    internal class TetraminoO : Tetramino
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new Position(0, 0), new Position(0, 1), new Position(1, 0),  new Position(1, 1)}
        };
        public override int ID => 4;
        public override Position[][] Tiles => tiles;
    }
}

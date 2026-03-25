using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_avalonia.Models
{
    internal class TetraminoI : Tetramino
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] {new Position(1, 0), new Position(1, 1), new Position(1, 2), new Position(1, 3)},
            new Position[] {new Position(0, 2), new Position(1, 2), new Position(2, 2), new Position(3, 2)},
            new Position[] {new Position(2, 0), new Position(2, 1), new Position(2, 2), new Position(2, 3)},
            new Position[] {new Position(0, 1), new Position(1, 1), new Position(2, 1), new Position(3, 1)}
        };

        public override int ID => 1;

        protected override Position StartOffset => new Position(-1, 3); 
        protected override Position[][] Tiles => tiles;
    }
}

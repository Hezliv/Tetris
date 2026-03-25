using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_avalonia.Models
{
    public struct Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
    public abstract class Tetramino
    {
        protected abstract Position[][] Tiles { get; }
        protected abstract Position StartOffset { get; }
        public abstract int ID { get; }

        private int rotation_state;
       
        protected Position offset;
        public Tetramino()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
        }

        public void RotateCW() => rotation_state = (rotation_state + 1) % Tiles.Length;
        
        public void RotateCCW()
        {
            if (rotation_state == 0)
            {
                rotation_state = Tiles.Length - 1;
            }
            else
                rotation_state--;
        }

        public void Move(int row, int column)
        {
            offset.Row += row;
            offset.Column += column;
        }

        public void Reset()
        {
            rotation_state = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
        
        public IEnumerable<Position> GetPositions()
        {
            foreach(Position p in Tiles[rotation_state]) 
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);  
        }
    }
}

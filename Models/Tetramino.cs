using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public abstract Position[][] Tiles { get; }
        protected Position StartOffset { get; }
        public abstract int ID { get; }

        private int _rotationState;
        private Position _offset;
        public Position Offset { private set; get; }

        public Tetramino()
        {
            _offset = new Position(StartOffset.Row, StartOffset.Column);

        }

        public void RotateCW() => _rotationState = (_rotationState + 1) % Tiles.Length;
        
        public void RotateCCW()
        {
            if (_rotationState == 0)
            {
                _rotationState = Tiles.Length - 1;
            }
            else
                _rotationState--;
        }

        public void Move(int row, int column)
        {
            _offset.Row += row;
            _offset.Column += column;
        }

        public void Reset()
        {
            _rotationState = 0;
            _offset.Row = StartOffset.Row;
            _offset.Column = StartOffset.Column;
        }

        public void ResetPosition(int row, int column)
        {
            _offset.Row = row;
            _offset.Column = column; 
        }
        
        public IEnumerable<Position> GetPositions()
        {
            foreach(Position p in Tiles[_rotationState]) 
                yield return new Position(p.Row + _offset.Row, p.Column + _offset.Column);  
        }
    }
}

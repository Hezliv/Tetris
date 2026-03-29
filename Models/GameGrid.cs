using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_avalonia.Models
{
    public class GameGrid
    {
        private readonly int[,] grid;

        public int Rows { get; }
        public int Columns { get; }

        public int this[int r, int c]
        {
            get => grid[r, c];
            set=> grid[r, c] = value;
        }

        public GameGrid(int rows, int columns)
        {
            Rows = rows; 
            Columns = columns;

            grid = new int[rows, columns];
        }

        public bool IsInside(Position position)
        {
            return position.Row >= 0 && position.Row < Rows && position.Column >= 0 && position.Column < Columns;
        }

        public bool IsEmpty(Position position) 
        {
            return grid[position.Row, position.Column] == 0;
        }

        public bool IsRowFull(int r)
        {
            if (!IsInside(new Position(r, 0))) return false;
            for(var j = 0; j < Columns; j++) 
            {
                if (grid[r, j] == 0)
                {
                    return false;                
                }
            }
            return true;
        }

        public void MoveRowDown(int r)
        {
            for(int j = 0; j < Columns; j++)
            {
                grid[r + 1, j] = grid[r, j];
            }
            ClearRow(r);
        }

        public void UpdateField(int r)
        {
            for(int i = r - 1; i >= 0; i--)
            {   
                MoveRowDown(i);
            }
        }

        public void ClearRow(int r)
        {
            for(var j = 0;j < Columns;j++) 
            {
                grid[r, j] = 0;
            }
        }

        public void Clear()
        {
            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Columns; ++j)
                {
                    grid[i, j] = 0;
                }
            }
        }
    }
}

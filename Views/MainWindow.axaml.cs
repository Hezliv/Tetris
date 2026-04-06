using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Controls.Shapes;
using Tetris_avalonia.Models;
using Tetris_avalonia.ViewModels;
using System.ComponentModel;
using Avalonia.Threading;
using System;

namespace Tetris_avalonia.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel => (MainWindowViewModel)DataContext;
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += OnGameTick;
            _timer.Start();
        }

        private void OnGameTick(object? sender, EventArgs e)
        {
            
            ClearCanvas();
            viewModel.MoveDown();
            Render();
        }

        private void Render()
        {
            DrawGrid(viewModel.Grid);

            DrawTetramino(viewModel.CurrentBlock, viewModel.CurrentBlock.Offset);
        }

        private void DrawTile(int row, int col, int idColor)
        {
            var rect = new Rectangle
            {
                Width = 38, // Чуть меньше сетки, чтобы были видны границы
                Height = 28,
                Fill = viewModel.BlocksColor[idColor],
                RadiusX = 8, // Скруглим углы для стиля
                RadiusY = 8
            };

            // Устанавливаем координаты на холсте
            Canvas.SetLeft(rect, col * 40);
            Canvas.SetTop(rect, row * 30);
            
            GameCanvas.Children.Add(rect);
        }
        // Метод, который рисует всю фигуру целиком
        // Заменили Point на Position для смещения
        private void DrawTetramino(Tetramino tetramino, Position _offset)
        {
            // Берем текущий поворот фигуры (первый массив в Tiles)
            var cells = tetramino.Tiles[0];

            foreach (var cell in cells)
            {
                // Теперь всё красиво: Row к Row, Column к Column
                DrawTile(cell.Row + _offset.Row, cell.Column + _offset.Column, tetramino.ID);
            }
        }

        private void DrawGrid(GameGrid grid)
        {
            for(int i = 0; i < grid.Rows; i++)
            {
                for(int j = 0; j < grid.Columns; j++)
                {
                    if (grid[i, j] != 0)
                        DrawTile(i, j, grid[i, j]);
                }
            }
        }
        
        private void ClearCanvas()
        {
            GameCanvas.Children.Clear();
        }
    }
}
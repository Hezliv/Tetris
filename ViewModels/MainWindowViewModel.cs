using Avalonia.Media;
using Avalonia.Remote.Protocol.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Tetris_avalonia.Models;

namespace Tetris_avalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly GameGrid _grid = new GameGrid(16, 10);
        private readonly TetraminoQueue _queue = new TetraminoQueue();

        [ObservableProperty]
        private Tetramino _currentBlock;
        [ObservableProperty]
        private int _score;

        [ObservableProperty] private bool _isLobbyVisible = true;
        [ObservableProperty] private bool _isDifficultyVisible = false;
        [ObservableProperty] private bool _isGameVisible = false;

        public GameGrid Grid => _grid;

        public IReadOnlyList<IBrush> BlocksColor => new IBrush[]
        {
            Brushes.Gray,
            Brushes.Red,
            Brushes.Orange,
            Brushes.Yellow,
            Brushes.Green,
            Brushes.Blue,
            Brushes.Violet
        };

        public MainWindowViewModel()
        {
            CurrentBlock = _queue.GetAndUpdate();
            
            Score = 0;
        }

        [RelayCommand]
        public void ShowDifficulty()
        {
            IsLobbyVisible = false;
            IsDifficultyVisible = true;
        }

        
        [RelayCommand]
        public void StartGame(string level)
        {
            // Тут можно настроить скорость падения в зависимости от 'level'
            IsDifficultyVisible = false;
            IsGameVisible = true;
            
        }

        public void AddPoints(int points)
        {
            Score += points;
        }

        public bool IsOverlapping()
        {
            foreach (var p in CurrentBlock.GetPositions())
            {
                // 1. Пропускаем те части блока, которые еще ПОВЕРХУ поля (Row < 0)
                // Чтобы не было ошибки при спавне
                if (p.Row < 0) continue;

                // 2. Если блок ушел ниже дна (Row >= 20) — это столкновение
                if (p.Row >= Grid.Rows) return true;

                // 3. Если блок ушел за стенки — это тоже столкновение
                if (p.Column < 0 || p.Column >= Grid.Columns) return true;

                // 4. ГЛАВНОЕ: Проверяем, не занята ли эта клетка в сетке Grid
                // Если там число НЕ 0, значит там уже лежит другой блок
                if (Grid[p.Row, p.Column] != 0)
                {
                    return true;
                }
            }
            return false;

        }

        public void PlaceBlock()
        {
            foreach(var i in CurrentBlock.GetPositions())
            {
                Grid[i.Row, i.Column] = CurrentBlock.ID;
            }
        }

        public void MoveDown()
        {
            if (!IsGameVisible) return; // Чтобы не падало в меню

            CurrentBlock.Move(1, 0);

            if (!BlockInside() || IsOverlapping())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
                // Спавним новый блок
                CurrentBlock = _queue.GetAndUpdate();
                CurrentBlock.Reset();

                if (IsOverlapping())
                {
                    // Если новый блок сразу на чем-то лежит — это конец игры
                    IsGameVisible = false;
                    IsLobbyVisible = true;
                }
            }

            // ВАЖНО: Добавь эту строчку в конец метода!
            // Она заставит UI обновиться.
            OnPropertyChanged(nameof(CurrentBlock));
        }

        private bool BlockInside()
        {
            foreach(Position p in CurrentBlock.GetPositions())
            {
                if (!_grid.IsInside(p))
                    return false;
            }
            return true;
        }

        
    }
}

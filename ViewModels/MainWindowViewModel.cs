using Tetris_avalonia.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using Avalonia.Remote.Protocol.Input;

namespace Tetris_avalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly GameGrid _grid = new GameGrid(20, 10);
        private readonly TetraminoQueue _queue = new TetraminoQueue();

        [ObservableProperty]
        private Tetramino _currentBlock;

        public MainWindowViewModel()
        {
            CurrentBlock = _queue.GetAndUpdate();
        }

        public void MoveDown()
        {
            CurrentBlock.Move(1, 0);

            if(!BlockInside())
            {
                CurrentBlock.Move(-1, 0);
            }
        }

        private bool BlockInside()
        {
            foreach(Position p in CurrentBlock.GetPositions())
            {
                if (!_grid.IsInside(p) || !_grid.IsEmpty(p))
                    return false;
            }
            return true;
        }
    }
}

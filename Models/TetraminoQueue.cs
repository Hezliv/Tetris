using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_avalonia.Models
{
    internal class TetraminoQueue
    {
        private readonly Tetramino[] blocks = new Tetramino[]
        {
            new TetraminoI(),
            new TetraminoJ(),
            new TetraminoL(),
            new TetraminoO(),
            new TetraminoS(),
            new TetraminoT(),
            new TetraminoZ()
        };

        private readonly Random random = new Random();

        public Tetramino NextBlock { get; private set; }

        public TetraminoQueue()
        {
            NextBlock = GetRandomBlock();
        }

        public Tetramino GetRandomBlock()
        {
            int index = random.Next(blocks.Length);
            return (Tetramino)Activator.CreateInstance(blocks[index].GetType());
        }

        public Tetramino GetAndUpdate()
        {
            Tetramino block = NextBlock;
            NextBlock = GetRandomBlock();
            return block;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Stockfish(byte?[,] board, int depth) : Engine
    {
        private byte?[,] _Board = board;
        public override byte?[,] Board 
        {
            get => _Board;
            set
            {
                _Board = value;
                GenerateMove(value);
            }
        }
        public override int Depth { get; set; } = depth;

        public override List<Move> GenerateMove(byte?[,] boars)
        {
            return [];
        }
        public override double EvaluateMove(Move move, byte?[,] boars)
        {
            return 1;
        }

    }
}

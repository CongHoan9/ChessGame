using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public abstract class Engine
    {
        public abstract byte?[,] Board { get; set; }
        public abstract int Depth { get; set; }
        public abstract List<Move> GenerateMove(byte?[,] boars);
        public abstract double EvaluateMove(Move move, byte?[,] boars);
    }
}

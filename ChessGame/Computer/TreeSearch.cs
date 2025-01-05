using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Node(Player player, Move move, byte?[,] board)
    {
        public Move Move = move;
        public byte?[,] Board = board;
        public List<Node> Children = [];
        public void GenerateMove(Engine engine)
        {
            Children = new(engine.GenerateMove(Board).Select(m => new Node(player.Opponent(), m, Board)));
        }
    }
    public class TreeSearch
    {

    }
}

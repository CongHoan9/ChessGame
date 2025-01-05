using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ChessGame
{
    public class GameState(Player player, Board board)
    {
        public Board Board { get; } = board;
        public Player CurrentPlayer { get; set; } = player;
        public void MakeMove(Move move, Square from = null)
        {
            move.Execute(Board, from);
            Board.MoveHasMake.Push((move, from));
            CurrentPlayer = CurrentPlayer.Opponent();
        }
        public void UnMakeMove(Move move, Square from = null)
        {
            move.UnExecute(Board, from);
            Board.MoveHasUnMake.Push((move, from));
            CurrentPlayer = CurrentPlayer.Opponent();
        }
        public void MakeMove()
        {
            if (Board.MoveHasUnMake.Count > 0 && Board.MoveHasUnMake.Pop() is (Move, Square) move)
            {
                Board.WhitePreMove.Clear();
                Board.BlackPreMove.Clear();
                Board.HandleMove(move.Item1, move.Item2);
            }
        }
        public void UnMakeMove()
        {
            if (Board.MoveHasMake.Count > 0 && Board.MoveHasMake.Pop() is (Move, Square) move)
            {
                Board.WhitePreMove.Clear();
                Board.BlackPreMove.Clear();
                Board.HandleUnMove(move.Item1, move.Item2);
            }
        }
    }
}

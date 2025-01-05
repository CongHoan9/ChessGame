using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
namespace ChessGame
{
    public partial class Board : UserControl, INotifyPropertyChanged
    {
        public const string Fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        private GameState _GameState = new(Player.None, null);
        public GameState GameState
        {
            get => _GameState;
            set
            {
                _GameState = value;
            }
        }
        private ObservableCollection<Overlay> _Overlays = [];
        public ObservableCollection<Overlay> Overlays
        {
            get => _Overlays;
            set
            {
                _Overlays = value;
                OnPropertyChanged(nameof(Overlays));
            }
        }
        private ObservableCollection<Hover> _Hovers = [];
        public ObservableCollection<Hover> Hovers
        {
            get => _Hovers;
            set
            {
                _Hovers = [value.Single()];
                OnPropertyChanged(nameof(Hovers));
            }
        }
        private ObservableCollection<Square> _Squares = [];
        public ObservableCollection<Square> Squares
        {
            get => _Squares;
            set
            {
                _Squares = value;
                OnPropertyChanged(nameof(Squares));
            }
        }
        private ObservableCollection<Move> _Moves = [];
        public ObservableCollection<Move> Moves
        {
            get => _Moves;
            set
            {
                _Moves = value;
                Hints.Clear();
                foreach (Move move in Moves)
                {
                    SetHintforMove(move.ToPos);
                }
                OnPropertyChanged(nameof(Moves));
            }
        }
        private ObservableCollection<Hint> _Hints = [];
        public ObservableCollection<Hint> Hints
        {
            get => _Hints;
            set
            {
                _Hints = value;
                OnPropertyChanged(nameof(Hints));
            }
        }
        private ObservableCollection<Arrow> _Arrows = [];
        public ObservableCollection<Arrow> Arrows
        {
            get => _Arrows;
            set
            {
                _Arrows = value;
                OnPropertyChanged(nameof(Arrows));
            }
        }
        private ObservableCollection<Icon> _Icons = [];
        public ObservableCollection<Icon> Icons
        {
            get => _Icons;
            set
            {
                _Icons = value;
                OnPropertyChanged(nameof(Icons));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
        public Stack<(Move, Square)> MoveHasMake = new();
        public Stack<(Move, Square)> MoveHasUnMake = new();
        public Queue<(Move, Square)> WhitePreMove = new();
        public Queue<(Move, Square)> BlackPreMove = new();
        public Board()
        {
            InitializeComponent();
            DataContext = this;
            GameState = new(Player.None, this);
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            GameState = new(Player.None, this);
            LoadBoard(Fen);
        }
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            Hints.Clear();
            Moves.Clear();
            Hovers.Clear();
            PromotionCanvas.Children.Clear();
        }
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            Arrows = new(Arrows.Where(a =>
            {
                if (a.EvaluateArrow != EvaluateArrow.None)
                {
                    a.HighLightArrow = HighLightArrow.None;
                    return true;
                }
                return false;
            }));
            Overlays = new(Overlays.Where(o =>
            {
                if (o.EvaluateOverlay != EvaluateOverlay.None || o.IsWarning)
                {
                    o.BlurOverlay = BlurOverlay.None;
                    return true;
                }
                return false;
            }));
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Point Poi = e.GetPosition(Boardd);
            Position position = ToPosition(Poi);
            if (Tag == null && GetSquare(position) is Square square)
            {
                Tag = square;
                SetPointMoveCanvas(square, Poi);
                SetHighLightOverlay(square.Position, HighLightOverlay.Move);
                Moves = new(square.Piece.GetMoves(square.Position, Encrypt.ListToArray(new(Squares)), square.Piece.Color != GameState.CurrentPlayer).Where(m => m.IsLegar(square.Piece.Color, Encrypt.ListToArray(new(Squares)))));
            }
            else if(Tag is Square movesquare)
            {
                HandleNotMove(movesquare);
            }
        }
        private void SetPointMoveCanvas(Square square, Point Poi)
        {
            square.Visibility = Visibility.Hidden;
            double size = Boardd.ActualHeight / 8;
            Image piece = MoveCanvas.Children.OfType<Image>().FirstOrDefault() ?? new()
            {
                Source = new BitmapImage(new Uri(square.ImagePath)),
                Height = size,
                Width = size,
                Cursor = new(new MemoryStream(Resource.Move)),
            };
            if (!MoveCanvas.Children.Contains(piece))
            {
                MoveCanvas.Children.Add(piece);
            }
            piece.CaptureMouse();
            Canvas.SetLeft(piece, Math.Max(0, Math.Min(Poi.X, Boardd.ActualWidth)) - size / 2);
            Canvas.SetTop(piece, Math.Max(0, Math.Min(Poi.Y, Boardd.ActualHeight)) - size / 2);
        }
        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            Hints.Clear();
            Hovers.Clear();
            PromotionCanvas.Children.Clear();
        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (Tag is Square fromsquare)
            {
                Position topos = ToPosition(e.GetPosition(Boardd));
                if (GetMove(fromsquare.Position, topos) is Move move && fromsquare.Piece.Color == GameState.CurrentPlayer)
                {
                    if (move is PawnPromotion promotion)
                    {
                        HandlePromotionMove(promotion, fromsquare);
                    }
                    else
                    {
                        MoveHasUnMake.Clear();
                        HandleMove(move, fromsquare);
                    }
                }
                else
                {
                    HandleNotMove(fromsquare);
                    if (!fromsquare.Position.Equals(topos) && Move.IsInCheck(fromsquare.Piece.Color, Encrypt.ListToArray(new(Squares))))
                    {
                        WarningCheck(fromsquare.Piece.Color);
                    }
                }
            }
        }
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            if (Tag is Square fromsquare)
            {
                HandleNotMove(fromsquare);
            }
            Tag ??= ToPosition(e.GetPosition(Boardd));
        }
        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonUp(e);
            if (Tag is Position frompos)
            {
                Position topos = ToPosition(e.GetPosition(MoveCanvas));
                if (!Equals(frompos, topos))
                {
                    SetHighLightArrow(frompos, topos);
                }
                else
                {
                    SetBlurOverlay(frompos);
                }
                Tag = null;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if ((e.LeftButton == MouseButtonState.Pressed) && Tag is Square square)
            {
                Point Poi = e.GetPosition(Boardd);
                Hovers = [new(ToPosition(Poi))];
                SetPointMoveCanvas(square, Poi);
            }
        }
        public Move GetMove(Position from, Position to)
        {
            return Moves.FirstOrDefault(m => m.FromPos.Equals(from) && m.ToPos.Equals(to));
        }
        public Icon GetIcon(Position position)
        {
            return Icons.FirstOrDefault(i => i.Position.Equals(position));
        }
        public void SetEvaluateIcons(Position position, EvaluateIcon evaluate)
        {
            if (GetIcon(position) is Icon icon)
            {
                icon.EvaluateIcon = evaluate;
            }
            else
            {
                Icons.Add(new(position, Icons) { EvaluateIcon = evaluate }!);
            }
        }
        private void SetHighLightArrow(Position from, Position to)
        {
            if (GetArrow(from, to) is Arrow arrow)
            {
                arrow.BlurColorArrow();
                if (arrow.EvaluateArrow == EvaluateArrow.None && arrow.HighLightArrow == HighLightArrow.None)
                {
                    Arrows.Remove(arrow);
                }
            }
            else
            {
                Arrows.Add(arrow = new(from, to, this));
                arrow.BlurColorArrow();
            }
        }
        public Arrow GetArrow(Position from, Position to)
        {
            return Arrows.FirstOrDefault(a => a.Points.Start.Equals(from) && a.Points.End.Equals(to));
        }
        public void SetEvaluateArrow(Position from, Position to, EvaluateArrow evaluate)
        {
            if (GetArrow(from, to) is Arrow arrow)
            {
                arrow.EvaluateArrow = evaluate;
            }
            else
            {
                Arrows.Add(new(from, to, this) { EvaluateArrow = evaluate });
            }
        }
        public Square GetSquare(Position position)
        {
            return Squares.FirstOrDefault(s => s.Position.Equals(position) && s.Visibility == Visibility.Visible);
        }
        private void SetHintforMove(Position to)
        {
            Hints.Remove(Hints.FirstOrDefault(h => h.Position.Equals(to)));
            if (GetSquare(to) is not null)
            {
                Hints.Add(new(to, HintType.Capture));
            }
            else
            {
                Hints.Add(new(to, HintType.Normal));
            }
        }
        public Overlay GetOverlay(Position position)
        {
            return Overlays.FirstOrDefault(o => o.Position.Equals(position));
        }
        private void SetBlurOverlay(Position position)
        {
            if (GetOverlay(position) is Overlay overlay)
            {
                overlay.BlurColorOverlay();
            }
            else
            {
                Overlays.Add(overlay = new(new(position.Row, position.Column), Overlays));
                overlay.BlurColorOverlay();
            }
        }
        public void SetCheckOverlay(Position position)
        {
            if (GetOverlay(position) is Overlay overlay)
            {
                _ = overlay.Warning();
            }
            else
            {
                Overlays.Add(overlay = new(new(position.Row, position.Column), Overlays));
                _ = overlay.Warning();
            }
        }
        public void SetEvaluateOverlay(Position position, EvaluateOverlay evaluate)
        {
            if (GetOverlay(position) is Overlay overlay)
            {
                overlay.EvaluateOverlay = evaluate;
            }
            else
            {
                Overlays.Add(new(position, Overlays) { EvaluateOverlay = evaluate });
            }
        }
        private void SetHighLightOverlay(Position position, HighLightOverlay highLight)
        {
            if (GetOverlay(position) is Overlay overlay)
            {
                overlay.HighLightOverlay = highLight;
            }
            else
            {
                Overlays.Add(new(position, Overlays) { HighLightOverlay = highLight });
            }
        }
        public void HandleMove(Move move, Square from = null)
        {
            MoveCanvas.Children.Clear();
            UpdateForNewMove(move);
            GameState.MakeMove(move, from);
        }
        private void HandlePromotionMove(PawnPromotion move, Square from)
        {
            MoveCanvas.Children.Clear();
            PromotionControl promotion = new(from.Piece.Color);
            SetPointPromotionCanvas(promotion, move.ToPos);
            promotion.PieceSelected += select =>
            {
                if (select is PieceType type)
                {
                    MoveHasUnMake.Clear();
                    move.newType = type;
                    HandleMove(move, from);
                }
            };
        }
        private void UpdateForNewMove(Move move)
        {
            Tag = null;
            Moves.Clear();
            Icons.Clear();
            Arrows.Clear();
            Overlays.Clear();
            MoveCanvas.Children.Clear();
            SetEvaluateOverlay(move.ToPos, EvaluateOverlay.Move);
            SetEvaluateOverlay(move.FromPos, EvaluateOverlay.Move);
        }
        private void HandleNotMove(Square fromsquare)
        {
            Tag = null;
            MoveCanvas.Children.Clear();
            fromsquare.Visibility = Visibility.Visible;
            SetHighLightOverlay(fromsquare.Position, HighLightOverlay.None);
        }
        public void HandleUnMove(Move move, Square from = null)
        {
            Tag = null;
            Moves.Clear();
            Icons.Clear();
            Arrows.Clear();
            Overlays.Clear();
            GameState.UnMakeMove(move, from);
            if (MoveHasMake.Count > 0 && MoveHasMake.Peek() is (Move, Square) backmove)
            {
                SetEvaluateOverlay(backmove.Item1.FromPos, EvaluateOverlay.Move);
                SetEvaluateOverlay(backmove.Item1.ToPos, EvaluateOverlay.Move);
            }
        }
        private void SetPointPromotionCanvas(PromotionControl promotion, Position to)
        {
            if (!PromotionCanvas.Children.Contains(promotion))
            {
                PromotionCanvas.Children.Add(promotion);
            }
            Canvas.SetLeft(promotion, to.Column * Boardd.ActualWidth / 8);
            if (GameState.CurrentPlayer == Player.White)
            {
                Canvas.SetTop(promotion, 0);
                Canvas.SetBottom(promotion, Double.NaN);
            }
            else if (GameState.CurrentPlayer == Player.Black)
            {
                Canvas.SetTop(promotion, Double.NaN);
                Canvas.SetBottom(promotion, 0);
            }
        }
        public void NewGame()
        {
            GameState = new(Player.White, this);
            LoadBoard(Fen);
            Sounds.PlaySound(Resource.game_start);
        }
        private void LoadBoard(string fen)
        {
            fen ??= Fen;
            Hints.Clear();
            Moves.Clear();
            Icons.Clear();
            Arrows.Clear();
            Hovers.Clear();
            Squares.Clear();
            Overlays.Clear();
            MoveHasMake.Clear();
            MoveHasUnMake.Clear();
            MoveCanvas.Children.Clear();
            PromotionCanvas.Children.Clear();
            Tag = null;
            string[] rows = fen.Split(' ')[0].Split('/');
            for (int row = 0; row < rows.Length; row++)
            {
                int col = 0;
                foreach (char c in rows[row])
                {
                    if (char.IsDigit(c))
                    {
                        col += c - '0';
                    }
                    else if (Piece.GetPiece(c) is Piece piece)
                    {
                        Squares.Add(new(piece, new(row, col))!);
                        col++;
                    }
                }
            }
        }
        public Position ToPosition(Point point)
        {
            return new(Math.Max(0, Math.Min((int)(point.Y / (Boardd.ActualHeight / 8)), 7)), Math.Max(0, Math.Min((int)(point.X / (Boardd.ActualWidth / 8)), 7)));
        }
        public Point ToPoint(Position position)
        {
            return new(position.Column * (Boardd.ActualWidth / 8) + Boardd.ActualWidth / 8 / 2, position.Row * (Boardd.ActualHeight / 8) + Boardd.ActualHeight / 8 / 2);
        }
        public static bool IsInside(Position pos)
        {
            return pos >= new Position(0, 0) && pos < new Position(8, 8);
        }
        public void WarningCheck(Player player)
        {
            Sounds.PlaySound(Resource.illegal);
            if (Squares.FirstOrDefault(s => s.Piece.Type == PieceType.King && s.Piece.Color == player) is Square square)
            {
                SetCheckOverlay(square.Position);
            }
        }
    }
}
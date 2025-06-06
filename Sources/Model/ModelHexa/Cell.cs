using System.ComponentModel;

namespace ModelHexa
{
    public class Cell : INotifyPropertyChanged
    {
        public int X { get; private init; }
        public int Y { get; private init; }
        private Pawn? _pawn;
        public Pawn? Pawn
        {
            get { return _pawn; }
            set
            {
                if (!Equals(_pawn, value))
                {
                    _pawn = value;
                    OnPropertyChanged(nameof(Pawn));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Cell(int x, int y, Pawn? p=null)
        {
            X = x;
            Y = y;
            Pawn = p;
        }
        public bool Empty()
        {
            return Pawn == null;
        }
        public override string ToString()
        {
            if (Pawn == null) return "";
            if (Pawn.Value.Color == TeamColor.Player1) return "♙";
            return "♟";
        }
        
        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
    
}







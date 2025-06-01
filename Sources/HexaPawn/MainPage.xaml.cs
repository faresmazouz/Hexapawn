using System.ComponentModel;
using ModelHexa;

namespace solution;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    private Board _plateau = new(3);
    public Board Plateau
    {
        get => _plateau;
        set
        {
            if (_plateau != value)
            {
                _plateau = value;
                OnPropertyChanged(nameof(Plateau));
            }
        }
    }

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
using System.ComponentModel;
using ModelHexa;

namespace solution;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    private Rules Rules { get; set; }
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
    private void ChangeVisibility()
    {
        texteMode.IsVisible = !texteMode.IsVisible;
        cl.IsVisible = !cl.IsVisible;
        qxq.IsVisible = !qxq.IsVisible;
        cxc.IsVisible = !cxc.IsVisible;
        Scores.IsVisible = !Scores.IsVisible;
        uvu.IsVisible = !uvu.IsVisible;
        Bvu.IsVisible = !Bvu.IsVisible;
        BvB.IsVisible = !BvB.IsVisible;
        GiveUp.IsVisible = !GiveUp.IsVisible;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    private async void OnClickedScores(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Scores());
    }
    private void OnClickedClassic(object sender, EventArgs e)
    {
        Plateau = new(3);
    }
    private void OnClicked4x4(object sender, EventArgs e)
    {
        Plateau = new(4);
    }
    private void OnClicked5x5(object sender, EventArgs e)
    {
        Plateau = new(5);
    }
    private void OnClicked1v1(object sender, EventArgs e)
    {
        ChangeVisibility();
    }
    private void OnClickedBv1(object sender, EventArgs e)
    {
        ChangeVisibility();
    }
    private void OnClickedBvB(object sender, EventArgs e)
    {
        ChangeVisibility();
    }
    private void OnClickedGiveUp(object sender, EventArgs e)
    {
        ChangeVisibility();
    }
    private async void OnClickedApparence(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ThemePage());
    }


}
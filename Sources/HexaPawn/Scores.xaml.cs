using ModelHexa;
namespace Hexapawn;


public partial class Scores : ContentPage
{
    public Manager Mgr { get; private set; } = new Manager(); 

    public Scores()
    {
        
        InitializeComponent(); 
        Mgr.Charger(); // Charger les scores depuis le fichier XML
        BindingContext = this;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Mgr.Charger(); // Recharger les scores à chaque fois que la page apparaît
        OnPropertyChanged(nameof(Mgr)); // Notifier que le contexte de liaison a changé
    }
}

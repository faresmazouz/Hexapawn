using ModelHexa;
namespace Hexapawn;


public partial class Scores : ContentPage
{
    public Manager Mgr { get; private set; } = new Manager(); // Ensure 'Manager' is defined in the 'ModelHexa' namespace

    public Scores()
    {
        //InitializeComponent();
        BindingContext = this;
    }
}
    
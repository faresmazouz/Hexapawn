namespace solution;
public partial class ThemePage : ContentPage
{
	public ThemePage()
	{
		InitializeComponent();
	}

private void OnThemeSelected(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is string themeName)
        {
            // Ici tu récupères le thème sélectionné
            Console.WriteLine($"Thème sélectionné : {themeName}");
        }
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
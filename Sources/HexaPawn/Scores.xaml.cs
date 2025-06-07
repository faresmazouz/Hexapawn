

namespace solution;

public partial class Scores : ContentPage
{
	public Scores()
	{
		InitializeComponent();
	}

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}

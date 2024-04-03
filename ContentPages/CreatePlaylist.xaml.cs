namespace BetaWaveMultiplatform;

public partial class CreatePlaylist : ContentPage
{
	public CreatePlaylist()
	{
		InitializeComponent();
    }

    async void CPBackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainMenu");
    }
	
}
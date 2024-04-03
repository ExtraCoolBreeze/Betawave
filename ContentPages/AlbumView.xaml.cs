namespace BetaWaveMultiplatform;

public partial class AlbumView : ContentPage
{
	public AlbumView()
	{
		InitializeComponent();
	}

	async void AVBackButton_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}

}
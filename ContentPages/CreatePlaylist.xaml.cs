using Betawave.Classes;
namespace Betawave;

public partial class CreatePlaylist : ContentPage
{
	public CreatePlaylist()
	{
		InitializeComponent();
    }


    async void CPCreateButton_Clicked(object sender, EventArgs e)
    {
        string PlaylistName = PlaylistNameEntry.Text;

        if (string.IsNullOrWhiteSpace(PlaylistName))
        {
            await DisplayAlert("Error", "Playlist must have a name and cannot be blank.", "OK");
            return;
        }
        else
        {
            await Shell.Current.GoToAsync("///PlaylistView");
        }
    }


    async void CPBackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainMenu");
    }
	
}
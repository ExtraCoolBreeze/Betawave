using Betawave.ViewModels;

namespace Betawave
{ 
    public partial class AddAlbum : ContentPage
    {
	    public AddAlbum()
        {
            InitializeComponent();
            BindingContext = new AddAlbumViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is AddAlbumViewModel viewModel)
            {
                viewModel.ResetFields();
            }
        }


        async void OnBackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///AddMediaScreen");
        }
    }
}
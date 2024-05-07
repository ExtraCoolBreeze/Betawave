/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This is the code behind the AddAlbum page and is to create and run the page as well as deal with simple interactions   */

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
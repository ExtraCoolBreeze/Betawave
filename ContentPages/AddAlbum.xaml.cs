/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This is the code behind the AddAlbum page and is to create and run the page as well as deal with simple interactions   */

using Betawave.ViewModels;

namespace Betawave
{   //inheriting from content page
    public partial class AddAlbum : ContentPage
    {
	    public AddAlbum()
        {   //initialising page and adding bindings
            InitializeComponent();
            BindingContext = new AddAlbumViewModel();
        }
        
        /// <summary>
        /// When called this function overrides the information displayed in the text entry fields
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is AddAlbumViewModel viewModel)
            {
                viewModel.ResetFields();
            }
        }

        /// <summary>
        /// This function activates when the BackButton is pressed in the UI and takes you back to the Add Media Screen page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnBackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///AddMediaScreen");
        }
    }
}
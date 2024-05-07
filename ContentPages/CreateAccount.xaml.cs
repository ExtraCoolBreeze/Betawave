/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This is the code behind the CreateAccount page and is to create and run the page as well as deal with simple interactions   */

using Betawave.ViewModels;

namespace Betawave;

public partial class CreateAccount : ContentPage
{
    public CreateAccount()
    {
        InitializeComponent();
        Appearing += OnAppearingHandler;
    }

    public void OnAppearingHandler(object sender, EventArgs e)
    {
        var viewModel = BindingContext as CreateAccountViewModel;
        if (viewModel != null)
        {
            viewModel.Username = "";
            viewModel.Password = "";
        }
    }

    async void CABackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LoginScreen");
    }
}
 
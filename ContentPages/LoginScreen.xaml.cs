using Betawave.Classes;
using Betawave.ViewModels;

namespace Betawave;

public partial class LoginScreen : ContentPage
{

    public LoginScreen()
    {
        InitializeComponent();
        this.Appearing += OnAppearingHandler;
    }

    private void OnAppearingHandler(object sender, EventArgs e)
    {
        var viewModel = this.BindingContext as LoginViewModel;
        if (viewModel != null)
        {
            viewModel.Username = "";  // Clear the username
            viewModel.Password = "";  // Clear the password
        }
    }

    async void CreateAccountButton_Clicked(object sender, EventArgs e)
    {
        // Navigate to the CreateAccount page
        await Shell.Current.GoToAsync("///CreateAccount");
    }

    private void LSCloseButton_Clicked(object sender, EventArgs e)
    {
        // Close the application
        Application.Current.Quit();
    }
}

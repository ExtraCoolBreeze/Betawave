using BetaWaveMultiplatform.Classes; // Ensure this using directive is added to access the Account and DatabaseAccess classes

namespace BetaWaveMultiplatform;

public partial class LoginScreen : ContentPage
{
    private Account account = new Account(); // Instantiate Account to access its Login method
    private DatabaseAccess dbAccess = new DatabaseAccess(); // Assuming DatabaseAccess is needed for account operations

    public LoginScreen()
    {
        InitializeComponent();
    }

    async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string enteredUsername = usernameEntry.Text;
        string enteredPassword = passwordEntry.Text;

        // Directly attempt login with the entered credentials
        if (!dbAccess.ValidateUser(enteredUsername, enteredPassword))
        {
            // Logic to determine the navigation based on user role should ideally be here,
            // possibly by extending the ValidateUser method to return more information about the user.
            await Shell.Current.GoToAsync("///MainMenu"); // Adjust navigation as needed
        }
        else
        {
            // Inform the user about incorrect credentials
            await DisplayAlert("Error", "Username or password incorrect.", "OK");
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

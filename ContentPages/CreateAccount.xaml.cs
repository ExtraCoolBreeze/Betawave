using Betawave.Classes;

namespace Betawave;

public partial class CreateAccount : ContentPage
{
    private Account account = new Account();
    private DatabaseAccess dbAccess = new DatabaseAccess();

    public CreateAccount()
    {
        InitializeComponent();
    }

    async void CACreateButton_Clicked(object sender, EventArgs e)
    {
        string enteredUsername = usernameEntry.Text;
        string enteredPassword = passwordEntry.Text;

        if (!account.SetUsername(enteredUsername, out string usernameError))
        {
            await DisplayAlert("Invalid username", usernameError, "OK");
            return;
        }

        if (!account.SetPassword(enteredPassword, out string passwordError))
        {
            await DisplayAlert("Invalid Password", passwordError, "OK");
            return;
        }

        if (!dbAccess.CheckUserExists(enteredUsername))
        {
            dbAccess.CreateUser(enteredUsername, enteredPassword);
            await DisplayAlert("New User", "Account created successfully. Please take note of your password for later use.", "OK");
            await Shell.Current.GoToAsync("///MainMenu");
        }
        else
        {
            await DisplayAlert("User Exists", "The username is already taken. Please choose a different one.", "OK");
        }
    }

    async void CABackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LoginScreen");
    }
}
 
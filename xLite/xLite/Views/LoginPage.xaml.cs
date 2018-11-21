using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xLite.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xLite.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
	    Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public LoginPage ()
		{
			InitializeComponent ();
        }
	    async void OnSignUpButtonClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new SignUpPage());
	    }

	    async void OnLoginButtonClicked(object sender, EventArgs e)
	    {
	        var user = new User
	        {
	            Username = usernameEntry.Text,
	            Password = passwordEntry.Text
	        };

	        var isValid = AreCredentialsCorrect(user);
	        if (isValid)
	        {
	            App.IsUserLoggedIn = true;
	            Navigation.InsertPageBefore(new MainPage(), this);
	            await Navigation.PopAsync();
	        }
	        else
	        {
	            messageLabel.Text = "Login failed";
	            passwordEntry.Text = string.Empty;
	        }
	    }

	    bool AreCredentialsCorrect(User user)
	    {
	        return user.Username.Equals(Constants.Username, StringComparison.OrdinalIgnoreCase)
	               && user.Password.Equals(Constants.Password, StringComparison.OrdinalIgnoreCase);
	    }
    }
}
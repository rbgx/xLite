using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xLite.Models;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace xLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        public LoginPage()
        {
            InitializeComponent();
            //this.Resources.Add(StyleSheet.FromAssemblyResource(
            //    typeof(LoginPage).GetTypeInfo().Assembly,
            //    "MyProject.Assets.styles.css"));
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(usernameEntry.Text))
            {
                errors.Add("Login Email is Required");
            }
            else if (!IsValidEmail(usernameEntry.Text))
            {
                errors.Add("Invalid Login Email");
            }
            if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                errors.Add("Password is Required");
            }

            if (errors.Any())
            {
                messageLabel.Text = string.Empty;
                foreach (var error in errors)
                {
                    messageLabel.Text = messageLabel.Text + "\n" + error;
                }

                passwordEntry.Text = string.Empty;
                return;
            }

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
            return user.Username.Equals(Constants.LoginEmail, StringComparison.OrdinalIgnoreCase)
                   && user.Password.Equals(Constants.Password, StringComparison.OrdinalIgnoreCase);
        }
    }
}
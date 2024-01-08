using Microsoft.Maui.Controls;
using WorldleGameMichaelFerry;

namespace WordleGameMichaelFerry
{
    public partial class LoginPage : ContentPage
    {
        private Entry UsernameEntry;
        private Entry PasswordEntry;
        private Button LoginButton;

        public LoginPage()
        {
            InitializeComponent();

            this.BackgroundColor = Color.FromArgb(DefaultConstants.BackgroundColour());

            CreateUserInterface();

            LoginButton.Clicked += LoginButtonClicked;
        }

        private void CreateUserInterface()
        {
            Label headerLabel = new Label
            {
                Text = "Login",
                FontSize = 50,
                TextColor = Colors.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };
            Label wordleLabel = new Label
            {
                Text = "WORDLE",
                FontAttributes = FontAttributes.Bold,
                FontSize = 75, 
                TextColor = Colors.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 30)

            };

            UsernameEntry = new Entry
            {
                Placeholder = "Username",
                HorizontalOptions = LayoutOptions.Center,
                PlaceholderColor = Colors.LightGray,
                WidthRequest = 240,
                Margin = new Thickness(0, 0, 0, 10),
                FontSize = 16
            };

            PasswordEntry = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
                HorizontalOptions = LayoutOptions.Center,
                PlaceholderColor = Colors.LightGray,
                WidthRequest = 240,
                Margin = new Thickness(0, 0, 0, 20),
                FontSize = 16
            };

            LoginButton = new Button
            {
                Text = "Login",
                TextColor = Colors.White,
                BackgroundColor = Color.FromArgb(DefaultConstants.ButtonBackgroundColour(1)),
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 120,
                Margin = new Thickness(0, 0, 0, 0)
            };

            Content = new VerticalStackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = { wordleLabel,headerLabel, UsernameEntry, PasswordEntry, LoginButton }
            };
        }

        private async void LoginButtonClicked(object sender, EventArgs e)
        {
            
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

           
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
               
                await DisplayAlert("Login Error", "Invalid username or password.", "OK");
            }
        }
    }
}

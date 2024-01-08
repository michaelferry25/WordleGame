using System.Text.Json;
using WorldleGameMichaelFerry;

namespace WordleGameMichaelFerry
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            ListOfWords.LoadWordsAsync();

            this.BackgroundColor = Color.FromArgb(DefaultConstants.BackgroundColour());
            Label titleLabel = new Label
            {
                Text = "WORDLE",
                FontSize = 80,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.DarkSlateGrey,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            Image mainImage = new Image
            {
                Source = "WordleGameMichaelFerry\\Resources\\Images\\wordle_logo.png", 
                HeightRequest = 300, 
                WidthRequest = 300, 
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };


            //Button for play with set dimentions
            Button playButton = new Button
            {
                Text = "Play",
                BackgroundColor = Color.FromArgb(DefaultConstants.ButtonBackgroundColour(2)),
                TextColor = Colors.White,
                FontSize = 30,
                HeightRequest = 60,
                WidthRequest = 500,
                CornerRadius = 15,
                Margin = 10,
                HorizontalOptions = LayoutOptions.Center
            };

            //Button for player instructions with set dimentions
            Button instructionsButton = new Button
            {
                Text = "Instructions",
                BackgroundColor = Color.FromArgb(DefaultConstants.ButtonBackgroundColour(1)),
                TextColor = Colors.White,
                FontSize = 30,
                HeightRequest = 60,
                WidthRequest = 300,
                CornerRadius = 15,
                Margin = 10,
                HorizontalOptions = LayoutOptions.Center
            };

            //Button for player settings with set dimentions
            Button settingsButton = new Button
            {
                Text = "Settings",
                BackgroundColor = Color.FromArgb(DefaultConstants.ButtonBackgroundColour(1)),
                TextColor = Colors.White,
                FontSize = 30,
                HeightRequest = 60,
                WidthRequest = 300,
                CornerRadius = 15,
                Margin = 10,
                HorizontalOptions = LayoutOptions.Center
            };

            //Button for the About game with set dimentions
            Button aboutButton = new Button
            {
                Text = "Credits",
                BackgroundColor = Color.FromArgb(DefaultConstants.ButtonBackgroundColour(1)),
                TextColor = Colors.White,
                FontSize = 20,
                HeightRequest = 60,
                WidthRequest = 250,
                CornerRadius = 10,
                Margin = 5,
                HorizontalOptions = LayoutOptions.Center
            };

            playButton.Clicked += PlayButtonClicked;
            settingsButton.Clicked += SettingsButtonClicked;
            instructionsButton.Clicked += InstructionsButtonClicked;
            aboutButton.Clicked += AboutButtonClicked;
            
            topLevel.Children.Add(titleLabel);
            topLevel.Children.Add(mainImage);    
            topLevel.Children.Add(playButton);

            middleLevel.Children.Add(instructionsButton);
            middleLevel.Children.Add(aboutButton);
            middleLevel.Children.Add(settingsButton);
        }
        //All the pushers for when a button is clicked to bring you to the destination page

        //Pushes you to Game page
        private async void PlayButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
        }

        //Pushes you to Settings
        private async void SettingsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        //Pushes you to InstrcutionsPages
        private async void InstructionsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InstructionsPage());
        }

        private async void AboutButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }



    }
}
using WordleGameMichaelFerry;
using WorldleGameMichaelFerry;

namespace WordleGameMichaelFerry;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
        InitializeComponent();

        CreateUserInterface();
    }

    private void CreateUserInterface()
    {
        this.BackgroundColor = Color.FromArgb(DefaultConstants.BackgroundColour());
        layout.Children.Add(CreateTheStyledButton("Exit", OnExitButtonClicked, 1));
    }
    private static Button CreateTheStyledButton(string text, EventHandler handler, int buttonType)
    {
        var button = new Button
        {
            Text = text,
            BackgroundColor = Color.FromArgb(DefaultConstants.ButtonBackgroundColour(buttonType)),
            TextColor = Colors.White,
            FontSize = 32,
            HeightRequest = 70,
            WidthRequest = 300,
            CornerRadius = 20,
            Margin = 10,
            HorizontalOptions = LayoutOptions.Center
        };

        if (handler != null)
        {
            button.Clicked += handler;
        }

        return button;
    }

    private void OnExitButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

}

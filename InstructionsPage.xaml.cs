using WorldleGameMichaelFerry;

namespace WordleGameMichaelFerry
{
    public partial class InstructionsPage : ContentPage
    {

        public InstructionsPage()
        {
            InitializeComponent();
            CreateUserInterface();
        }

                //Function for User interface
                private void CreateUserInterface()
                {
                    this.BackgroundColor = Color.FromArgb(DefaultConstants.BackgroundColour());
                    layout.Children.Add(CreateTheStyledButton("Exit", OnTheExitButtonClicked, 1));
                }

                    //Button Dimentions and event handler
                    private static Button CreateTheStyledButton(string text, EventHandler handler, int buttonType)
                    {
                        var button = new Button
                        {
                            Text = text,
                            BackgroundColor = Color.FromArgb(DefaultConstants.ButtonBackgroundColour(buttonType)),
                            TextColor = Colors.White,
                            FontSize = 30,
                            HeightRequest = 70,
                            WidthRequest = 300,
                            CornerRadius = 15,
                            Margin = 10,
                            HorizontalOptions = LayoutOptions.Center
                        };

                        if (handler != null)
                        {
                            button.Clicked += handler;
                        }

                        return button;
                    }
        //When Exit is pressed pushes you back to main page
        private void OnTheExitButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

    }
}

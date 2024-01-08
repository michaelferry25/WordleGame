using WorldleGameMichaelFerry;

namespace WordleGameMichaelFerry
{
    public partial class SettingsPage : ContentPage
    {
        Switch dark_mode_selector;
        Switch hints_selector;

        public SettingsPage()
        {
            InitializeComponent();
            CreateUserInterface();
            ApplyDefaultSettings();
        }

        private void CreateUserInterface()
        {
            this.BackgroundColor = Color.FromArgb(DefaultConstants.BackgroundColour());

            dark_mode_selector = CreateSwitch();
            layout.Children.Add(CreateLabel("Dark Mode"));
            layout.Children.Add(dark_mode_selector);

            hints_selector = CreateSwitch();
            layout.Children.Add(CreateLabel("Hints"));
            layout.Children.Add(hints_selector);


            layout.Children.Add(CreateStyledButton("Save", SaveButtonClicked, 1));
            layout.Children.Add(CreateStyledButton("Reset to Defaults", ResetButtonClicked, 1));
            layout.Children.Add(CreateStyledButton("Save and Exit", SaveAndExitButtonClicked, 2));
            layout.Children.Add(CreateStyledButton("Exit without saving", ExitWithoutSavingButtonClicked, 1));
        }

        private static Switch CreateSwitch()
        {
            return new Switch {};
        }

        private static Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text
            };
        }

        private static Button CreateStyledButton(string text, EventHandler handler, int buttonType)
        {
            var button = new Button
            {
                Text = text,
                BackgroundColor = Color.FromArgb(DefaultConstants.ButtonBackgroundColour(buttonType)),
                TextColor = Colors.White,
                FontSize = 30,
                HeightRequest = 60,
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

        private void ApplyDefaultSettings()
        {
            dark_mode_selector.IsToggled = DefaultConstants.DarkMode();
            hints_selector.IsToggled = DefaultConstants.HintEnabled();
        }

        private void SaveButtonClicked(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void ResetButtonClicked(object sender, EventArgs e)
        {
            ResetToDefaults();
        }

        private void SaveAndExitButtonClicked(object sender, EventArgs e)
        {
            SaveAndExit();
        }

        private void ExitWithoutSavingButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void SaveSettings()
        {
            var IsDarkMode = dark_mode_selector.IsToggled;
            var showHints = hints_selector.IsToggled;

            Preferences.Default.Set("dark_mode", IsDarkMode);
            Preferences.Default.Set("show_hints", showHints);
        }

        private void ResetToDefaults()
        {
            dark_mode_selector.IsToggled = false;
            hints_selector.IsToggled = false;
            Preferences.Default.Set("dark_mode", false);
            Preferences.Default.Set("font-size", 30);
            Preferences.Default.Set("show_hints", false);
        }

        private void SaveAndExit()
        {
            SaveSettings();
            Navigation.PushAsync(new MainPage());
        }
    }
}

using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using WorldleGameMichaelFerry;

namespace WordleGameMichaelFerry
{
    public partial class GamePage : ContentPage
    {

        // Constants here are identifiers for when a letter is incorrect, correct or misplaced
        const int LETTER_INCORRECT = -1;
        const int LETTER_CORRECT = 1;
        const int LETTER_MISPLACED = 2;

        // Game constants
        const int GAME_WON = 0;
        const int GAME_LOST = 1;

        // "RowIndex" and "ColumnIndex" are the index of the currently used column and row during the game, when the game begins it starts off on 0,0 but when a user enters 0,0 the value of the column increases and when a 
        // word is entered and submitted, the row increases and the column resets back to the default of 0,0
       
        private int RowIndex = 0;
        private int ColumnIndex = 0;

        private int charButtonLoadCount = 1;

        // The following variable increases/decreases depending on the amount of characters entered, acts like a counter and will only allow max of 5 characters to be entered
        private int indexedChars = 0;



        // Amount of tries on guessing
        private int attempts = 0;

        private bool gameEndStatus = false;

        string randomWord; // Randomised word is stored here

        // When a user enters a guess, it will be stored in this array
        private char[] lettersArray = { '-', '-', '-', '-', '-'};

        // The random word will be split up into a set of chars, stored here
        private char[] charArray;

        // Positions of all of the chars get stored here 
        Dictionary<char, int[]> indexedCharacterPositions = new Dictionary<char, int[]>();


        public GamePage()
        {
            InitializeComponent();

            randomWord = ListOfWords.GetRandomWord().ToUpper(); // Random word gets chosen

            charArray = randomWord.ToCharArray(); // String split up and stored in array

            CreateUI();

            InitializeLetterBox();
            
        }

        // Gameplay UI will be loaded in with this method
        private void CreateUI()
        {
            this.BackgroundColor = Color.FromArgb(DefaultConstants.BackgroundColour());

            // Positions of all of the character buttons will be loaded in here

            indexedCharacterPositions['A'] = new int[] { 0, 0 };
            indexedCharacterPositions['B'] = new int[] { 0, 1 };
            indexedCharacterPositions['C'] = new int[] { 0, 2 };
            indexedCharacterPositions['D'] = new int[] { 0, 3 };
            indexedCharacterPositions['E'] = new int[] { 0, 4 };
            indexedCharacterPositions['F'] = new int[] { 0, 5 };
            indexedCharacterPositions['G'] = new int[] { 0, 6 };
            indexedCharacterPositions['H'] = new int[] { 0, 7 };

            indexedCharacterPositions['I'] = new int[] { 1, 0 };
            indexedCharacterPositions['J'] = new int[] { 1, 1 };
            indexedCharacterPositions['K'] = new int[] { 1, 2 };
            indexedCharacterPositions['L'] = new int[] { 1, 3 };
            indexedCharacterPositions['M'] = new int[] { 1, 4 };
            indexedCharacterPositions['N'] = new int[] { 1, 5 };
            indexedCharacterPositions['O'] = new int[] { 1, 6 };
            indexedCharacterPositions['P'] = new int[] { 1, 7 };

            indexedCharacterPositions['Q'] = new int[] { 2, 0 };
            indexedCharacterPositions['R'] = new int[] { 2, 1 };
            indexedCharacterPositions['S'] = new int[] { 2, 2 };
            indexedCharacterPositions['T'] = new int[] { 2, 3 };
            indexedCharacterPositions['U'] = new int[] { 2, 4 };
            indexedCharacterPositions['V'] = new int[] { 2, 5 };
            indexedCharacterPositions['W'] = new int[] { 2, 6 };
            indexedCharacterPositions['X'] = new int[] { 2, 7 };

            indexedCharacterPositions['Y'] = new int[] { 3, 0 };
            indexedCharacterPositions['Z'] = new int[] { 3, 1 };

            LoadCharButtons();
        }

        // The following method simply loops through the grid adding empty spaces that will be filled in by the player with their characters
        private void InitializeLetterBox()
        {
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    var label = new Label
                    {
                        Text = " ",
                        FontSize = 20,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        
                        TextColor = Colors.White,
                        BackgroundColor = Color.FromArgb(DefaultConstants.IncorrectGuessBackGroundColour()),
                    Margin = new Thickness(5),
                        HeightRequest = 60,
                        WidthRequest = 60
                    };


                    if(x == RowIndex)
                    {
                        label.BackgroundColor = Colors.White;
                        label.TextColor = Colors.Black;
                    }
                    lettersGrid.Children.Add(label);
                    Microsoft.Maui.Controls.Grid.SetRow(label, x);
                    Microsoft.Maui.Controls.Grid.SetColumn(label, y);
                }
            }
        }

        // Reapplys the styling back onto the grid boxes
        private void ReapplyLetterBox()
        {
            for (int y = 0; y < 5; y++) {
                 foreach (var child in lettersGrid.Children)
                    {
                     if (lettersGrid.GetRow(child) == RowIndex && lettersGrid.GetColumn(child) == y && child is Label label)
                        {

                        label.BackgroundColor = Colors.White;
                        label.TextColor = Colors.Black;

                        break;
                     }
                }
            }
        }

        // Resets all of the character grid boxes when game resets
        private void ResetLetterBox()
        {
        
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    foreach (var child in lettersGrid.Children)
                    {
                        if (lettersGrid.GetRow(child) == x && lettersGrid.GetColumn(child) == y && child is Label label)
                        {
                            label.Text = "";
                            if (x != RowIndex)
                            { 
                                label.BackgroundColor = Colors.SlateGray;
                            }
                            else
                            {
                                label.BackgroundColor = Colors.White;
                            }
                            break;
                        }
                    }
                }
            }
        }
        // This method checks what char was selected and then adds it to the grid
        private void OnCharBtnClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if(indexedChars < 5) // Word is only max 5 letters so this adds a limiter
                {
         
                    char selectedLetter = button.Text.Length > 0 ? button.Text[0] : 'A'; // Checks what char was entered
                    AddLetterToGrid(selectedLetter); // Adds character to the grid
                    indexedChars++; // Increases so that it moves to the next column in the grid
                }
            }
        }

        // Whenever the backspace is clicked, it will remove it from the grid
        private void OnBackspaceBtnClicked(object sender, EventArgs e)
        {
            if (indexedChars < 0) return;
                foreach (var child in lettersGrid.Children)
                {
                    if (lettersGrid.GetRow(child) == RowIndex && lettersGrid.GetColumn(child) == ColumnIndex - 1 && child is Label label)
                    {
                        label.Text = "";
                        break;
                    }
                }

                ColumnIndex--;
                indexedChars--;


                if (ColumnIndex < 0)
                {
                    RowIndex--;


                    if (RowIndex >= 0)
                    {
                        ColumnIndex = lettersGrid.ColumnDefinitions.Count - 1;
                    }
                    else
                    {
                        RowIndex = 0;
                        ColumnIndex = 0;
                    
                }
            }
        }

        // When the player has entered 5 characters, the submit button will check which chars was correct, misplaced, etc
        private void OnSubmitBtnClicked(object sender, EventArgs e)
        {

            if (indexedChars < 4) return; // Submit button wont work unless 5 chars are entered

            int correct = 0;
  
            if (RowIndex >= 0 && RowIndex < randomWord.Length)
            {
                // This will loop through the current rows column, going through each letter
                for(int column = 0; column < charArray.Length; column++)
                {

                    char currentLetter = lettersArray[column]; // Current columns entered char
                    char correctLetter = charArray[column]; // The correct char that should be entered in this column


                    // The following if statements will determine which characters entered were correct, incorrect or in the wrong position (misplaced)
                    if(currentLetter == correctLetter)
                    {
                        SetCharGridStatus(RowIndex, column, LETTER_CORRECT);
                        correct++;

                    }else if(StringContainsLetter(randomWord, currentLetter)){
                        SetCharGridStatus(RowIndex, column, LETTER_MISPLACED);
                    }
                    else
                    {
                        SetCharGridStatus(RowIndex, column, LETTER_INCORRECT);
                        int[] position = indexedCharacterPositions[currentLetter];
                        int rowA = position[0];
                        int colA = position[1];
                        DeactivateAndHighlightButton(rowA, colA); // Deactivates the button if it is not in the word and highlights it for the player
                    }
                }
            }

            // Players who have hints enabled will get a character revealed after 3 tries
            if(attempts == 2 && DefaultConstants.HintEnabled())
            {
                RevealGridCharacter();
                UpdateMessage("A letter has been unveiled, keep trying!");
            }

            if (isValidAnswer())
            {
                ReapplyLetterBox();
                GameCompleted(GAME_WON);
                UpdateMessage("Congratulations! You have guessed correctly and won!");
                return;
            }

            // Game fails if 6 wrong guesses entered
            if (attempts == 5) {
                ReapplyLetterBox();
                GameCompleted(GAME_LOST);
                UpdateMessage("Correct Word was: " + randomWord);
                return;
            }

            RowIndex++;
            attempts++;
            ColumnIndex = 0;
            indexedChars = 0;
        }

        // Method updates the header message above the grid
        private void UpdateMessage(String message)
        {
            headingLabel.Text = message;
        }

        // Validates if an answer is correct or not
        private Boolean isValidAnswer()
        {
            int correct = 0;
            for(int i = 0; i < charArray.Length; i++)
            {
                char c = charArray[i];
                char a = lettersArray[i];
                if(c == a) {
                    correct++;
                }
            }

            if(correct == 5)
            {
                return true;
            }

            return false;
        }

        // Reveals a random letter of the word and highlights it
        private void RevealGridCharacter()
        {
            Random random = new Random();

            // Get a random index within the length of randomWord
            int randomIndex = random.Next(randomWord.Length);

            // Convert the string to a character array
            char[] wordCharArray = randomWord.ToCharArray();

            // Get the randomly selected letter
            char randomLetter = wordCharArray[randomIndex];

            if(ArrayContainsLetter(lettersArray, randomLetter))
            {
                UpdateMessage("You have guessed all the letters, no more can be revealed :c");
                return;
            }

            int[] position = indexedCharacterPositions[randomLetter];
            int rowA = position[0];
            int colA = position[1];
            foreach (var child in keyboardGrid.Children)
            {
                if (child is Button button &&
                    Microsoft.Maui.Controls.Grid.GetRow(button) == rowA &&
                    Microsoft.Maui.Controls.Grid.GetColumn(button) == colA)
                {

                    button.BackgroundColor = Color.FromArgb(DefaultConstants.GetColour("yellow"));

                    break;
                }
            }
        }

        // Method simply deactivates and highlights the character button
        private void DeactivateAndHighlightButton(int row, int column)
        {
            // Iterate through the children of lettersGrid
            foreach (var child in keyboardGrid.Children)
            {
                if (child is Button button &&
                    Microsoft.Maui.Controls.Grid.GetRow(button) == row &&
                    Microsoft.Maui.Controls.Grid.GetColumn(button) == column)
                {
                    // Deactivate the button and change its background color
                    button.IsEnabled = false;
                    button.BackgroundColor = Color.FromArgb(DefaultConstants.ButtonBackgroundColour(3));
                    break;
                }
            }
        }

        // Checks whether or not a string contains a character, returns true if so, false if not
        private static bool StringContainsLetter(String text, char expected)
        {
            char[] letters = text.ToCharArray();

            for (int i = 0; i < letters.Length; i++)
            {
                if (expected == letters[i])
                {
                    return true;
                }
            }

            return false;
        }

        // Same as the above method but check if an array has the letter
        private static bool ArrayContainsLetter(char[] letters, char expected)
        {

            for (int i = 0; i < letters.Length; i++)
            {
                if (expected == letters[i])
                {
                    return true;
                }
            }

            return false;
        }

        // Changes the status of the character in the grid, making its design signal if it was correct, incorrect or misplaced
        private void SetCharGridStatus(int row, int column, int state)
        {
            // Find the label in the specified row and column
            foreach (var child in lettersGrid.Children)
            {
                if (lettersGrid.GetRow(child) == row && lettersGrid.GetColumn(child) == column && child is Label label)
                {

                    switch (state)
                    {
                        case LETTER_INCORRECT:
                            label.BackgroundColor = Color.FromArgb(DefaultConstants.IncorrectGuessBackGroundColour());
                            break;
                        case LETTER_CORRECT:
                            label.BackgroundColor = Color.FromArgb(DefaultConstants.CorrectGuessBackgroundColour());
                            break;
                        case LETTER_MISPLACED:
                            label.BackgroundColor = Color.FromArgb(DefaultConstants.MisplacedLetterBackgroundColour());
                            break;
                    }
                    
                    break;
                }
            }
        }

        // Adds a character to the grid in the correct column/row
        private void AddLetterToGrid(char letter)
        {
            lettersArray[ColumnIndex] = letter;
            foreach (var child in lettersGrid.Children)
            {
                if (lettersGrid.GetRow(child) == RowIndex && lettersGrid.GetColumn(child) == ColumnIndex && child is Label label)
                {
                    label.Text = "" + letter;
                    break;
                }
            }
            ColumnIndex++;
        }

        // This method resets all of variables and all of the functions 
        private void ResetGame()
        {
            keyboardGrid.Children.Clear();
            RowIndex = 0;
            ColumnIndex = 0;
            indexedChars = 0;
            gameEndStatus = false;
          
            attempts = 0;

            randomWord = ListOfWords.GetRandomWord().ToUpper();
            for(int i = 0; i < 5; i++) {
                lettersArray[i] = ' ';
            }
            charArray = randomWord.ToCharArray();

            UpdateMessage("");
            ResetLetterBox();
            LoadCharButtons();
        }

        // When the User Interface is being created, this method is called so the character buttons can be loaded in
        //loads teh characters for the keyboard
        private void LoadCharButtons()
        {
            keyboardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            keyboardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            keyboardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            keyboardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            keyboardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            keyboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            keyboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            keyboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            keyboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            keyboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            keyboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            keyboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            keyboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            //array to store char entered
            lettersArray = new char[] { '-', '-', '-', '-', '-' };
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            for (int i = 0; i < alphabet.Length; i++)
            {
                int[] position = indexedCharacterPositions[alphabet[i]];
                int row = position[0];
                int col = position[1];

                //each character button
                var letterButton = new Button
                {
                    Text = alphabet[i].ToString(),
                    BackgroundColor = Colors.SlateGray,
                    TextColor = Colors.White,
                    FontSize = 20,
                    HeightRequest = 60,
                    WidthRequest = 60,
                    CornerRadius = 10,
                    Margin = new Thickness(5)
                };

                letterButton.Clicked += OnCharBtnClicked;

                if (charButtonLoadCount != 1)
                {
                    keyboardGrid.Children.Add(letterButton);
                    Microsoft.Maui.Controls.Grid.SetRow(letterButton, row);
                    Microsoft.Maui.Controls.Grid.SetColumn(letterButton, col + 1);
                }
                else
                {
                    keyboardGrid.Children.Add(letterButton);
                    Microsoft.Maui.Controls.Grid.SetRow(letterButton, row);
                    Microsoft.Maui.Controls.Grid.SetColumn(letterButton, col);
                }

                letterButton.IsEnabled = true;
            }
                
                //Backspace Button
                var backspaceButton = new Button
                {
                    Text = "Backspace",
                    FontSize = 30,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Colors.White,
                    BackgroundColor = Color.FromArgb(DefaultConstants.ButtonBackgroundColour(1)),
                    CornerRadius = 15,
                    HeightRequest = 70,
                    WidthRequest = 170
                };

                backspaceButton.Clicked += OnBackspaceBtnClicked;

                //Submit button
                var submitButton = new Button
                {
                    Text = "Submit",
                    FontSize = 30,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Colors.White,
                    BackgroundColor = Color.FromArgb(DefaultConstants.ButtonBackgroundColour(3)),
                    CornerRadius = 15,
                    HeightRequest = 70,
                    WidthRequest = 170
                };

                submitButton.Clicked += OnSubmitBtnClicked;
                
                //Exit button
                var exitButton = new Button
                {
                    Text = "Exit",
                    FontSize = 30,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Colors.White,
                    BackgroundColor = Color.FromArgb(DefaultConstants.GetColour("red")),
                    CornerRadius = 15,
                    HeightRequest = 70,
                    WidthRequest = 170
                };

                exitButton.Clicked += (sender, e) =>
                {
                    Navigation.PushAsync(new MainPage());
                };


                keyboardGrid.Children.Add(backspaceButton);
                keyboardGrid.Children.Add(submitButton);
                keyboardGrid.Children.Add(exitButton);

                
                backspaceButton.IsEnabled = true;
                submitButton.IsEnabled = true;
                exitButton.IsEnabled = true;


                        Microsoft.Maui.Controls.Grid.SetRow(backspaceButton, 3);
                        Microsoft.Maui.Controls.Grid.SetColumn(backspaceButton, 2);
                        Microsoft.Maui.Controls.Grid.SetColumnSpan(backspaceButton, 2);

                        Microsoft.Maui.Controls.Grid.SetRow(submitButton, 3);
                        Microsoft.Maui.Controls.Grid.SetColumn(submitButton, 3);
                        Microsoft.Maui.Controls.Grid.SetColumnSpan(submitButton, 4);

                        Microsoft.Maui.Controls.Grid.SetRow(exitButton, 3);
                        Microsoft.Maui.Controls.Grid.SetColumn(exitButton, 6);
                        Microsoft.Maui.Controls.Grid.SetColumnSpan(exitButton, 8);
            
        }

        // Once the game finishes, this method will be called into action
        private async void GameCompleted(int gameStatus)
        {
            keyboardGrid.Children.Clear();
            gameEndStatus = true;

            var menuButton = new Button
            {
                Text = "Exit",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,

                TextColor = Colors.White,
                BackgroundColor = Color.FromArgb(DefaultConstants.ButtonBackgroundColour(0)),
                Margin = new Thickness(5),
                HeightRequest = 60,
                WidthRequest = 100
            };

          
            menuButton.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new MainPage());
            };

            keyboardGrid.Children.Add(menuButton);
            Microsoft.Maui.Controls.Grid.SetRow(menuButton, 0);
            Microsoft.Maui.Controls.Grid.SetColumn(menuButton, 0);

            var replayButton = new Button
            {
                Text = "Play Again",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,

                TextColor = Colors.White,
                BackgroundColor = Color.FromArgb(DefaultConstants.ButtonBackgroundColour(0)),
                Margin = new Thickness(5),
                HeightRequest = 60,
                WidthRequest = 150
            };

            // Handle the Clicked event to navigate back to MainPage
            replayButton.Clicked += (sender, e) =>
            {
                ResetGame();
            };


            keyboardGrid.Children.Add(replayButton);
            Microsoft.Maui.Controls.Grid.SetRow(replayButton, 0);
            Microsoft.Maui.Controls.Grid.SetColumn(replayButton, 1);
           
            
        }

    }


    

}
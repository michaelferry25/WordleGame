using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace WorldleGameMichaelFerry
{
    //Creates default constants for throughout the project so u can have a set standard throughout
    internal class DefaultConstants
    {
        //Create different types
        public const int PRI_TYPE = 1,
                         SEC_TYPE = 2,
                         ALT_TYPE = 3;

        public const String LIGHTMODE_BACKGROUND = "#58ad5a",//Background colour for ui
                            //Create different colour scenarios
                            LIGHTMODE_CORRECT_GUESS_BACKGROUND = "#73c981",
                            LIGHTMODE_INCORRECT_GUESS_BACKGROUND = "#5c5c5c",
                            LIGHTMODE_MISPLACED_GUESS_BACKGROUND = "#ede27e",
                            LIGHTMODE_BUTTON_BACKGROUND_COLOUR_PRIMARY = "#cfc8b8",
                            LIGHTMODE_BUTTON_BACKGROUND_COLOUR_SECONDARY = "#d9caa3",
                            LIGHTMODE_BUTTON_BACKGROUND_COLOUR_ALT = "#c7c4bd",

                            LIGHTMODE_RED = "#e8633a",
                            LIGHTMODE_ORANGE = "#e8ba3a",
                            LIGHTMODE_YELLOW = "#e5e83a",
                            LIGHTMODE_GREEN = "#57e83a",
                            LIGHTMODE_BLUE = "#3a97e8";

        public const String DARKMODE_BACKGROUND = "#328033",//Background colour for ui
                            //doing the same for the dark mode to create a good user ui and experience
                            DARKMODE_CORRECT_GUESS_BACKGROUND = "#63996c",
                            DARKMODE_INCORRECT_GUESS_BACKGROUND = "#404040",
                            DARKMODE_MISPLACED_GUESS_BACKGROUND = "#968f50",
                            DARKMODE_BUTTON_BACKGROUND_COLOUR_PRIMARY = "#7d7b74",
                            DARKMODE_BUTTON_BACKGROUND_COLOUR_SECONDARY = "#6b6659",
                            DARKMODE_BUTTON_BACKGROUND_COLOUR_ALT = "#6b6b69",
            
                            DARKMODE_RED = "#912707",
                            DARKMODE_ORANGE = "#ab7a09",
                            DARKMODE_YELLOW = "#ab9b09",
                            DARKMODE_GREEN = "#37ab09",
                            DARKMODE_BLUE = "#097dab";



        public static string BackgroundColour()//Function to call if the user selected dark mode
        {
            if (DarkMode())
            {
                return DARKMODE_BACKGROUND;//sets the background to the assigned colour for darkmode
            }

            return LIGHTMODE_BACKGROUND;//return lightmode 
        }

        public static string CorrectGuessBackgroundColour()//function for if user guesses correct letter
        {
                    if (DarkMode())//If darkmode change colour
                    {
                        return DARKMODE_CORRECT_GUESS_BACKGROUND;
                    }

            return LIGHTMODE_CORRECT_GUESS_BACKGROUND;
        }

        public static string IncorrectGuessBackGroundColour()//function for if user guesses incorrect letter
        {
            if (DarkMode())
            {
                return DARKMODE_INCORRECT_GUESS_BACKGROUND;
            }

            return LIGHTMODE_INCORRECT_GUESS_BACKGROUND;
        }

        public static string MisplacedLetterBackgroundColour()//function for if user guesses correct letter just in wrong place
        {
                    if (DarkMode())
                    {
                        return DARKMODE_MISPLACED_GUESS_BACKGROUND;
                    }

            return LIGHTMODE_MISPLACED_GUESS_BACKGROUND;
        }

        public static string ButtonBackgroundColour(int type)//function for button background colour
        {

            switch (type)//switch for each case selection
            {
                case PRI_TYPE:
                    if (DarkMode())
                    {
                        return DARKMODE_BUTTON_BACKGROUND_COLOUR_PRIMARY;
                    }

                    return LIGHTMODE_BUTTON_BACKGROUND_COLOUR_PRIMARY;
                case SEC_TYPE:
                    if (DarkMode())
                    {
                        return DARKMODE_BUTTON_BACKGROUND_COLOUR_SECONDARY;
                    }

                    return LIGHTMODE_BUTTON_BACKGROUND_COLOUR_SECONDARY;

                case ALT_TYPE:
                    if (DarkMode())
                    {
                        return DARKMODE_BUTTON_BACKGROUND_COLOUR_ALT;
                    }

                    return LIGHTMODE_BUTTON_BACKGROUND_COLOUR_ALT;
            }

            if (DarkMode())
            {
                return DARKMODE_BUTTON_BACKGROUND_COLOUR_PRIMARY;
            }

            return LIGHTMODE_BUTTON_BACKGROUND_COLOUR_PRIMARY;

        }


        public static bool DarkMode()
        {
            return Preferences.Default.Get("dark_mode", false);
        }

        public static int FontSize()
        {
            return Preferences.Default.Get("font_size", 20);
        }

        public static bool HintEnabled()
        {
            return Preferences.Default.Get("show_hints", false);

        }

        public static String GetColour(String colour)
        {
            switch(colour.ToLower())
            {
                case "red":
                    if (DarkMode())
                    {
                        return DARKMODE_RED;
                    }

                    return LIGHTMODE_RED;
                case "orange":
                    if (DarkMode())
                    {
                        return DARKMODE_ORANGE;
                    }

                    return LIGHTMODE_ORANGE;

                case "yellow":
                    if (DarkMode())
                    {
                        return DARKMODE_YELLOW;
                    }

                    return LIGHTMODE_YELLOW;
            }
            return "#FFFFFFF";
        }

       
    }

    
}


using UnityEngine;

public class Hacker : MonoBehaviour
{
    //Game Configuration data
    string[] level1Passwords = { "class", "bus", "books", "desk", "team" };
    string[] level2Passwords = { "jail", "police", "arrest", "crime", "handcuffs" };
    string[] level3Passwords = { "seize", "criminal", "investigation", "bureau", "federal" };

    //game state
    int level;
    string pw;
    enum Screen { MainMenu, Password, Win};
    Screen currentScreen;
    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    void ShowMainMenu()                                  
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Hello Player");
        Terminal.WriteLine(" What do you want to hack into today?");
        Terminal.WriteLine(" Press 1 if its the School (Easy Difficulty)");
        Terminal.WriteLine(" Press 2 if its the local Police Department (Medium Difficulty)");
        Terminal.WriteLine(" Press 3 if your Feeling lucky enough  to hack the FBI! (Hard Difficulty)");
        Terminal.WriteLine(" Enter Your Selection : ");
    }

    void OnUserInput(string input)     
    {
        if (input == "menu")             
        {
            ShowMainMenu();
            currentScreen = Screen.MainMenu;
        }
        else if (input == "exit" || input == "close" || input == "quit")
        {
            Terminal.WriteLine("If on web, close the tab:-)");
            Application.Quit();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        bool validNumber;
        int newLevel;
        validNumber = int.TryParse(input, out newLevel);
        bool isValidLevel = (validNumber && newLevel >= 1 && newLevel <= 3);
        if (isValidLevel)
        {
            level = newLevel;
            AskforPassword();
        }
        else if (input == "4")
        {
            Terminal.WriteLine(" Jesus Christ! its jason bourne ");
            Terminal.WriteLine(" type menu again to select a level");
        }
        else
        {
            Terminal.WriteLine("Type a valid command");
        }
    }

    void AskforPassword()            //function to respond to the user after the selection of level.
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Type 'menu' to choose another level");
        Terminal.WriteLine("Enter the password,hint: " + pw.Anagram());
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                int index1 = Random.Range(0, level1Passwords.Length);
                pw = level1Passwords[index1];
                break;
            case 2:
                int index2 = Random.Range(0, level2Passwords.Length);
                pw = level2Passwords[index2];
                break;
            case 3:
                int index3 = Random.Range(0, level3Passwords.Length);
                pw = level3Passwords[index3];
                break;
            default:
                Debug.LogError("Invalid Level Number");
                break;
        }
    }

    void CheckPassword (string input)
    {
        if (input == pw)
        {
            DisplayWinScreen();
        }
        else
        {
            AskforPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Access Granted! Correst Password");
                Terminal.WriteLine(@"
     /\     /\
    /  \___/  \
   (  / \ / \  )
    \  = . =  /

 ('')         ('')
 "
              );
                break;
            case 2:
                Terminal.WriteLine("The Password you've entered is correct!");
                Terminal.WriteLine(@"
   _________
  /         \
 /  0        \_____________________
(  __cell___  _____ _____   ___ - _|
 \  _ 3235_  /    -=    -===  =| |
  \_________/                  |=|

"
                    );
                break;
            case 3:
                Terminal.WriteLine(@"
   ,   /\   ,
  / '-'  '-' \
 |    FBI     |
 \    .--.    /
  |  ( 13 )  |
  \   '--'   /
   '--.  .--'
       \/
"
                    );
                break;
            default:
                Debug.LogError("invalid Level Reached");
                break; 
        }
        Terminal.WriteLine("Type menu to play another level!");

    }
}

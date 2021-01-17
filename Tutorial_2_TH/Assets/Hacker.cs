using UnityEngine;


public class Hacker : MonoBehaviour
{
     const string menuHint = "You may type menu at any time.";
     string[] level1Passwords ={"books","aisle","shelf","password","font","borrow"};
     string[] level2Passwords = {"prisoner","handcuffs","holster","uniform","arrest" };
     string[] level3Passwords = { "telescope","astronaut","meteorite","lightyear","spaceship","interstellar","neptune"};
    
     int level;
     string password;
     enum Screen { MainMenu, Password, Win };
     Screen currentScreen;
    void Start()
    {
        ShowMainMenu();
    }
    void ShowMainMenu()
    {
        password = null;
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 Local Library");
        Terminal.WriteLine("Press 2 Police Station");
        Terminal.WriteLine("Press 3 NASA");
        Terminal.WriteLine("Enter your selection:");
    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (input == "quit" || input == "exit" || input == "close")
        {
            Application.Quit();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen==Screen.Password)
        {
            CheckPassword(input);
        }
       
    }

    void RunMainMenu(string input)
    {
        bool isValidNumber = (input == "1" || input == "2" || input=="3");
        if (isValidNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else if (input == "007")
        {
            Terminal.WriteLine("Please select a level Mr Bond!");
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level");
            Terminal.WriteLine(menuHint);
        }
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine(menuHint);
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[Random.Range(0,level1Passwords.Length)];
                break;;
            case 2:
                password = level2Passwords[Random.Range(0,level2Passwords.Length)];
                break;
            case 3: 
                password = level3Passwords[Random.Range(0,level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
        Terminal.WriteLine("Enter your password: Hint: "+password.Anagram());   
    }

    void CheckPassword(string input)
    {
        if (input==password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowRewardLevel();
        Terminal.WriteLine(menuHint);
    }
    void ShowRewardLevel()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    __________
   /       / /
  /       / /
 / _____ / /
(_______( /
                ");
                break;
            case 2:
                Terminal.WriteLine("Have a pistol...");
                Terminal.WriteLine(@"

,--^----------,----,---,-------^--,
 | ||||||   `----'     |          O
 `+---------------------^---------|
   `\_,-------, __________________|
     / XX /`|  /
    / XX /  `\/
   / XX /\___(
  / XX /
 (____(
 `---'           
                ");
                break;
            case 3:
                Terminal.WriteLine("NASA");
                Terminal.WriteLine(@"

 _ __   __ _ ___  __ _ 
| '_ \ / _` / __|/ _` |
| | | | (_| \__ \ (_| |
|_| |_|\__,_|___/\__,_|
                ");
                break;
            default:
                Debug.LogError("invalid level reached");
                break;
        }
    }
}

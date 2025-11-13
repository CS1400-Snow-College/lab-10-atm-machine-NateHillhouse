//List<(string name, int pin, int balance, bool locked)> information = new List<(string name, int pin, int balance, bool locked)>();

using System.ComponentModel;

Console.Clear();

(string? name, Info? place) username = (null, null);
List<Info> information = ReadFile("bank.txt");
username = GetUsername();
int tries = 0; int pin;
if (!username.place.Locked)
{
    do
    {
        if (tries != 0)
        {
            Console.Clear();
            Console.WriteLine("Please try again. ");
        }
        pin = ReadPIN();
        if (username.place.Pin != pin) tries++;
    }
    while (tries < 3 && username.place.Pin != pin);

    if (!(tries < 3)){
        Console.Clear();
        Console.WriteLine("Your account has been locked due to too many incorrect attempts. Please contact the bank to unlock it. ");
        username.place.Locked = true;
    }
    else DisplayOptions();
}
else Console.WriteLine("Your account has been locked due to too many incorrect attempts. Please contact the bank to unlock it. ");

WriteFile("bank.txt", information);
/*
Write
TO 
FILE

Must write all changed info, including if its been locked.
*/



void DisplayOptions()
{
    Console.WriteLine("What would you like to do?");
    string[] options = new string[] { "Check Balance", "Withdraw", "Deposit", "Display last 5 transactions", "Quick Withdraw $40", "Quick Withdraw $100", "End current session" };
    foreach (string item in options) Console.WriteLine(item);
    Console.WriteLine();
    bool success = false;
    int input = 0;
    while (!success) success = Int32.TryParse(Console.ReadLine(), out input);

    switch (input)
    {
        case 1:
            Checkbalance();
            Loop();
            break;
        case 2:
            Withdraw();
            Loop();
            break;
        case 3:
            Deposit();
            Loop();
            break;
        case 4:
            ViewTransactions();
            Loop();
            break;
        case 5:
            Withdraw40();
            Loop();
            break;
        case 6:
            Withdraw100();
            Loop();
            break;
        case 7:
            EndSession();
            break;
        default:
            Loop();
            break;

    }


    void Loop()
    {
        Console.ReadKey(true);
        Console.Clear();
        DisplayOptions();
    }
}


static int ReadPIN()
{
    int pin = 0; bool succesful = false;
    while (!succesful)
    {
        Console.Write("What is your PIN? ");
        string? pinstring = Console.ReadLine();
        succesful = Int32.TryParse(pinstring, out pin);
        Console.WriteLine();
        Console.Clear();
        if (succesful == false) Console.WriteLine("Please enter a number. ");
    }
    return pin;
}

(string? name, Info? place) GetUsername()
{
    while (username.name == null) 
    {
        Console.Write("What is your username? ");
        username.name = Console.ReadLine();

        bool found = false;
        foreach (Info item in information)
        {
            if (item.Username == username.name)
            {
                found = true;
                username.place = item;
                break;
            }
        }
        if (!found)
        {
            Console.Clear();
            Console.WriteLine($"The username \"{username}\" was not found. Please try again.");
            username = (null, null);
        }
    }
    return username;
}

static List<Info> ReadFile(string location)
{
    List<Info> information = new List<Info>();
    string[] lines = File.ReadAllLines(location);
    for (int item = 0; item < lines.Length; item++)
    {
        string[] splitline = lines[item].Split(',');

        information.Add(new Info { Username = splitline[0], Pin = Convert.ToInt32(splitline[1]), Balance = Convert.ToInt32(splitline[2]), Locked = Convert.ToBoolean(splitline[3]) });
    }
    return information;
}

static void WriteFile(string location, List<Info> information)
{
    string[] writeinfo = new string[information.Count];
    for (int item = 0; item < information.Count; item++)
    {
        writeinfo[item] = $"{information[item].Username}, {information[item].Pin}, {information[item].Balance}, {information[item].Locked}";
    }
    File.WriteAllLines(location, writeinfo);
}

void Checkbalance()
{
    Console.Clear();
    Console.WriteLine($"Your balance is: {username.place.Balance}");
}
void Withdraw()
{
    int withdraw = 0;
    bool success = false;
    while (!success)
    {
        Console.Clear();
        Console.Write("How much would you like to withdraw? $");
        success = Int32.TryParse(Console.ReadLine(), out withdraw);
    }
    Console.Clear();
    if ((username.place.Balance -= withdraw) >= 0)
    {
        username.place.Balance -= withdraw;
        Console.WriteLine($"${withdraw} withdrawn successfully!");
    }
    else Console.WriteLine("You do not have enough funds to complete this action. ");
    Console.WriteLine($"Your balance is: {username.place.Balance}");
}
void Deposit()
{
    int deposit = 0;
    bool success = false;
    while (!success)
    {
        Console.Clear();
        Console.Write("How much would you like to Deposit? $");
        success = Int32.TryParse(Console.ReadLine(), out deposit);
    }
    Console.Clear();
    username.place.Balance += deposit;
    Console.WriteLine($"${deposit} deposited successfully!");
    Console.WriteLine($"Your balance is: {username.place.Balance}");
}
void ViewTransactions()
{

}
void Withdraw40()
{

}
void Withdraw100()
{

}
void EndSession()
{
    Console.WriteLine("Thank you for coming! ");
}

class Info
{
    public string? Username { get; set; }
    public int Pin { get; set; }
    public int Balance { get; set; }
    public bool Locked { get; set; }
}



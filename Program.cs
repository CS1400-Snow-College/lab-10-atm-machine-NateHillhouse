//List<(string name, int pin, int balance, bool locked)> information = new List<(string name, int pin, int balance, bool locked)>();
using System.Net.NetworkInformation;

Console.Clear();

(string? name, Info? place) username = (null, null);
List<Info> information = ReadFile("bank.txt");
username = GetUsername();
int tries = 0; int pin;
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

if (tries < 3) Console.WriteLine("success");
else Console.WriteLine("Failed");





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

class Info
{
    public string? Username { get; set; }
    public int Pin { get; set; }
    public int Balance { get; set; }
    public bool Locked { get; set; }
}



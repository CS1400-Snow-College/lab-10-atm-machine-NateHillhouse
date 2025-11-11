//List<(string name, int pin, int balance, bool locked)> information = new List<(string name, int pin, int balance, bool locked)>();
Console.Clear();

string? username = null;
List<Info> information = ReadFile("bank.txt");
while (username == null) username = GetUsername();


string? GetUsername()
{
    Console.Write("What is your username? ");
    string? username = Console.ReadLine();

    bool found = false;
    foreach (Info item in information)
    {
        if (item.Username == username)
        {
            found = true;
            break;
        }
    }
    if (!found)
    {
        Console.Clear();
        Console.WriteLine($"The username \"{username}\" was not found. Please try again.");
        username = null;
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



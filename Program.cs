List<(string name, int pin, int balance, bool locked)> information = new List<(string name, int pin, int balance, bool locked)>();
information = ReadFile("bank.txt", information);








static List<(string, int, int, bool)> ReadFile(string location, List<(string name, int pin, int balance, bool locked)> information)
{
    string[] lines = File.ReadAllLines(location);
    for (int item = 0; item < lines.Length; item++)
    {
        string[] splitline = lines[item].Split(',');
        information.Add((name: splitline[0], pin: Convert.ToInt32(splitline[1]), balance: Convert.ToInt32(splitline[2]), locked: Convert.ToBoolean(splitline[3])));
    }
    foreach ((string, int, int, bool) x in information) Console.WriteLine(x);
    return information;
}
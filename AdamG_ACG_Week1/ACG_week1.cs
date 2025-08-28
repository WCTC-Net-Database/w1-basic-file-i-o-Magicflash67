using System;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Linq;

// THis program is built with mono
namespace ACG_Week1
{
    internal class Program
    {
        static void Main(string[] args)
        
        {
            var UserInput = 0;
            while (true)
            {
                while (true)
                {
                    Console.WriteLine("1. Display Charaters");
                    System.Console.WriteLine("2. Add Charater");
                    System.Console.WriteLine("3. Level Up Charater");
                    System.Console.WriteLine("4. Exit");
                    Console.Write(">");
                    try
                    {
                        UserInput = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        System.Console.WriteLine("Please try again");
                    }

                }
                System.Console.WriteLine($"You picked: {UserInput}");

                var filePath = "ThatCoolCsv.csv";
                switch (UserInput)
                {
                    case 4:
                        Environment.Exit(0);
                        break;
                    case 3:
                        
                        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HasHeaderRecord = false
                        };
                        Console.WriteLine("Please type the Name of the Person you would like to Level up:");
                        Console.Write("> ");
                        var choice = Console.ReadLine();

                        var lines = File.ReadAllLines(filePath).ToList();

                        // 4. Parse CSV in memory, find the matching record, update it
                        using (var reader = new StringReader(string.Join(Environment.NewLine, lines)))
                        using (var csv    = new CsvReader(reader, config))
                        {
                            int rowIndex = 0;
                            bool found   = false;

                            while (csv.Read())
                            {
                                var name    = csv.GetField(0);
                                var @class  = csv.GetField(1);
                                var level   = int.Parse(csv.GetField(2));
                                var hp      = csv.GetField(3);
                                var items = csv.GetField(4);
                                if (string.Equals(name, choice, StringComparison.OrdinalIgnoreCase))
                                {
                                    found = true;
                                    level++;
                                    lines[rowIndex] = string.Join(",",
                                        name,
                                        @class,
                                        level,
                                        hp,
                                        items
                                    );

                                    Console.WriteLine($"Found {name}, updated level to {level}.");
                                    break;
                                }
                                rowIndex++;
                            }

                            if (!found)
                                Console.WriteLine("Sorry, no one was found with that name.");
                        }
                     File.WriteAllLines(filePath, lines);

                     break;

                    case 2:
                        System.Console.WriteLine("Making a New charater\n Please eneter a name: ");
                        System.Console.Write(">");
                        var Name = Console.ReadLine();
                        System.Console.WriteLine("Please Enter A class");
                        System.Console.Write(">");
                        var Class = Console.ReadLine();
                        System.Console.WriteLine("Please Enter A Charater Level");
                        System.Console.Write(">");
                        int levels = 0;
                        while (true)
                        {
                            try
                            {
                                 levels = Convert.ToInt32(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                System.Console.WriteLine("Please Enter a charater number within the intger range");
                                System.Console.Write(">");
                            }
                        }
                        System.Console.WriteLine("Please Enter HP");
                        System.Console.Write(">");
                        int HP = 0;
                        while (true)
                        {
                            try
                            {
                                HP = Convert.ToInt32(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                System.Console.WriteLine("Please Enter a charater health points number within the intger range");
                                System.Console.Write(">");
                            }
                        }
                        System.Console.WriteLine("What items do they have; Type EXIT to stop");
                        var ItemsSele = "";
                        var ItemSaid = "";
                        var CD = "";
                        var IsFirst = true;
                        while (true)
                        {
                            System.Console.Write("\n>");
                            ItemSaid = Console.ReadLine();
                            if (ItemSaid == "EXIT")
                            {
                                break;
                            }
                            else
                            {
                                if (IsFirst)
                                {
                                    ItemsSele = $"{ItemSaid}";
                                    IsFirst != IsFirst;
                                }
                                else
                                {
                                    ItemsSele = $"{ItemsSele}|{ItemSaid}";
                                }
                            }
                        }

                        CD = $"{Name},{Class},{HP},{levels},{ItemsSele}";
                        File.AppendAllText(filePath, Environment.NewLine + CD);
                        break;
                    case 1:
                        Console.WriteLine("Characters");

                        var configs = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HasHeaderRecord = false
                        };

                        using (var reader = new StreamReader(filePath))
                        using (var csv = new CsvReader(reader, configs))
                        {
                            while (csv.Read())
                            {
                                var name = csv.GetField(0);
                                var classes = csv.GetField(1);
                                var level = csv.GetField(2);
                                var hp = csv.GetField(3);
                                var items = csv.GetField(4);

                                Console.WriteLine($@"
Name: {name}
Class: {classes}
Level: {level}
HP: {hp}");

            var equipment = items.Split("|");
            foreach (var equip in equipment)
            {
                Console.WriteLine($"\t{equip}");
            }
        }
    }
    break;


                    default:
                        System.Console.WriteLine("Seems like you didnt Enter a correct value displayed; Please try again");
                        break;

                }
            }
            
        }
    }
}
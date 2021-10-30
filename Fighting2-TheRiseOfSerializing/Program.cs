using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Fighting2_TheRiseOfSerializing
{
    class Program
    {
        static void Main(string[] args)
        {
            //PlayerCollection deserializedPlayerData = JsonSerializer.Deserialize<PlayerCollection>(File.ReadAllText(@"..\data.json"));
            GreetUser();



            Console.ReadLine();


            // foreach (Player a in deserializedPlayerData.players)
            // {
            //     System.Console.WriteLine(a.name);
            // }
        }

        static void GreetUser()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to my fighter simulator!");
            Console.WriteLine("In this game you are primarly going to play as a character and defeat enemies.");
            Console.WriteLine("After defeating them you will get gold and enter the shop, buy stuff and get better.");
            Console.WriteLine("The game in itself is an infinite scaling battle arena with lots of stuff to explore!");
            Console.WriteLine();

            UpdateLog();

            Menu(true);
        }

        static void UpdateLog()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            string[] log = File.ReadAllLines(@"..\updateLog.txt");
            Console.WriteLine("Update log:");
            foreach (string update in log)
            {
                Console.WriteLine(update);
            }

            Console.ForegroundColor = ConsoleColor.White;

            Menu(true);
        }

        static void Menu(bool intro)
        {
            if (intro)
            {
                Console.WriteLine("\nPress any key to continue!");
                Console.ReadKey();
            }
            Console.Clear();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("Play the game (P), see update log (L) or inspect game (G)?");

                string answer = Console.ReadLine();

                if (answer.ToLower() == "p")
                {
                    //StartGame();
                }
                else if (answer.ToLower() == "l")
                {
                    Console.Clear();
                    UpdateLog();
                }
                else if (answer.ToLower() == "g")
                {
                    Console.Clear();
                    InspectGame();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Can not process \"{answer}\" into an answer!");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }
            }
        }

        static bool YesOrNo(string prompt)
        {
            Console.WriteLine(prompt);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("(y/n)");
            Console.ForegroundColor = ConsoleColor.White;

            string answer = Console.ReadLine();
            if (answer.ToLower().Contains("y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void InspectGame()
        {
            string rawData = File.ReadAllText(@"..\data.json");
            PlayerCollection deserializedPlayerData = JsonSerializer.Deserialize<PlayerCollection>(rawData);
            Shop deserializedShopItems = JsonSerializer.Deserialize<Shop>(rawData);
            DefensiveCollection deserializedShopDefensive = JsonSerializer.Deserialize<DefensiveCollection>(rawData);
            OffensiveCollection deserializedShopOffensive = JsonSerializer.Deserialize<OffensiveCollection>(rawData);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Welcome to the game inspector tool!");
                Console.WriteLine("Would you like to see characters (C) or items (I)?");
                Console.WriteLine("(Return to menu with M)");


                string answer = Console.ReadLine();
                if (answer.ToLower() == "c")
                {
                    Console.Clear();

                    foreach (Player p in deserializedPlayerData.players)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Character {p.name}.");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Base HP: {p.hp}");
                        Console.WriteLine($"Base attack: {p.attack}");
                        Console.WriteLine($"Base hit chance: {p.acc}%");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine($"Description: {p.description}.");
                        Console.WriteLine();
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (answer.ToLower() == "i")
                {
                    var ch = ConsoleKey.B;
                    int page = 0;
                    Console.Clear();

                    do
                    {
                        switch (page)
                        {
                            case 0:
                                Console.WriteLine("Items:");
                                foreach (ItemCollection collection in deserializedShopItems.itemCollection)
                                {
                                    foreach (Item i in collection.items)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine($"Item name: {i.name}.");
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine($"Accuracy modifier: {i.accModifier}%");
                                        Console.WriteLine($"Attack increase: {i.attackIncrease}");
                                        Console.WriteLine($"Max HP increase: {i.maxHPmodifier}");
                                        Console.WriteLine($"Cost: {i.cost}");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.WriteLine($"Description: {i.description}.");
                                        Console.WriteLine();
                                    }
                                }
                                break;
                                // case 1:
                                //     Console.WriteLine("Defensive consumables:");
                                //     foreach (Item i in deserializedItems.items)
                                //     {
                                //         Console.ForegroundColor = ConsoleColor.Cyan;
                                //         Console.WriteLine($"Item name: {i.name}.");
                                //         Console.ForegroundColor = ConsoleColor.DarkYellow;
                                //         Console.WriteLine($"Accuracy modifier: {i.accModifier}%");
                                //         Console.WriteLine($"Attack increase: {i.attackIncrease}");
                                //         Console.WriteLine($"Max HP increase: {i.maxHPmodifier}");
                                //         Console.WriteLine($"Cost: {i.cost}");
                                //         Console.ForegroundColor = ConsoleColor.Gray;
                                //         Console.WriteLine($"Description: {i.description}.");
                                //         Console.WriteLine();
                                //     }
                                //     break;
                                // case 2:
                                //     Console.WriteLine("Offensive consumables:");
                                //     foreach (Item i in deserializedItems.items)
                                //     {
                                //         Console.ForegroundColor = ConsoleColor.Cyan;
                                //         Console.WriteLine($"Item name: {i.name}.");
                                //         Console.ForegroundColor = ConsoleColor.DarkYellow;
                                //         Console.WriteLine($"Accuracy modifier: {i.accModifier}%");
                                //         Console.WriteLine($"Attack increase: {i.attackIncrease}");
                                //         Console.WriteLine($"Max HP increase: {i.maxHPmodifier}");
                                //         Console.WriteLine($"Cost: {i.cost}");
                                //         Console.ForegroundColor = ConsoleColor.Gray;
                                //         Console.WriteLine($"Description: {i.description}.");
                                //         Console.WriteLine();
                                //     }
                                //     break;
                        }

                        Console.WriteLine();
                        Console.WriteLine($"You are on page {page + 1}/3");
                        Console.WriteLine("Press the arrow keys to switch viewed page or enter to exit");
                        ch = Console.ReadKey(true).Key;
                        if (ch == ConsoleKey.LeftArrow)
                        {
                            if (page == 0)
                            {
                                page = 2;
                            }
                            else
                            {
                                page--;
                            }
                        }
                        else if (ch == ConsoleKey.RightArrow)
                        {
                            if (page == 2)
                            {
                                page = 0;
                            }
                            else
                            {
                                page++;
                            }
                        }
                    } while (ch != ConsoleKey.Enter);
                }
                else if (answer.ToLower() == "m")
                {
                    Menu(false);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Can not process \"{answer}\" into an answer!");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }
            }
        }

    }












    public class PlayerCollection
    {
        public Player[] players { get; set; }
    }
    public class Player
    {
        public string name { get; set; }
        public string hp { get; set; }
        public string attack { get; set; }
        public string acc { get; set; }
        public string description { get; set; }
    }

    public class Shop
    {
        public ItemCollection[] itemCollection { get; set; }
        public OffensiveCollection[] defensiveCollection { get; set; }
        public DefensiveCollection[] offensiveCollection { get; set; }
    }
    public class ItemCollection
    {
        public Item[] items { get; set; }
    }
    public class Item
    {
        public string name { get; set; }
        public string attackIncrease { get; set; }
        public string cost { get; set; }
        public string accModifier { get; set; }
        public string maxHPmodifier { get; set; }
        public string description { get; set; }
    }

    public class OffensiveCollection
    {
        public Offensive[] offensive { get; set; }
    }
    public class Offensive
    {
        public string name { get; set; }
        public string attackIncrease { get; set; }
        public string cost { get; set; }
        public string accModifier { get; set; }
        public string maxHPmodifier { get; set; }
        public string description { get; set; }
    }
    public class DefensiveCollection
    {
        public Defensive[] defensive { get; set; }
    }
    public class Defensive
    {
        public string name { get; set; }
        public string attackIncrease { get; set; }
        public string cost { get; set; }
        public string accModifier { get; set; }
        public string maxHPmodifier { get; set; }
        public string description { get; set; }
    }

}

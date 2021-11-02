using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Fighting2_TheRiseOfSerializing
{
    class Program
    {
        static void Main(string[] args)
        {
            Round(1);

            GreetUser();

            Console.ReadLine();


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
                    Console.Clear();
                    StartGame();
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
            ShopItems deserializedShopItems = JsonSerializer.Deserialize<ShopItems>(rawData);
            ShopOffensive deserializedShopOffensive = JsonSerializer.Deserialize<ShopOffensive>(rawData);
            ShopDefensive deserializedShopDefensive = JsonSerializer.Deserialize<ShopDefensive>(rawData);

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
                    var ch = ConsoleKey.A;
                    int page = 0;
                    Console.Clear();

                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        switch (page)
                        {
                            case 0:
                                Console.WriteLine("Items:");
                                Console.WriteLine();
                                foreach (Item i in deserializedShopItems.items.items)
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
                                    Console.WriteLine("\n");
                                }
                                break;
                            case 1:
                                Console.WriteLine("Defensive consumables:");
                                foreach (Defensive c in deserializedShopDefensive.defensive.defensive)
                                {
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine($"Item name: {c.name}.");
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine($"HP increase: {c.baseHP}");
                                    Console.WriteLine($"HP increase percent : {c.percentHP}%");
                                    Console.WriteLine($"Cost: {c.cost}");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.WriteLine($"Description: {c.description}.");
                                    Console.WriteLine();
                                }
                                break;
                            case 2:
                                Console.WriteLine("Offensive consumables:");
                                foreach (Offensive c in deserializedShopOffensive.offensive.offensive)
                                {
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine($"Item name: {c.name}.");
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine($"Attack base increase: {c.baseAttack}");
                                    Console.WriteLine($"Attack % increase: {c.percentAttack}%");
                                    Console.WriteLine($"Cost: {c.cost}");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.WriteLine($"Description: {c.description}.");
                                    Console.WriteLine();
                                }
                                break;
                        }

                        Console.WriteLine();
                        Console.WriteLine($"You are on page {page + 1}/3");
                        Console.WriteLine("Press the arrow keys to switch viewed page or Enter to exit");
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
                        Console.Clear();
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

        static void StartGame()
        {
            var ch = ConsoleKey.B;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to the arena!");
            Console.WriteLine("Here you play for rewards and are able to progress through the story!");
            Console.WriteLine("If you feel unfamiliar with the game, please consider the game inspector tool (I).");
            Console.WriteLine("Otherwise, load a journey (L) or start a new journey (N)!");

            ch = Console.ReadKey(false).Key;

            while (true)
            {
                if (ch == ConsoleKey.I)
                {
                    Console.Clear();
                    InspectGame();
                }
                else if (ch == ConsoleKey.L)
                {
                    //LoadGame();
                }
                else if (ch == ConsoleKey.N)
                {
                    Console.Clear();
                    NewGame();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Can not process \"{ch}\" into a response!");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }
            }
        }

        static void NewGame()
        {
            Player chosenCharacter = new Player();
            bool alive = true;
            int level = 1;
            int experience = 0;
            int difficulty = 1;
            List<Item> inventory = new List<Item>();
            List<Object> consumables = new List<Object>();

            string rawData = File.ReadAllText(@"..\data.json");
            PlayerCollection deserializedPlayerData = JsonSerializer.Deserialize<PlayerCollection>(rawData);
            ShopItems deserializedShopItems = JsonSerializer.Deserialize<ShopItems>(rawData);
            ShopOffensive deserializedShopOffensive = JsonSerializer.Deserialize<ShopOffensive>(rawData);
            ShopDefensive deserializedShopDefensive = JsonSerializer.Deserialize<ShopDefensive>(rawData);

            Player[] players = deserializedPlayerData.players;
            ItemCollection shopItems = deserializedShopItems.items;
            OffensiveCollection offensiveItems = deserializedShopOffensive.offensive;
            DefensiveCollection defensiveItems = deserializedShopDefensive.defensive;

            Console.WriteLine("Welcome to a new game, a new beginning!");
            Console.WriteLine("Your next step to creating your journey is picking a character:");

            var ch = ConsoleKey.B;
            int playerNumber = 1;
            bool chosing = true;

            while (chosing)
            {
                foreach (Player p in players)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Character {p.name}. ({playerNumber})");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Base HP: {p.hp}");
                    Console.WriteLine($"Base attack: {p.attack}");
                    Console.WriteLine($"Base hit chance: {p.acc}%");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"Description: {p.description}.");
                    Console.WriteLine();
                    playerNumber++;
                }
                Console.WriteLine("Press the number corresponding to the character you want.");
                ch = Console.ReadKey(false).Key;
                Console.Clear();
                switch (ch)
                {
                    case ConsoleKey.D1:
                        chosenCharacter = players[0];
                        chosing = false;
                        break;
                    case ConsoleKey.D2:
                        chosenCharacter = players[1];
                        chosing = false;
                        break;
                    case ConsoleKey.D3:
                        chosenCharacter = players[2];
                        chosing = false;
                        break;
                    case ConsoleKey.D4:
                        chosenCharacter = players[3];
                        chosing = false;
                        break;
                    case ConsoleKey.D5:
                        chosenCharacter = players[4];
                        chosing = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"Can not process \"{ch}\" into a response!");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine($"You have now chosen {chosenCharacter.name}!");
            Console.WriteLine("Let the journey begin!");

            bool loop = true;
            while (loop)
            {
                loop = Round(difficulty);
                difficulty++;
            }
        }

        static bool Round(int difficulty)
        {
            Random generator = new Random();
            Enemy[] enemies;
            List<string> arenas = new List<string> {
                "by an old house",
                "outside the village",
                "on the way to next quest",
                "on the way to the master",
                "in a dungeon",
                "encountering a boss"
            };

            int spread = generator.Next(1, difficulty);
            enemies = new Enemy[spread];

            for (int i = 0; i < spread; i++)
            {
                enemies[i] = new Enemy();
                enemies[i].baseAttack /= generator.Next(1, spread);
                enemies[i].baseHP /= generator.Next(1, spread);
            }

            Console.WriteLine($"You encounter strange activity while {arenas[difficulty]}.");
            Console.WriteLine("You realise it is hostile, you now need to fight for survival!");

            foreach (Enemy e in enemies)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{e.name} has spawned.");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Attack dmg: {e.baseAttack}");
                Console.WriteLine($"HP: {e.baseHP}");
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("The fight will now begin! You are the first to strike.");



            return true;
        }



    }


    public class Enemy
    {
        public static Random generator = new Random();
        public static string[] names = File.ReadAllLines(@"..\enemyNames.txt");
        public string name = names[generator.Next(0, names.Length)];
        public int baseHP = 75;
        public int baseAttack = 10;
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

    public class ShopItems
    {
        [JsonPropertyName("shop")]
        public ItemCollection items { get; set; }
    }
    public class ItemCollection
    {
        public List<Item> items { get; set; }
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

    public class ShopOffensive
    {
        [JsonPropertyName("shop")]
        public OffensiveCollection offensive { get; set; }
    }

    public class OffensiveCollection
    {
        public List<Offensive> offensive { get; set; }
    }
    public class Offensive
    {
        public string name { get; set; }
        public string baseAttack { get; set; }
        public string percentAttack { get; set; }
        public string cost { get; set; }
        public string description { get; set; }
    }

    public class ShopDefensive
    {
        [JsonPropertyName("shop")]
        public DefensiveCollection defensive { get; set; }
    }
    public class DefensiveCollection
    {
        public List<Defensive> defensive { get; set; }
    }
    public class Defensive
    {
        public string name { get; set; }
        public string baseHP { get; set; }
        public string percentHP { get; set; }
        public string cost { get; set; }
        public string description { get; set; }
    }
}

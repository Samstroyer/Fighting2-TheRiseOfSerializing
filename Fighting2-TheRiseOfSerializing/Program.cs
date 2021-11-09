using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fighting2_TheRiseOfSerializing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hey, started!");
            test();
            NewGame();
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

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Welcome to the arena!");
                Console.WriteLine("Here you play for rewards and are able to progress through the story!");
                Console.WriteLine("If you feel unfamiliar with the game, please consider the game inspector tool (I).");
                Console.WriteLine("Otherwise, load a journey (L) or start a new journey (N) or exit to menu (M)!");

                ch = Console.ReadKey(false).Key;

                if (ch == ConsoleKey.I)
                {
                    Console.Clear();
                    InspectGame();
                }
                else if (ch == ConsoleKey.L)
                {
                    Console.Clear();
                    bool succesfulLoad = false;
                    succesfulLoad = LoadGame();
                    if (!succesfulLoad)
                    {
                        Console.WriteLine("Unable to load saved data!");
                        Console.WriteLine("A new game was started! (Old file at: \"old-save.txt\" in dir)");
                        NewGame();
                    }
                }
                else if (ch == ConsoleKey.N)
                {
                    Console.Clear();
                    NewGame();
                }
                else if (ch == ConsoleKey.M)
                {
                    Console.Clear();
                    Menu(true);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Can not process \"{ch}\" into a response!");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }

                //PROBABLY NEED SMT HERE TO NOT DO WEIRD LOOPING!
            }
        }

        static void NewGame()
        {
            Player chosenCharacter = new Player(); //Man får error om det inte är en new player, för ofc så tror inte visual att while(chosenCharacter!=Player) liknande loop ger en annan sak än player till slut...
            int difficulty = 1;
            List<Item> inventory = new List<Item>();
            List<Object> consumables = new List<Object>();

            string rawData = File.ReadAllText(@"..\data.json");
            PlayerCollection deserializedPlayerData = JsonSerializer.Deserialize<PlayerCollection>(rawData);
            ShopItems deserializedShopItems = JsonSerializer.Deserialize<ShopItems>(rawData);
            ShopOffensive deserializedShopOffensive = JsonSerializer.Deserialize<ShopOffensive>(rawData);
            ShopDefensive deserializedShopDefensive = JsonSerializer.Deserialize<ShopDefensive>(rawData);

            Player[] players = deserializedPlayerData.players;

            //DEBUG for when Test() be finished!
            //Player f = Consumables(players[0]);

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
                        Console.WriteLine($"Can not process \"{ch}\" into a character number!");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine($"You have now chosen {chosenCharacter.name}!");
            Console.WriteLine("Press any key to begin!");
            Console.ReadLine();
            Console.Clear();

            chosenCharacter = Game(chosenCharacter, difficulty);
            //Summarize
        }

        static Player Game(Player c, int diff)
        {
            bool alive = true;
            var ch = ConsoleKey.B;

            while (alive)
            {
                c = Round(diff, c);
                switch (int.Parse(c.hp))
                {
                    case < 0:
                        alive = false;
                        break;
                    default:
                        alive = true;
                        break;
                }
                if (alive)
                {
                    diff++;
                    Console.WriteLine("You completed a round! Do you want to continue (C)?");
                    ch = Console.ReadKey(false).Key;
                    if (ch != ConsoleKey.C)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("You died!");
                }
            }

            return c;
        }

        static Player Round(int difficulty, Player character)
        {
            Random generator = new Random();
            Enemy[] enemies;
            List<string> arenas = new List<string> {
                "by an old house",
                "outside the village",
                "on the way to next quest",
                "on the way to the master",
                "in a dungeon",
                "to a boss"
            };

            int spread = generator.Next(1, difficulty);
            enemies = new Enemy[spread];

            for (int i = 0; i < spread; i++)
            {
                enemies[i] = new Enemy();
                enemies[i].baseHP /= generator.Next(1, spread);
            }

            Console.WriteLine($"You encounter strange activity while walking {arenas[difficulty]}.");
            Console.WriteLine("You realise it is hostile activity, you now need to fight for survival!");

            foreach (Enemy e in enemies)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{e.name} has spawned.");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Attack dmg: {e.baseAttack}");
                Console.WriteLine($"HP: {e.baseHP}");
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"{character.name} current HP: {character.hp} / {character.maxHp}");
            Console.WriteLine();
            Console.WriteLine("If you are low on HP or need extra damage from items, open consumables.");
            Console.WriteLine("Press any key to start the battle or press (C) to open consumables?");
            var ch = Console.ReadKey(false).Key;
            if (ch == ConsoleKey.C)
            {
                //character = Consumables(character, enemies);
            }

            Console.Clear();

            Console.WriteLine("The fight will now begin! You are the first to strike.");
            Console.WriteLine("\n");

            //enemy setups
            bool enemiesAlive = true;

            //character - (player) - setups
            string[] minMaxDamageString = character.attack.Split("-");
            int[] minMaxDamageInt = new int[2];
            for (int i = 0; i < minMaxDamageString.Length; i++)
            {
                minMaxDamageInt[i] = int.Parse(minMaxDamageString[i]);
            }

            int substage = 1;

            while (enemiesAlive && int.Parse(character.hp) > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Round {substage} starts!");

                foreach (Enemy e in enemies)
                {
                    int chanceToHit = generator.Next(0, 101);
                    if (chanceToHit <= int.Parse(character.acc))
                    {
                        int characterDamageRoll = generator.Next(minMaxDamageInt[0], minMaxDamageInt[1] + 1);
                        e.baseHP -= characterDamageRoll;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{e.name} took {characterDamageRoll} damage! New HP: {e.baseHP}.");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{character.name} missed the attack on {e.name}!");
                    }
                }

                foreach (Enemy e in enemies)
                {
                    if (e.baseHP > 0)
                    {
                        int chanceToHit = generator.Next(0, 101);
                        if (chanceToHit <= e.acc)
                        {
                            string[] minMaxEnemyDamage = e.baseAttack.Split("-");
                            int enemyDamage = generator.Next(int.Parse(minMaxEnemyDamage[0]), int.Parse(minMaxEnemyDamage[1]));
                            int newCharacterHP = int.Parse(character.hp) - enemyDamage;
                            character.hp = newCharacterHP.ToString();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"{e.name} dealt {enemyDamage} damage to {character.name}! New HP: {character.hp}.");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{e.name} missed the attack on {character.name}!");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine($"Enemy {e.name} can not attack! {e.name} is dead!");
                    }
                }

                //Quick check if any enemy is alive - assuming everyone is dead. But if a hp is greater than zero it turns true
                bool anyEnemyAlive = false;
                foreach (Enemy e in enemies)
                {
                    if (e.baseHP > 0)
                    {
                        anyEnemyAlive = true;
                    }
                }
                enemiesAlive = anyEnemyAlive;

                Console.WriteLine();
                substage++;
            }

            if (enemiesAlive)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You lost the battle!");
                Console.WriteLine("Now you sadly have to try again, press any key to continue.");
                Console.ReadLine();
                return character;
            }
            else
            {
                int gold = 0;
                foreach (Enemy e in enemies)
                {
                    string[] goldMinMaxString = e.carriedGold.Split("-");
                    int[] goldMinMaxInt = new int[2];
                    for (int i = 0; i < goldMinMaxInt.Length; i++)
                    {
                        goldMinMaxInt[i] = int.Parse(goldMinMaxString[i]);
                    }

                    gold += generator.Next(goldMinMaxInt[0], goldMinMaxInt[1]);
                }
                character.money = gold.ToString();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"You won the battle and got {gold} gold!");
                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();
                SaveGame(character, difficulty);
                return character;
            }
        }

        static void SaveGame(Player c, int d)
        {
            string[] loadedData = File.ReadAllLines(@"..\save.txt");
            List<string> save = new List<string>();

            save[0] = $"{c.name},{c.hp},{c.money},{d}";
            save[1] = loadedData[1];
            save[2] = loadedData[2];
            save[3] = loadedData[3];

            File.WriteAllLines("save.json", save);
        }
        static void SaveGame(Player c, int d, string dataLine1, string dataLine2, string dataLine3)
        {
            string[] loadedData = File.ReadAllLines(@"..\save.txt");
            List<string> save = new List<string>();

            save[0] = $"{c.name},{c.hp},{c.money},{d}";
            save[1] = dataLine1;
            save[2] = dataLine2;
            save[3] = dataLine3;

            File.WriteAllLines("save.json", save);
        }

        static Player Shop(Player c)
        {

            return c;
        }

        static bool LoadGame()
        {
            bool success = false;
            return success;
        }

        //static {Player, Enemy[]} UseConsumables(Player c, Enemy[] e, int difficulty)
        static void test()
        {
            string[] loadedData = File.ReadAllLines(@"..\save.txt");
            string rawData = File.ReadAllText(@"..\data.json");

            string allDefensiveItemsCopy = loadedData[1];
            List<int> usedDefensiveItemsID = new List<int>();

            string allOffensiveItemsCopy = loadedData[2];
            List<int> usedOffensiveItemsID = new List<int>();

            string allItemItemsCopy = loadedData[3];
            List<int> usedItemItemsID = new List<int>();

            ShopItems deserializedShopItems = JsonSerializer.Deserialize<ShopItems>(rawData); ItemCollection shopItems = deserializedShopItems.items;
            ShopOffensive deserializedShopOffensive = JsonSerializer.Deserialize<ShopOffensive>(rawData); OffensiveCollection offensiveItems = deserializedShopOffensive.offensive;
            ShopDefensive deserializedShopDefensive = JsonSerializer.Deserialize<ShopDefensive>(rawData); DefensiveCollection defensiveItems = deserializedShopDefensive.defensive;

            //Jag kan göra det lite sketchy och räkna människo värden. Det är lite enklare då allt matchar men det e lite sketch som sagt. vill helst ha page starta från 0 till 2 istället för 1 till 3
            int page = 1;
            bool done = false;
            var tempChar = ConsoleKey.B;
            int consumableTotalDamage = 0;
            int consumableTotalHeal = 0;

            while (!done)
            {
                switch (page)
                {




                    case 1:
                        if (!allDefensiveItemsCopy.Contains("-") || !allDefensiveItemsCopy.Contains(""))
                        {
                            tempChar = ConsoleKey.B;
                            while (tempChar != ConsoleKey.LeftArrow && tempChar != ConsoleKey.RightArrow && tempChar != ConsoleKey.E)
                            {
                                Console.WriteLine("Your defensive items are:\n");

                                int index = 0;
                                foreach (char c in allDefensiveItemsCopy)
                                {
                                    Console.WriteLine($"Selection #: {index}");
                                    Console.WriteLine($"Name: {defensiveItems.defensive[int.Parse(c.ToString())].name}");
                                    Console.WriteLine($"Description: {defensiveItems.defensive[int.Parse(c.ToString())].description}");
                                    Console.WriteLine();
                                    index++;
                                }

                                Console.WriteLine("These are your defensive consumables!");
                                Console.WriteLine("Press the number of the item you want to use that it is paired to!");
                                Console.WriteLine($"Press arrow keys to switch page, {page}/3");
                                Console.WriteLine("Press (E) to exit (also finish this interface).");
                                tempChar = Console.ReadKey().Key;

                                if (tempChar == ConsoleKey.RightArrow)
                                {
                                    if (page == 3)
                                    {
                                        page = 1;
                                    }
                                    else
                                    {
                                        page++;
                                    }
                                }
                                else if (tempChar == ConsoleKey.LeftArrow)
                                {
                                    if (page == 1)
                                    {
                                        page = 3;
                                    }
                                    else
                                    {
                                        page--;
                                    }
                                }
                                else if (tempChar == ConsoleKey.E)
                                {
                                    Console.WriteLine("You have now saved and exited!");
                                    Thread.Sleep(1000);
                                    done = true;
                                }

                                //eftersom nummer i consoleKey har ett D framför sig..., så måste man ta bort D från nummret (då nummret kommer efteråt, och jag gissar att man har skrivit ett nummer om det är 2+ karaktärer)
                                int tempInt;
                                string stringOfTempChar = tempChar.ToString();
                                if (stringOfTempChar.Length == 2)
                                {
                                    char actualNumber = stringOfTempChar.ToCharArray()[1];
                                    bool success = int.TryParse(actualNumber.ToString(), out tempInt);
                                    if (success)
                                    {
                                        if (tempInt >= 0 && tempInt < allDefensiveItemsCopy.Length)
                                        {
                                            usedDefensiveItemsID.Add(int.Parse(allDefensiveItemsCopy.ToCharArray()[tempInt].ToString()));
                                            allDefensiveItemsCopy = allDefensiveItemsCopy.Remove(tempInt, 1);
                                        }
                                    }
                                }

                                Console.Clear();
                            }
                        }
                        else
                        {
                            loadedData[1] = "-";
                            Console.Clear();
                            Console.WriteLine("You have nothing here! It is empty or corrupted.");
                            Console.WriteLine($"Press arrow keys to switch page, {page}/3");
                            Console.WriteLine("Press (E) to exit (also finish this interface).");
                            tempChar = Console.ReadKey().Key;

                            if (tempChar == ConsoleKey.RightArrow)
                            {
                                if (page == 3)
                                {
                                    page = 1;
                                }
                                else
                                {
                                    page++;
                                }
                            }
                            else if (tempChar == ConsoleKey.LeftArrow)
                            {
                                if (page == 1)
                                {
                                    page = 3;
                                }
                                else
                                {
                                    page--;
                                }
                            }
                            else if (tempChar == ConsoleKey.E)
                            {
                                Console.WriteLine("You have now saved and exited!");
                                Thread.Sleep(1000);
                                done = true;
                            }
                        }
                        break;






                    case 2:
                        tempChar = ConsoleKey.B;
                        if (!allOffensiveItemsCopy.Contains("-") || allOffensiveItemsCopy.Length < 1)
                        {
                            while (tempChar != ConsoleKey.LeftArrow && tempChar != ConsoleKey.RightArrow && tempChar != ConsoleKey.E)
                            {
                                Console.WriteLine("Your offensive items are:\n");

                                int index = 0;
                                foreach (char c in allOffensiveItemsCopy)
                                {
                                    Console.WriteLine($"Selection #: {index}");
                                    Console.WriteLine($"Name: {offensiveItems.offensive[int.Parse(c.ToString())].name}");
                                    Console.WriteLine($"Description: {offensiveItems.offensive[int.Parse(c.ToString())].description}");
                                    Console.WriteLine();
                                    index++;
                                }

                                Console.WriteLine("These are your offensive consumables!");
                                Console.WriteLine("Press the number of the item you want to use that it is paired to!");
                                Console.WriteLine($"Press arrow keys to switch page, {page}/3");
                                Console.WriteLine("Press (E) to exit (also finish this interface).");
                                tempChar = Console.ReadKey().Key;

                                if (tempChar == ConsoleKey.RightArrow)
                                {
                                    if (page == 3)
                                    {
                                        page = 1;
                                    }
                                    else
                                    {
                                        page++;
                                    }
                                }
                                else if (tempChar == ConsoleKey.LeftArrow)
                                {
                                    if (page == 1)
                                    {
                                        page = 3;
                                    }
                                    else
                                    {
                                        page--;
                                    }
                                }
                                else if (tempChar == ConsoleKey.E)
                                {
                                    Console.WriteLine("You have now saved and exited!");
                                    Thread.Sleep(1000);
                                    done = true;
                                }

                                //eftersom nummer i consoleKey har ett D framför sig..., så måste man ta bort D från nummret (då nummret kommer efteråt, och jag gissar att man har skrivit ett nummer om det är 2+ karaktärer)
                                int tempInt;
                                string stringOfTempChar = tempChar.ToString();
                                if (stringOfTempChar.Length == 2)
                                {
                                    char actualNumber = stringOfTempChar.ToCharArray()[1];
                                    bool success = int.TryParse(actualNumber.ToString(), out tempInt);
                                    if (success)
                                    {
                                        if (tempInt >= 0 && tempInt < allOffensiveItemsCopy.Length)
                                        {
                                            usedOffensiveItemsID.Add(int.Parse(allOffensiveItemsCopy.ToCharArray()[tempInt].ToString()));
                                            allOffensiveItemsCopy = allOffensiveItemsCopy.Remove(tempInt, 1);
                                        }
                                    }
                                }

                                Console.Clear();
                            }
                        }
                        else
                        {
                            loadedData[1] = "-";
                            Console.WriteLine("You have nothing here! It is empty or corrupted.");
                            Console.WriteLine($"Press arrow keys to switch page, {page}/3");
                            Console.WriteLine("Press (E) to exit (also finish this interface).");
                            tempChar = Console.ReadKey().Key;

                            if (tempChar == ConsoleKey.RightArrow)
                            {
                                if (page == 3)
                                {
                                    page = 1;
                                }
                                else
                                {
                                    page++;
                                }
                            }
                            else if (tempChar == ConsoleKey.LeftArrow)
                            {
                                if (page == 1)
                                {
                                    page = 3;
                                }
                                else
                                {
                                    page--;
                                }
                            }
                            else if (tempChar == ConsoleKey.E)
                            {
                                Console.WriteLine("You have now saved and exited!");
                                Thread.Sleep(1000);
                                done = true;
                            }
                        }
                        break;


                    case 3:
                        if (!loadedData[3].Contains("-") || loadedData[3].Length < 1)
                        {
                            while (!(tempChar == ConsoleKey.LeftArrow || tempChar == ConsoleKey.RightArrow || tempChar == ConsoleKey.E))
                            {
                                Console.WriteLine("Your items are:\n");

                                int index = 0;
                                foreach (char c in allItemItemsCopy)
                                {
                                    Item currentItem = shopItems.items[int.Parse(c.ToString())];
                                    Console.WriteLine($"Item #: {index}");
                                    Console.WriteLine($"Name: {currentItem.name}");
                                    Console.WriteLine($"Accuracy modifier: {currentItem.accModifier}%");
                                    Console.WriteLine($"Attack modifier: {currentItem.attackIncrease}");
                                    Console.WriteLine($"Max HP modifier: {currentItem.maxHPmodifier}");
                                    Console.WriteLine($"Description: {currentItem.description}");
                                    Console.WriteLine();
                                    index++;
                                }

                                Console.WriteLine("These are all your items!");
                                Console.WriteLine($"Press arrow keys to switch page, {page}/3");
                                Console.WriteLine("Press (E) to exit (also finish this interface).");
                                tempChar = Console.ReadKey().Key;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The data is corrupted or you dont have any.");
                            Console.WriteLine($"Press arrow keys to switch page, {page}/3");
                            Console.WriteLine("Press (E) to exit (also finish this interface).");
                            tempChar = Console.ReadKey().Key;
                        }

                        if (tempChar == ConsoleKey.RightArrow)
                        {
                            if (page == 3)
                            {
                                page = 1;
                            }
                            else
                            {
                                page++;
                            }
                        }
                        else if (tempChar == ConsoleKey.LeftArrow)
                        {
                            if (page == 1)
                            {
                                page = 3;
                            }
                            else
                            {
                                page--;
                            }
                        }
                        else if (tempChar == ConsoleKey.E)
                        {
                            Console.WriteLine("You have now saved and exited!");
                            Thread.Sleep(1000);
                            done = true;
                        }
                        break;
                }

                foreach (int i in usedDefensiveItemsID)  //item apply damage and heal 
                {
                    consumableTotalHeal *= int.Parse(defensiveItems.defensive[i].percentHP);
                    consumableTotalHeal += int.Parse(defensiveItems.defensive[i].baseHP);
                }
                foreach (int i in usedOffensiveItemsID)  //item apply damage and heal 
                {
                    consumableTotalDamage *= int.Parse(offensiveItems.offensive[i].percentAttack);
                    consumableTotalHeal += int.Parse(offensiveItems.offensive[i].baseAttack);
                }


                //SaveGame(c, difficulty, allOffensiveItemsCopy, allDefensiveItemsCopy, allItemItemsCopy);

                Console.Clear();
                Console.WriteLine("Your total healing is {consumableTotalHeal}, and your instant damage will be {consumableTotalDamage}");
            }
            //return c;
        }

    }



    public class Enemy
    {
        static Random generator = new Random();
        static string[] names = File.ReadAllLines(@"..\enemyNames.txt");
        public string name = names[generator.Next(0, names.Length)];
        public int baseHP = 75;
        public int acc = 90;
        public string baseAttack = "5-9";
        public string carriedGold = "0-10";
    }

    public class PlayerCollection
    {
        public Player[] players { get; set; }
    }
    public class Player
    {
        public string name { get; set; }
        public string hp { get; set; }
        public string maxHp { get; set; }
        public string attack { get; set; }
        public string acc { get; set; }
        public string money { get; set; }
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

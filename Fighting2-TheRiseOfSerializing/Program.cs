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
            GreetUser();
            NewGame();
            Console.WriteLine("Thanks for playing! The game has been saved!");
            Console.ReadLine();
        }

        static void GreetUser()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Clear();
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
                        ch = Console.ReadKey(false).Key;
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
                        Console.WriteLine("A new game was started! (Old file data at: \"old-save.txt\" in dir)");
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
            int playerNumber;
            bool chosing = true;

            while (chosing)
            {
                playerNumber = 1;
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
            SaveGame(chosenCharacter, difficulty);
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

            Console.WriteLine($"{character.name} current HP: {character.hp} / {GetMaxHP(character)}");
            Console.WriteLine();
            Console.WriteLine("If you are low on HP or need extra damage from items, open consumables.");
            Console.WriteLine("Press any key to start the battle or press (C) to open consumables?");
            var ch = Console.ReadKey(false).Key;
            Console.Clear();
            if (ch == ConsoleKey.C)
            {
                (Player returnedPlayer, Enemy[] returnedEnemies) c = UseConsumables(character, enemies, difficulty);
                character = c.returnedPlayer;
                enemies = c.returnedEnemies;
                Console.WriteLine("Press any key to continue!");
                Console.ReadKey();
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
                Console.WriteLine("Press any key to continue or (S) to go to the shop!");
                ch = Console.ReadKey(true).Key;
                if (ch == ConsoleKey.S)
                {
                    character = Shop(character);
                }
                SaveGame(character, difficulty);
                return character;
            }
        }

        static void AppendToSave((string dID, bool dBought) d, (string oID, bool oBought) o, (string iID, bool iBought) i)
        {
            string[] loadedData = File.ReadAllLines(@"..\save.txt");
            string[] save = new string[4];

            save[0] = loadedData[0];

            if (d.dBought)
            {
                if (loadedData[1].Length < 1 || loadedData[1].Contains("-"))
                {
                    save[1] = d.dID;
                }
                else
                {
                    save[1] = loadedData[1] + d.dID;
                }
            }
            else
            {
                save[1] = loadedData[1];
            }

            if (o.oBought)
            {
                if (loadedData[2].Length < 1 || loadedData[2].Contains("-"))
                {
                    save[2] = o.oID;
                }
                else
                {
                    save[2] = loadedData[2] + o.oID;
                }
            }
            else
            {
                save[2] = loadedData[2];
            }

            if (i.iBought)
            {
                if (loadedData[3].Length < 1 || loadedData[3].Contains("-"))
                {
                    save[3] = o.oID;
                }
                else
                {
                    save[3] = loadedData[3] + i.iID;
                }
            }
            else
            {
                save[3] = loadedData[3];
            }

            File.WriteAllLines(@"..\save.txt", save);
        }
        static void SaveGame(Player c, int d)
        {
            string[] loadedData = File.ReadAllLines(@"..\save.txt");
            string[] save = new string[4];

            save[0] = $"{c.name},{c.hp},{c.money},{d}";
            save[1] = loadedData[1];
            save[2] = loadedData[2];
            save[3] = loadedData[3];

            File.WriteAllLines(@"..\save.txt", save);
        }
        static void SaveGame(Player c, int d, string dataLine1, string dataLine2, string dataLine3)
        {
            string[] loadedData = File.ReadAllLines(@"..\save.txt");
            string[] save = new string[4];

            save[0] = $"{c.name},{c.hp},{c.money},{d}";
            save[1] = dataLine1;
            save[2] = dataLine2;
            save[3] = dataLine3;

            File.WriteAllLines(@"..\save.txt", save);
        }

        static Player Shop(Player c)
        {
            string[] saveData = File.ReadAllLines(@"..\save.txt");
            string rawData = File.ReadAllText(@"..\data.json");
            ShopItems deserializedShopItems = JsonSerializer.Deserialize<ShopItems>(rawData); ItemCollection shopItems = deserializedShopItems.items;
            ShopOffensive deserializedShopOffensive = JsonSerializer.Deserialize<ShopOffensive>(rawData); OffensiveCollection offensiveItems = deserializedShopOffensive.offensive;
            ShopDefensive deserializedShopDefensive = JsonSerializer.Deserialize<ShopDefensive>(rawData); DefensiveCollection defensiveItems = deserializedShopDefensive.defensive;

            (bool d, bool o, bool i) availabilityOfCollection;
            (Defensive, Offensive, Item) collection;
            (bool d, bool o, bool i) boughtFromCollection = (false, false, false);

            Random gen = new Random();


            (int d, int o, int i) itemID = (gen.Next(0, defensiveItems.defensive.Count), gen.Next(0, offensiveItems.offensive.Count), gen.Next(0, shopItems.items.Count));
            collection.Item1 = defensiveItems.defensive[itemID.d];
            collection.Item2 = offensiveItems.offensive[itemID.o];
            collection.Item3 = shopItems.items[itemID.i];

            //I UseConsumable() menyn så har jag en readkey, det gör enklare för mig OCH gör så att man inte kan ha mer än 0-9 items. 
            //Så för att göra så att man inte fixar mer items än man kan ha så finns det en check om man kan köpa eller inte!
            //Det är också < istället för <= för att man inte ska kunna gå över 10 items. *trial and error correction*
            if (saveData[1].Length < 10)
            {
                availabilityOfCollection.d = true;
            }
            else
            {
                availabilityOfCollection.d = false;
            }
            if (saveData[2].Length < 10)
            {
                availabilityOfCollection.o = true;
            }
            else
            {
                availabilityOfCollection.o = false;
            }
            //Kan stacka items oändligt!
            availabilityOfCollection.i = true;

            bool done = false;
            var ch = ConsoleKey.B;
            while (!done && !(boughtFromCollection.d && boughtFromCollection.o && boughtFromCollection.i))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("This is the shop menu! Here you can use your money to gain advantage over oponents.");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"You have {c.money} money.");
                Console.WriteLine($"You have {saveData[1].Length}/10 defensive item(s).");
                Console.WriteLine($"You have {saveData[1].Length}/10 offensive item(s).");
                Console.WriteLine($"You have {saveData[3].Length} item(s).");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Press (E) to exit or you can buy:");
                Console.ForegroundColor = ConsoleColor.Yellow;

                if ((int.Parse(c.money) >= int.Parse(collection.Item1.cost) || boughtFromCollection.d))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (availabilityOfCollection.d && !boughtFromCollection.d)
                {
                    Console.WriteLine($"\nDefensive: {collection.Item1.name} for {collection.Item1.cost} (D)");
                    Console.WriteLine(collection.Item1.description);
                }
                else if (boughtFromCollection.d)
                {

                    Console.WriteLine("\nBought");
                }
                else
                {
                    Console.WriteLine("\nUnable to purchase at this moment!");
                }

                if ((int.Parse(c.money) >= int.Parse(collection.Item2.cost)) || boughtFromCollection.o)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (availabilityOfCollection.o && !boughtFromCollection.o)
                {
                    Console.WriteLine($"\nOffensive: {collection.Item2.name} for {collection.Item2.cost} (O)");
                    Console.WriteLine(collection.Item2.description);
                }
                else if (boughtFromCollection.o)
                {
                    Console.WriteLine("\nBought");
                }
                else
                {
                    Console.WriteLine("\nUnable to purchase at this moment!");
                }

                if ((int.Parse(c.money) >= int.Parse(collection.Item3.cost)) || boughtFromCollection.i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (boughtFromCollection.i)
                {
                    Console.WriteLine("\nBought");
                }
                else
                {
                    Console.WriteLine($"\nItem: {collection.Item3.name} for {collection.Item3.cost} (I)");
                    Console.WriteLine(collection.Item3.description);
                }

                ch = Console.ReadKey(false).Key;
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                if (ch == ConsoleKey.D && int.Parse(c.money) >= int.Parse(collection.Item1.cost) && !boughtFromCollection.d)
                {
                    boughtFromCollection.d = true;
                    c.money = (int.Parse(c.money) - int.Parse(collection.Item1.cost)).ToString();
                    Console.WriteLine($"You have bought {collection.Item1.name}! Your new balance is {c.money}");
                    Thread.Sleep(1000);
                }
                else if (ch == ConsoleKey.O && int.Parse(c.money) >= int.Parse(collection.Item2.cost) && !boughtFromCollection.o)
                {
                    boughtFromCollection.o = true;
                    c.money = (int.Parse(c.money) - int.Parse(collection.Item2.cost)).ToString();
                    Console.WriteLine($"You have bought {collection.Item2.name}! Your new balance is {c.money}");
                    Thread.Sleep(1000);
                }
                else if (ch == ConsoleKey.I && int.Parse(c.money) >= int.Parse(collection.Item3.cost) && !boughtFromCollection.i)
                {
                    boughtFromCollection.i = true;
                    c.money = (int.Parse(c.money) - int.Parse(collection.Item3.cost)).ToString();
                    Console.WriteLine($"You have bought {collection.Item3.name}! Your new balance is {c.money}");
                    Thread.Sleep(1000);
                }

                if (ch == ConsoleKey.E)
                {
                    break;
                }
            }

            if (boughtFromCollection.d && boughtFromCollection.o && boughtFromCollection.i)
            {
                Console.WriteLine("You have bought all items in the store!");
                Console.WriteLine($"Current money: {c.money}");
                Console.WriteLine("Press any key to continue!");
                AppendToSave((itemID.d.ToString(), boughtFromCollection.d), (itemID.o.ToString(), boughtFromCollection.o), (itemID.i.ToString(), boughtFromCollection.i));
                Console.ReadKey();
                return c;
            }
            else
            {
                Console.WriteLine("You have exited the store!");
                Console.WriteLine($"Current money: {c.money}");
                Console.WriteLine("Press any key to continue!");
                AppendToSave((itemID.d.ToString(), boughtFromCollection.d), (itemID.o.ToString(), boughtFromCollection.o), (itemID.i.ToString(), boughtFromCollection.i));
                Console.ReadKey(true);
                return c;
            }
        }

        static bool LoadGame()
        {
            string[] data = File.ReadAllLines(@"..\save.txt");
            bool success = true;
            if (data[0].Contains("-") || data[0].Length < 1)
            {
                success = false;
                Console.WriteLine("The save is corrupted or can't load! Starting new game.");
                NewGame();
            }
            else
            {
                for (int i = 1; i < data.Length; i++)
                {
                    if (data[i].Length < 1 || data[i].Contains("-"))
                    {
                        data[i] = "-";
                    }
                }
                Player p = new Player();
                string[] playerInfo = data[0].Split(",");
                p.name = playerInfo[0];

                string rawData = File.ReadAllText(@"..\data.json");
                PlayerCollection deserializedPlayerData = JsonSerializer.Deserialize<PlayerCollection>(rawData);

                foreach (Player pPresets in deserializedPlayerData.players)
                {
                    if (pPresets.name == p.name)
                    {
                        p = pPresets;
                        break;
                    }
                }

                p.hp = playerInfo[1];
                p.money = playerInfo[2];
                int difficulty = int.Parse(playerInfo[3]);

                Game(p, difficulty);
            }

            return success;
        }

        static (Player, Enemy[]) UseConsumables(Player p, Enemy[] e, int difficulty)
        {
            string[] loadedData = File.ReadAllLines(@"..\save.txt");
            string rawData = File.ReadAllText(@"..\data.json");

            string allDefensiveItemsCopy = loadedData[1];
            List<int> usedDefensiveItemsID = new List<int>();

            string allOffensiveItemsCopy = loadedData[2];
            List<int> usedOffensiveItemsID = new List<int>();

            string allItemItemsCopy = loadedData[3];

            ShopItems deserializedShopItems = JsonSerializer.Deserialize<ShopItems>(rawData); ItemCollection shopItems = deserializedShopItems.items;
            ShopOffensive deserializedShopOffensive = JsonSerializer.Deserialize<ShopOffensive>(rawData); OffensiveCollection offensiveItems = deserializedShopOffensive.offensive;
            ShopDefensive deserializedShopDefensive = JsonSerializer.Deserialize<ShopDefensive>(rawData); DefensiveCollection defensiveItems = deserializedShopDefensive.defensive;


            //Jag kan göra det lite sketchy och räkna med direkta människo värden. Det fungerar då [1] är första raden vi använder  
            int page = 1;
            bool done = false;
            var tempChar = ConsoleKey.B;
            double consumableTotalDamage = 0;
            double consumableTotalHeal = 0;

            Console.Clear();
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
                        if (!allOffensiveItemsCopy.Contains("-") || allOffensiveItemsCopy.Length < 1)
                        {
                            tempChar = ConsoleKey.B;
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
                        if (!allItemItemsCopy.Contains("-") || allItemItemsCopy.Length < 1)
                        {
                            tempChar = ConsoleKey.B;
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
                Console.Clear();
            }

            foreach (int i in usedDefensiveItemsID)  //item apply damage and heal 
            {
                consumableTotalHeal += int.Parse(defensiveItems.defensive[i].baseHP);
                if (int.Parse(defensiveItems.defensive[i].percentHP) > 0)
                {
                    double percentage = double.Parse(defensiveItems.defensive[i].percentHP) / 100d;
                    consumableTotalHeal += (percentage) * int.Parse(GetMaxHP(p).ToString());
                }
            }
            p.hp = Math.Floor((consumableTotalHeal + int.Parse(p.hp))).ToString();
            int maxHP = GetMaxHP(p);
            if (int.Parse(p.hp) > maxHP)
            {
                p.hp = maxHP.ToString();
            }

            foreach (int i in usedOffensiveItemsID)  //item apply damage and heal 
            {
                consumableTotalDamage += int.Parse(offensiveItems.offensive[i].baseAttack);
                if (int.Parse(offensiveItems.offensive[i].percentAttack) > 0)
                {
                    consumableTotalDamage *= 1 + (int.Parse(offensiveItems.offensive[i].percentAttack) / 100);
                }
            }
            foreach (Enemy enemy in e)
            {
                enemy.baseHP -= Convert.ToInt32(consumableTotalDamage);
            }


            SaveGame(p, difficulty, allOffensiveItemsCopy, allDefensiveItemsCopy, allItemItemsCopy);

            Console.Clear();
            Console.WriteLine($"Your total healing is {Math.Floor(consumableTotalHeal)}\nYour instant damage to all enemies will be {Math.Floor(consumableTotalDamage)}");
            return (p, e);
        }

        static int GetMaxHP(Player p)
        {
            string rawData = File.ReadAllText(@"..\data.json");
            string items = File.ReadAllLines(@"..\save.txt")[3];
            PlayerCollection deserializedPlayerData = JsonSerializer.Deserialize<PlayerCollection>(rawData);
            ShopItems deserializedShopItems = JsonSerializer.Deserialize<ShopItems>(rawData);

            int baseHPForCharacter = 0;
            int maxHP;

            foreach (Player playerPresets in deserializedPlayerData.players)
            {
                if (playerPresets.name == p.name)
                {
                    baseHPForCharacter = int.Parse(playerPresets.hp);
                    break;
                }
            }

            //Det här gör så att ordningen som man får items i kommer ändra hur mycket HP man får i slutet. Men det kan vara en strategi som bara de bästa vet, de som vill speedruna det här!
            maxHP = baseHPForCharacter;
            foreach (char i in items)
            {
                if (!(i.ToString() == "-"))
                {
                    maxHP += int.Parse(deserializedShopItems.items.items[int.Parse(i.ToString())].maxHPmodifier);
                }
            }

            return maxHP;
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
        public string carriedGold = "5-40";
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

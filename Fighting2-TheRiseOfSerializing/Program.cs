using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Fighting2_TheRiseOfSerializing
{
    class Program
    {
        PlayerCollection deserializedPlayerData = JsonSerializer.Deserialize<PlayerCollection>(File.ReadAllText(@"..\data.json"));

        static void Main(string[] args)
        {
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
            Console.WriteLine("Here you can bet money on champions, play in a fight and upgrade your champion!");
            Console.WriteLine();

            UpdateLog();

            Menu();
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

            Menu();
        }

        static void Menu()
        {
            Console.WriteLine("\nPress any key to continue!");
            Console.ReadKey();
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
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Welcome to the game inspector tool!");
                Console.WriteLine("Would you like to see characters (C) or items (I)?");
                Console.WriteLine("(Return to menu with M)");


                string answer = Console.ReadLine();
                if (answer.ToLower() == "c")
                {

                }
                else if (answer.ToLower() == "i")
                {

                }
                else if (answer.ToLower() == "m")
                {
                    Menu();
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
        public string hp { get; }
        public string attack { get; }
        public string hitChance { get; }
    }
}

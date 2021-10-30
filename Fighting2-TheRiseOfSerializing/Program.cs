using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json;

namespace Fighting2_TheRiseOfSerializing
{
    class Program
    {
        static void Main(string[] args)
        {
            GreetUser();
            string rawPlayerData = File.ReadAllText(@"..\players.json");
            PlayerCollection deserializedPlayerData = JsonSerializer.Deserialize<PlayerCollection>(rawPlayerData);

            foreach (Player a in deserializedPlayerData.players)
            {
                System.Console.WriteLine(a.name);
            }
        }

        static void GreetUser()
        {
            Console.WriteLine("Welcome to my fighter simulator!");
            Console.WriteLine("Here you can bet money on champions, play in a fight and upgrade your champion!");
        }

    }

    public class PlayerDecoder
    {
        public Player[] Players { get; set; }
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

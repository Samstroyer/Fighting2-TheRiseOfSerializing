using System;
using System.IO;
using System.Text.Json;

namespace Fighting2_TheRiseOfSerializing
{
    class Program
    {
        static void Main(string[] args)
        {
            string rawPlayerData = File.ReadAllText(@"..\players.json");
            PlayerCollection deserializedPlayerData = JsonSerializer.Deserialize<PlayerCollection>(rawPlayerData);

            foreach (Player a in deserializedPlayerData.players)
            {
                System.Console.WriteLine(a.name);
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

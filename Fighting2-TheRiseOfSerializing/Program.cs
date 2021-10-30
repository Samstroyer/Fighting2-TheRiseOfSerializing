using System;
using System.IO;
<<<<<<< HEAD
using System.Text;
=======
>>>>>>> 78f1a9f0442c80ac224e12ae4d53e16691a7e980
using System.Text.Json;

namespace Fighting2_TheRiseOfSerializing
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            
            GreetUser();
            

            PlayerDecoder pDecoder = new PlayerDecoder();
=======
            string rawPlayerData = File.ReadAllText(@"..\players.json");
            PlayerCollection deserializedPlayerData = JsonSerializer.Deserialize<PlayerCollection>(rawPlayerData);

            foreach (Player a in deserializedPlayerData.players)
            {
                System.Console.WriteLine(a.name);
            }
>>>>>>> 78f1a9f0442c80ac224e12ae4d53e16691a7e980
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

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
            
            GreetUser();
            

            PlayerDecoder pDecoder = new PlayerDecoder();
        }

        
    }

    public class PlayerDecoder
    {
        public Player[] Players { get; set; }
    }

    public class Player
    {
        public string name { get; set; }
        public string hp { get; set; }
        public string attack { get; set; }
        public string hitssChance { get; set; }
    }
}

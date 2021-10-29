using System;

namespace Fighting2_TheRiseOfSerializing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

        }
    }

    public class Player
    {
        public string name { get; set; }
        public string hp { get; set; }
        public string attack { get; set; }
        public string hitssChance { get; set; }
    }
}

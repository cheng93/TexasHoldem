using System;

namespace TexasHoldem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("How many players (2-8) ? ");
            PokerLogic logic = new PokerLogic(int.Parse(Console.ReadLine()));
            logic.Run();
            Console.Read();
        }
    }
}
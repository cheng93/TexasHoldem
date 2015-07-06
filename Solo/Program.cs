using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem;

namespace Solo
{
    class Program
    {
        static void Main(string[] args)
        {
            bool control;
            do
            {
                Console.Clear();
                int opponents;
                do
                {
                    Console.Write("Number of opponents: ");
                } while (!int.TryParse(Console.ReadLine(), out opponents));
                Console.WriteLine();
                var hand = new Hand("Me", opponents);
                var boardHand = new Hand();
                Card card;

                for (int i = 1; i <= 2; i++)
                {
                    Console.WriteLine("Card {0}:", i);
                    card = GetCard();
                    hand.Add(card);
                }
                Console.WriteLine();

                Console.WriteLine("Flop:");
                for (int i = 0; i < 3; i++)
                {
                    card = GetCard();
                    hand.Add(card);
                    boardHand.Add(card);
                }
                hand = TwoPlusTwo.Evaluate(hand, boardHand);
                Console.WriteLine();
                PrintHandStrength(hand, opponents +1);

                Console.WriteLine("Turn:");
                card = GetCard();
                hand.Add(card);
                boardHand.Add(card);
                hand = TwoPlusTwo.Evaluate(hand, boardHand);
                Console.WriteLine();
                PrintHandStrength(hand, opponents + 1);

                Console.WriteLine("River:");
                card = GetCard();
                hand.Add(card);
                boardHand.Add(card);
                Console.WriteLine();
                hand = TwoPlusTwo.Evaluate(hand, boardHand);
                PrintHandStrength(hand, opponents + 1);

                Console.WriteLine("Do you want to play again? ");
                if (Console.ReadLine() == "y")
                    control = true;
                else
                    control = false;
            } while (control);
        }

        private static Card GetCard()
        {
            int value;
            char suit;

            do
            {
                Console.Write("Value: ");
            } while (!int.TryParse(Console.ReadLine(), out value));
            do
            {
                Console.Write("Suit: ");
            } while (!char.TryParse(Console.ReadLine(), out suit));
            Card card = new Card(0,0);
            switch (suit)
            {
                case 'c':
                    card = new Card((Value)(value - 2), Suit.Clubs);
                   1 break;
                case 'd':
                    card = new Card((Value)(value - 2), Suit.Diamonds);
                    break;
                case 'h':
                    card = new Card((Value)(value - 2), Suit.Hearts);
                    break;
                case 's':
                    card = new Card((Value)(value - 2), Suit.Spades);
                    break;
            }
            Console.WriteLine(card);
            return card;
        }

        private static void PrintHandStrength(Hand hand, int players)
        {
            double threshold = 2/((double)players + 1);
            Console.WriteLine("{0} HS: {1} EFS: {2} P(Win): {3} Threshold: {4}", hand.Name,
                hand.HandStrength.ToString("F"), hand.EffectiveHandStrength.ToString("F"),
                hand.WinningProbability.ToString("F"), threshold.ToString("F"));

            Console.WriteLine();
        }
    }
}

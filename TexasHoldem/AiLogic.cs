using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    public class AiLogic
    {
        private static ICollection<Hand> _hands;

        public static ICollection<Hand> Run(ICollection<Hand> hands, ICollection<Card> boardCards)
        {
            _hands = new List<Hand>();
            foreach (var hand in hands)
            {
                _hands.Add(hand);
            }
            var unfoldedHands = _hands.Where(hand => hand.Fold == false).ToList();
            var percentages = GetFoldRaisePercentages(unfoldedHands.Count());
            var raisedHands = unfoldedHands.Where(hand => hand.WinningProbability >= percentages[1]).ToList();
            
            if (raisedHands.Count > 0)
            {
                PrintRaisedHands(raisedHands);
                foreach (var hand in unfoldedHands.Where(hand => !raisedHands.Contains(hand)))
                {
                    if (hand.WinningProbability < percentages[1])
                    {
                        hand.Fold = true;
                    }
                }
                PrintFoldedHands(unfoldedHands.Where(hand => hand.Fold).ToList());
                Console.WriteLine();
            }

            return _hands;
        }

        private static double[] GetFoldRaisePercentages(double players)
        {
            return new [] {1/(players + 1), 2/(players + 1)};
        }

        private static void PrintRaisedHands(List<Hand> hands)
        {
            for (int i = 0; i < hands.Count; i++)
            {
                Console.Write("{0} {1}", (i == 0 ? "Raised hands:" : ","), hands.ElementAt(i).Name);
            }
            Console.WriteLine();
        }
        
        private static void PrintFoldedHands(List<Hand> hands)
        {
            for (int i = 0; i < hands.Count; i++)
            {
                Console.Write("{0} {1}", (i == 0 ? "Folded hands:" : ","), hands.ElementAt(i).Name);
            }
            if (hands.Count > 0)
                Console.WriteLine();
        }
    }
}
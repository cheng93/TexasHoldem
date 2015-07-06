using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    public class HandEvaluator
    {
        public static Hand Evaluate(Hand hand)
        {
            foreach (var evalHand in GetHandCombinations(hand))
            {
                var q = Shift(evalHand);
                if (Flush(evalHand))
                {
                    if (Array.Flushes[q] < hand.Rank)
                        hand.Rank = Array.Flushes[q];
                }
                else if (StraightOrHighHand(evalHand))
                {
                    if (Array.Unique5[q] < hand.Rank)
                        hand.Rank = Array.Unique5[q];
                }
                else
                {
                    q = PrimeMagic(evalHand);
                    var index = System.Array.IndexOf(Array.Products, q);
                    if (Array.Values[index] < hand.Rank)
                        hand.Rank = Array.Values[index];
                }
            }
            return hand;
        }

        public static Hand Evaluate(Hand hand, Hand boardCards)
        {
            hand = Evaluate(hand);
            hand.HandStrength = GetHandStrength(hand, boardCards);
            var potential = GetHandPotential(hand, boardCards);
            hand.EffectiveHandStrength = GetEffectiveHandStrength(hand, potential);

            return hand;
        }

        public static List<Hand> GetHandCombinations(Hand hand, int length = 5)
        {
            var output = new List<Hand>();
            var numbers = FillArray(length);
            bool control;
            do
            {
                var combinationHand = new Hand();
                for (int i = 0; i < length; i++)
                {
                    combinationHand.Add(hand.ElementAt(numbers[i]));
                }
                output.Add(combinationHand);

                int[] newNumbers = Algorithm.Combination(numbers, hand.Count, length);
                control = !newNumbers.SequenceEqual(numbers);
                numbers = newNumbers;
            } while (control);


            return output;
        }

        private static int[] FillArray(int size)
        {
            var output = new int[size];
            for (int i = 0; i < size; i++)
                output[i] = i;
            return output;
        }

        public static bool Flush(Hand hand)
        {
            var cardInts = ToInts(hand);
            var bit = 0xF000 & cardInts[0] & cardInts[1] & cardInts[2] & cardInts[3] & cardInts[4];
            return bit != 0;
        }

        public static bool StraightOrHighHand(Hand hand)
        {
            var q = Shift(hand);
            return Array.Unique5[q] != 0;
        }

        public static int Shift(Hand hand)
        {
            var cardInts = ToInts(hand);
            return (cardInts[0] | cardInts[1] | cardInts[2] | cardInts[3] | cardInts[4]) >> 16;
        }

        public static int PrimeMagic(Hand hand)
        {
            var cardInts = ToInts(hand);
            return (cardInts[0] & 0xFF) * (cardInts[1] & 0xFF) * (cardInts[2] & 0xFF) * (cardInts[3] & 0xFF) *
                   (cardInts[4] & 0xFF);
        }

        public static double GetHandStrength(Hand hand, Hand boardCards)
        {
            var array = TwoPlusTwo.HandStrength(hand, boardCards);
            var ahead = array[0];
            var tied = array[1];
            var behind = array[2];

            return (ahead + tied / 2) / (ahead + tied + behind);
        }

        public static double[] GetHandPotential(Hand hand, Hand boardCards)
        {
            var ahead = 0;
            var tied = 1;
            var behind = 2;

            var array = TwoPlusTwo.HandPotential(hand, boardCards);

            var positivePotential = (array[0][behind, ahead] + array[0][behind, tied]  /2 +
                                     array[0][tied, ahead]/2)/
                                    (array[1][0, behind] + array[1][0, tied]);

            if (double.IsNaN(positivePotential))
                positivePotential = 0;

            var negativePotential = (array[0][ahead, behind] + array[0][tied, behind] / 2 +
                                     array[0][ahead, tied] / 2) /
                                    (array[1][0, ahead] + array[1][0, tied]);

            if (double.IsNaN(negativePotential))
                negativePotential = 0;

            return new[] { positivePotential, negativePotential };
        }

        public static double GetEffectiveHandStrength(Hand hand, double[] potential)
        {
            return hand.HandStrength + (1 - hand.HandStrength) * potential[0];
        }

        public static double GetWinningProbability(Hand hand, double[] potential)
        {
            return hand.HandStrength*(1 - potential[1]) + (1 - hand.HandStrength)*potential[0];
        }
        public static Hand GetUnknownCards(params Hand[] hands)
        {
            var output = new Hand();
            var deck = new Deck();
            foreach (var card in deck.Cards)
            {
                if (hands.All(hand => !hand.Contains(card)))
                {
                    output.Add(card);
                }
            }


            return output;
        }

        private static int[] ToInts(Hand hand)
        {
            var output = new int[hand.Count];
            for (int i = 0; i < hand.Count; i++)
            {
                output[i] = Card.ToInt32(hand.ElementAt(i));
            }
            return output;
        }
    }
}
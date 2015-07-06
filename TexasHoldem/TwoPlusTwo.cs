using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TexasHoldem
{
    public class TwoPlusTwo
    {
        private static readonly int HAND_RANK_SIZE = 32487834;
        private static readonly string HAND_RANK_DATA_FILENAME = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"HandRanks.dat");
        private static int[] HR = new int[HAND_RANK_SIZE];
        private static readonly int tableSize = HAND_RANK_SIZE * 4;
        private static byte[] b = new byte[tableSize];

        public static string[] HAND_RANKS =
        {
            "BAD!!", "High Card", "Pair", "Two Pair", "Three of a Kind",
            "Straight", "Flush", "Full House", "Four of a Kind", "Straight Flush"
        };

        static TwoPlusTwo()
        {
            using (BufferedStream reader = new BufferedStream(new FileStream(HAND_RANK_DATA_FILENAME, FileMode.Open)))
            {
                reader.Read(b, 0, tableSize);
            }
            //for (int i = 0; i < HAND_RANK_SIZE; i++)
            //{
            //    HR[i] = BitConverter.ToInt32(b, i*4);
            //}
        }

        public static Hand Evaluate(Hand hand, Hand boardCards)
        {
            hand.Evaluate();
            if (hand.Fold == false)
            {
                hand.HandStrength = HandEvaluator.GetHandStrength(hand, boardCards);
                var potential = HandEvaluator.GetHandPotential(hand, boardCards);
                hand.EffectiveHandStrength = HandEvaluator.GetEffectiveHandStrength(hand, potential);
                hand.WinningProbability = HandEvaluator.GetWinningProbability(hand, potential);
            }

            return hand;
        }

        public static int EvaluateCards(int[] cards)
        {
            var p = 53;
            for (var i = 0; i < cards.Length; i++)
            {
                p = EvalCard(p + cards[i]);
            }

            if (cards.Length == 5 || cards.Length == 6)
            {
                p = EvalCard(p);
            }
            return p;
        }

        public static int EvaluateCards(Hand hand)
        {
            int[] cards = new int[hand.Count];
            for (int i = 0; i < hand.Count; i++)
                cards[i] = hand.ElementAt(i).Number;
            return EvaluateCards(cards);
        }

        public static int EvalCard(int card)
        {
            return BitConverter.ToInt32(b, card * 4);
        }

        public static double[] HandStrength(Hand hand, Hand boardCards)
        {
            var handNumbers = new int[7];
            hand.ToInts().CopyTo(handNumbers, 0);
            var boardNumbers = boardCards.ToInts();

            return HandStrength(handNumbers, boardNumbers);
        }

        public static double[] HandStrength(int[] handNumbers, int[] boardNumbers)
        {
            var ahead = 0.00;
            var tied = 0.00;
            var behind = 0.00;

            for (var c0 = 1; c0 < 53; c0++)
            {
                if (!handNumbers.Contains(c0))
                {
                    var o0 = TwoPlusTwo.EvalCard(53 + c0);
                    var h0 = TwoPlusTwo.EvalCard(53 + handNumbers[0]);
                    var h1 = TwoPlusTwo.EvalCard(h0 + handNumbers[1]);
                    var opponentNumbers = new int[7];
                    boardNumbers.CopyTo(opponentNumbers, 2);
                    opponentNumbers[0] = c0;
                    for (var c1 = 1; c1 < 53; c1++)
                    {
                        if (!handNumbers.Contains(c1) && !opponentNumbers.Contains(c1))
                        {
                            var o1 = TwoPlusTwo.EvalCard(o0 + c1);
                            var c2 = boardNumbers[0];
                            var o2 = TwoPlusTwo.EvalCard(o1 + c2);
                            var h2 = TwoPlusTwo.EvalCard(h1 + c2);
                            var c3 = boardNumbers[1];
                            var o3 = TwoPlusTwo.EvalCard(o2 + c3);
                            var h3 = TwoPlusTwo.EvalCard(h2 + c3);
                            var c4 = boardNumbers[2];
                            var o4 = TwoPlusTwo.EvalCard(o3 + c4);
                            var h4 = TwoPlusTwo.EvalCard(h3 + c4);

                            opponentNumbers[1] = c1;

                            var comparedHands = CompareHands(h4, o4);

                            if (boardNumbers.Length == 4)
                            {
                                var c5 = boardNumbers[3];
                                var o5 = TwoPlusTwo.EvalCard(o4 + c5);
                                var h5 = TwoPlusTwo.EvalCard(h4 + c5);

                                comparedHands = CompareHands(h5, o5);
                            }

                            if (comparedHands == 0)
                                ahead++;
                            else if (comparedHands == 1)
                                tied++;
                            else
                                behind++;
                        }
                    }
                }
            }
            return new[] { ahead, tied, behind };
        }

        public static double[][,] HandPotential(Hand hand, Hand boardCards)
        {
            var handNumbers = new int[7];
            hand.ToInts().CopyTo(handNumbers, 0);
            var boardNumbers = boardCards.ToInts();

            return HandPotential(handNumbers, boardNumbers);
        }
        public static double[][,] HandPotential(int[] handNumbers, int[] boardNumbers)
        {
            var handPotentialTotal = new[,] { { 0.00, 0.00, 0.00 } };
            var handPotential = new[,] { { 0.00, 0.00, 0.00 }, { 0.00, 0.00, 0.00 }, { 0.00, 0.00, 0.00 } };

            var ahead = 0;
            var tied = 1;
            var behind = 2;

            for (var c0 = 1; c0 < 53; c0++)
            {
                if (!handNumbers.Contains(c0))
                {
                    var o0 = TwoPlusTwo.EvalCard(53 + c0);
                    var h0 = TwoPlusTwo.EvalCard(53 + handNumbers[0]);
                    var h1 = TwoPlusTwo.EvalCard(h0 + handNumbers[1]);
                    var opponentNumbers = new int[7];
                    boardNumbers.CopyTo(opponentNumbers, 2);
                    opponentNumbers[0] = c0;
                    for (var c1 = 1; c1 < 53; c1++)
                    {
                        if (!handNumbers.Contains(c1) && !opponentNumbers.Contains(c1))
                        {
                            var o1 = TwoPlusTwo.EvalCard(o0 + c1);
                            var c2 = boardNumbers[0];
                            var o2 = TwoPlusTwo.EvalCard(o1 + c2);
                            var h2 = TwoPlusTwo.EvalCard(h1 + c2);
                            var c3 = boardNumbers[1];
                            var o3 = TwoPlusTwo.EvalCard(o2 + c3);
                            var h3 = TwoPlusTwo.EvalCard(h2 + c3);
                            var c4 = boardNumbers[2];
                            var o4 = TwoPlusTwo.EvalCard(o3 + c4);
                            var h4 = TwoPlusTwo.EvalCard(h3 + c4);

                            opponentNumbers[1] = c1;

                            var comparedHands = CompareHands(h4, o4);

                            int o5 = 0;
                            int h5 = 0;
                            int index;
                            if (boardNumbers.Length == 4)
                            {
                                var c5 = boardNumbers[3];
                                o5 = TwoPlusTwo.EvalCard(o4 + c5);
                                h5 = TwoPlusTwo.EvalCard(h4 + c5);

                                comparedHands = CompareHands(h5, o5);
                            }

                            if (comparedHands == ahead)
                                index = ahead;
                            else if (comparedHands == tied)
                                index = tied;
                            else
                                index = behind;
                            for (var c6 = 1; c6 < 53; c6++)
                            {
                                if (boardNumbers.Length == 3)
                                {
                                    for (var c5 = 1; c5 < 53; c5++)
                                    {
                                        if (!handNumbers.Contains(c5) && !opponentNumbers.Contains(c5))
                                        {
                                            handNumbers[5] = c5;
                                            opponentNumbers[5] = c5;
                                            o5 = TwoPlusTwo.EvalCard(o4 + c5);
                                            h5 = TwoPlusTwo.EvalCard(h4 + c5);
                                            if (!handNumbers.Contains(c6) && !opponentNumbers.Contains(c6))
                                            {
                                                handPotentialTotal[0, index]++;
                                                var o6 = TwoPlusTwo.EvalCard(o5 + c6);
                                                var h6 = TwoPlusTwo.EvalCard(h5 + c6);

                                                comparedHands = CompareHands(h6, o6);

                                                if (comparedHands == ahead)
                                                    handPotential[index, ahead]++;
                                                else if (comparedHands == tied)
                                                    handPotential[index, tied]++;
                                                else
                                                    handPotential[index, behind]++;
                                            }
                                        }
                                    }
                                }
                                else if (boardNumbers.Length == 4)
                                {
                                    if (!handNumbers.Contains(c6) && !opponentNumbers.Contains(c6))
                                    {
                                        handPotentialTotal[0, index]++;
                                        var o6 = TwoPlusTwo.EvalCard(o5 + c6);
                                        var h6 = TwoPlusTwo.EvalCard(h5 + c6);

                                        comparedHands = CompareHands(h6, o6);

                                        if (comparedHands == ahead)
                                            handPotential[index, ahead]++;
                                        else if (comparedHands == tied)
                                            handPotential[index, tied]++;
                                        else
                                            handPotential[index, behind]++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return new[] { handPotential, handPotentialTotal };
        }

        private static int CompareHands(int a, int b)
        {
            var handRank = a & 0x00000fff;
            var handType = ((a >> 12) - 1);
            var opponentRank = b & 0x00000fff;
            var opponentType = ((b >> 12) - 1);

            if ((handType > opponentType) || (handType == opponentType && handRank > opponentRank))
                return 0;
            if (handType == opponentType && handRank == opponentRank)
                return 1;
            return 2;
        }
    }
}
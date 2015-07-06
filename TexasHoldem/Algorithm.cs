using System.Collections.Generic;

namespace TexasHoldem
{
    public class Algorithm
    {
        public static double[] HandStrength(Hand hand, Hand boardCards)
        {
            var ahead = 0.00;
            var tied = 0.00;
            var behind = 0.00;
            var unknownCards = HandEvaluator.GetUnknownCards(hand);
            foreach (var unknownHand in HandEvaluator.GetHandCombinations(unknownCards, 2))
            {
                foreach (var card in boardCards)
                {
                    unknownHand.Add(card);
                }
                unknownHand.Evaluate();
                if (hand > unknownHand)
                    ahead++;
                else if (hand == unknownHand)
                    tied++;
                else
                    behind++;
            }

            return new[] { ahead, tied, behind };
        }

        public static double[][,] HandPotential(Hand hand, Hand boardCards)
        {
            var unknownCards = HandEvaluator.GetUnknownCards(hand);

            var handPotentialTotal = new[,] { { 0.00, 0.00, 0.00 } };
            var handPotential = new[,] { { 0.00, 0.00, 0.00 }, { 0.00, 0.00, 0.00 }, { 0.00, 0.00, 0.00 } };
            
            foreach (var unknownHand in HandEvaluator.GetHandCombinations(unknownCards, 2))
            {
                var ahead = 0;
                var tied = 1;
                var behind = 2;

                foreach (var card in boardCards)
                {
                    unknownHand.Add(card);
                }
                int index;
                unknownHand.Evaluate();

                if (hand > unknownHand)
                    index = ahead;
                else if (hand == unknownHand)
                    index = tied;
                else
                    index = behind;
                var deckAfterFlop = HandEvaluator.GetUnknownCards(hand, unknownHand);
                if (boardCards.Count > 2 && boardCards.Count < 5)
                {
                    var processedCards = new Hand();
                    foreach (var card in deckAfterFlop)
                    {
                        processedCards.Add(card);
                        var possibleTurnHand = hand.Copy();
                        var opponentPossibleTurnHand = unknownHand.Copy();
                        possibleTurnHand.Add(card);
                        opponentPossibleTurnHand.Add(card);

                        if (boardCards.Count == 3)
                        {
                            var deckAfterTurn =
                                HandEvaluator.GetUnknownCards(possibleTurnHand, opponentPossibleTurnHand, processedCards);
                            foreach (var secondCard in deckAfterTurn)
                            {
                                handPotentialTotal[0, index]++;
                                var possibleRiverHand = possibleTurnHand.Copy();
                                var opponentPossibleRiverHand = opponentPossibleTurnHand.Copy();
                                possibleRiverHand.Add(secondCard);
                                opponentPossibleRiverHand.Add(secondCard);
                                possibleRiverHand.Evaluate();
                                opponentPossibleRiverHand.Evaluate();
                                if (possibleRiverHand > opponentPossibleRiverHand)
                                    handPotential[index, ahead]++;
                                else if (possibleRiverHand == opponentPossibleRiverHand)
                                    handPotential[index, tied]++;
                                else if (possibleRiverHand < opponentPossibleRiverHand)
                                    handPotential[index, behind]++;
                            }
                        }
                        else if (boardCards.Count == 4)
                        {
                            handPotentialTotal[0, index]++;
                            possibleTurnHand.Evaluate();
                            opponentPossibleTurnHand.Evaluate();
                            if (possibleTurnHand > opponentPossibleTurnHand)
                                handPotential[index, ahead]++;
                            else if (possibleTurnHand == opponentPossibleTurnHand)
                                handPotential[index, tied]++;
                            else if (possibleTurnHand < opponentPossibleTurnHand)
                                handPotential[index, behind]++;
                        }
                    }
                }
            }
            return new double[2][,] { handPotential, handPotentialTotal };
        }

        public static int[] Combination(int[] array, int n, int k)
        {
            var numbers = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                numbers[i] = array[i];
            }
            var finished = false;
            var changed = false;

            if (k > 0)
            {
                for (int i = k - 1; !finished && !changed; i--)
                {
                    if (numbers[i] < (n - 1) - (k - 1) + i)
                    {
                        numbers[i]++;
                        if (i < k - 1)
                        {
                            for (int j = i + 1; j < k; j++)
                            {
                                numbers[j] = numbers[j - 1] + 1;
                            }
                        }
                        changed = true;
                    }
                    finished = i == 0;
                }
            }
            return numbers;
        }
    }
}
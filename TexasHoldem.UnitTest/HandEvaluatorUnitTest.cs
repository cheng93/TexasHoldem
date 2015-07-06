using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TexasHoldem.UnitTest
{
    [TestClass]
    public class HandEvaluatorUnitTest
    {
        [TestMethod]
        [TestCategory("Evaluate")]
        public void TestEvaluate1()
        {
            Card card1 = new Card(Value.Two, Suit.Hearts);
            Card card2 = new Card(Value.Three, Suit.Hearts);
            Card card3 = new Card(Value.Four, Suit.Hearts);
            Card card4 = new Card(Value.Five, Suit.Hearts);
            Card card5 = new Card(Value.Six, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(9, HandEvaluator.Evaluate(hand).Rank);
        }

        [TestMethod]
        [TestCategory("Evaluate")]
        public void TestEvaluate2()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Queen, Suit.Hearts);
            Card card3 = new Card(Value.Ten, Suit.Hearts);
            Card card4 = new Card(Value.Jack, Suit.Hearts);
            Card card5 = new Card(Value.King, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(1, HandEvaluator.Evaluate(hand).Rank);
            Assert.AreEqual(HandType.StraightFlush, HandEvaluator.Evaluate(hand).HandType);
        }

        [TestMethod]
        [TestCategory("Evaluate")]
        public void TestEvaluate3()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Ace, Suit.Clubs);
            Card card3 = new Card(Value.Ace, Suit.Diamonds);
            Card card4 = new Card(Value.Ace, Suit.Spades);
            Card card5 = new Card(Value.King, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(11, HandEvaluator.Evaluate(hand).Rank);
            Assert.AreEqual(HandType.FourOfAKind, HandEvaluator.Evaluate(hand).HandType);
        }

        [TestMethod]
        [TestCategory("Evaluate")]
        public void TestEvaluate4()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Ace, Suit.Clubs);
            Card card3 = new Card(Value.Ace, Suit.Diamonds);
            Card card4 = new Card(Value.King, Suit.Spades);
            Card card5 = new Card(Value.King, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(167, HandEvaluator.Evaluate(hand).Rank);
            Assert.AreEqual(HandType.FullHouse, HandEvaluator.Evaluate(hand).HandType);
        }

        [TestMethod]
        [TestCategory("Evaluate")]
        public void TestEvaluate5()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Queen, Suit.Hearts);
            Card card3 = new Card(Value.Jack, Suit.Hearts);
            Card card4 = new Card(Value.Nine, Suit.Hearts);
            Card card5 = new Card(Value.King, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(323, HandEvaluator.Evaluate(hand).Rank);
            Assert.AreEqual(HandType.Flush, HandEvaluator.Evaluate(hand).HandType);
        }

        [TestMethod]
        [TestCategory("Evaluate")]
        public void TestEvaluate6()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.King, Suit.Clubs);
            Card card3 = new Card(Value.Queen, Suit.Diamonds);
            Card card4 = new Card(Value.Ten, Suit.Spades);
            Card card5 = new Card(Value.Jack, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(1600, HandEvaluator.Evaluate(hand).Rank);
            Assert.AreEqual(HandType.Straight, HandEvaluator.Evaluate(hand).HandType);
        }

        [TestMethod]
        [TestCategory("Evaluate")]
        public void TestEvaluate7()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Ace, Suit.Clubs);
            Card card3 = new Card(Value.Ace, Suit.Diamonds);
            Card card4 = new Card(Value.King, Suit.Spades);
            Card card5 = new Card(Value.Queen, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(1610, HandEvaluator.Evaluate(hand).Rank);
            Assert.AreEqual(HandType.ThreeOfAKind, HandEvaluator.Evaluate(hand).HandType);
        }

        [TestMethod]
        [TestCategory("Evaluate")]
        public void TestEvaluate8()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Ace, Suit.Clubs);
            Card card3 = new Card(Value.Queen, Suit.Diamonds);
            Card card4 = new Card(Value.King, Suit.Spades);
            Card card5 = new Card(Value.King, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(2468, HandEvaluator.Evaluate(hand).Rank);
            Assert.AreEqual(HandType.TwoPair, HandEvaluator.Evaluate(hand).HandType);
        }

        [TestMethod]
        [TestCategory("Evaluate")]
        public void TestEvaluate9()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Ace, Suit.Clubs);
            Card card3 = new Card(Value.Queen, Suit.Diamonds);
            Card card4 = new Card(Value.King, Suit.Spades);
            Card card5 = new Card(Value.Jack, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(3326, HandEvaluator.Evaluate(hand).Rank);
            Assert.AreEqual(HandType.OnePair, HandEvaluator.Evaluate(hand).HandType);
        }

        [TestMethod]
        [TestCategory("Evaluate")]
        public void TestEvaluate10()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Nine, Suit.Clubs);
            Card card3 = new Card(Value.Queen, Suit.Diamonds);
            Card card4 = new Card(Value.King, Suit.Spades);
            Card card5 = new Card(Value.Jack, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(6186, HandEvaluator.Evaluate(hand).Rank);
            Assert.AreEqual(HandType.HighCard, HandEvaluator.Evaluate(hand).HandType);
        }

        [TestMethod]
        [TestCategory("Evaluate")]
        public void TestEvaluate11()
        {
            Card card1 = new Card(Value.Two, Suit.Hearts);
            Card card2 = new Card(Value.Three, Suit.Hearts);
            Card card3 = new Card(Value.Four, Suit.Hearts);
            Card card4 = new Card(Value.Five, Suit.Hearts);
            Card card5 = new Card(Value.Six, Suit.Hearts);
            Card card6 = new Card(Value.Seven, Suit.Hearts);
            Hand hand1 = new Hand { card1, card2, card3, card4, card5 };
            Hand hand2 = new Hand { card6, card2, card3, card4, card5 };

            Assert.IsTrue(HandEvaluator.Evaluate(hand2).Rank < HandEvaluator.Evaluate(hand1).Rank);
        }

        [TestMethod]
        [TestCategory("Evaluate")]
        public void TestEvaluate12()
        {
            Card card1 = new Card(Value.Two, Suit.Hearts);
            Card card2 = new Card(Value.Three, Suit.Diamonds);
            Card card3 = new Card(Value.Four, Suit.Hearts);
            Card card4 = new Card(Value.Five, Suit.Hearts);
            Card card5 = new Card(Value.Six, Suit.Hearts);
            Card card6 = new Card(Value.Seven, Suit.Hearts);
            Card card7 = new Card(Value.Eight, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5, card6, card7 };

            Assert.AreEqual(HandType.StraightFlush, HandEvaluator.Evaluate(hand).HandType);
        }

        [TestMethod]
        [TestCategory("GetHandCombinations")]
        public void TestGetHandCombinations1()
        {
            Card card1 = new Card(Value.Two, Suit.Hearts);
            Card card2 = new Card(Value.Three, Suit.Hearts);
            Card card3 = new Card(Value.Four, Suit.Hearts);
            Card card4 = new Card(Value.Five, Suit.Hearts);
            Card card5 = new Card(Value.Six, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };
            var hands = new List<Hand> { hand };
            var evalHands = HandEvaluator.GetHandCombinations(hand);
            CollectionAssert.AreEqual(hands, evalHands);
        }

        [TestMethod]
        [TestCategory("GetHandCombinations")]
        public void TestGetHandCombinations2()
        {
            Card card1 = new Card(Value.Two, Suit.Hearts);
            Card card2 = new Card(Value.Three, Suit.Hearts);
            Card card3 = new Card(Value.Four, Suit.Hearts);
            Card card4 = new Card(Value.Five, Suit.Hearts);
            Card card5 = new Card(Value.Six, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };
            var evalHands = HandEvaluator.GetHandCombinations(hand,2);
            Assert.AreEqual(10, evalHands.Count);
        }

        [TestMethod]
        [TestCategory("Flush")]
        public void TestFlush1()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.King, Suit.Hearts);
            Card card3 = new Card(Value.Queen, Suit.Hearts);
            Card card4 = new Card(Value.Jack, Suit.Hearts);
            Card card5 = new Card(Value.Ten, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(true, HandEvaluator.Flush(hand));
        }

        [TestMethod]
        [TestCategory("Flush")]
        public void TestFlush2()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.King, Suit.Hearts);
            Card card3 = new Card(Value.Queen, Suit.Hearts);
            Card card4 = new Card(Value.Jack, Suit.Diamonds);
            Card card5 = new Card(Value.Ten, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(false, HandEvaluator.Flush(hand));
        }

        [TestMethod]
        [TestCategory("StraightOrHighHand")]
        public void TestStraightOrHighHand1()
        {
            Card card1 = new Card(Value.Nine, Suit.Hearts);
            Card card2 = new Card(Value.Ten, Suit.Hearts);
            Card card3 = new Card(Value.Jack, Suit.Hearts);
            Card card4 = new Card(Value.Queen, Suit.Diamonds);
            Card card5 = new Card(Value.King, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(true, HandEvaluator.StraightOrHighHand(hand));
        }
        
        [TestMethod]
        [TestCategory("StraightOrHighHand")]
        public void TestStraightOrHighHand2()
        {
            Card card1 = new Card(Value.Seven, Suit.Hearts);
            Card card2 = new Card(Value.Five, Suit.Hearts);
            Card card3 = new Card(Value.Four, Suit.Hearts);
            Card card4 = new Card(Value.Three, Suit.Diamonds);
            Card card5 = new Card(Value.Two, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(true, HandEvaluator.StraightOrHighHand(hand));
        }
        
        [TestMethod]
        [TestCategory("StraightOrHighHand")]
        public void TestStraightOrHighHand3()
        {
            Card card1 = new Card(Value.Eight, Suit.Hearts);
            Card card2 = new Card(Value.Ten, Suit.Hearts);
            Card card3 = new Card(Value.Jack, Suit.Hearts);
            Card card4 = new Card(Value.Queen, Suit.Diamonds);
            Card card5 = new Card(Value.King, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(true, HandEvaluator.StraightOrHighHand(hand));
        }

        [TestMethod]
        [TestCategory("StraightOrHighHand")]
        public void TestStraightOrHighHand4()
        {
            Card card1 = new Card(Value.King, Suit.Diamonds);
            Card card2 = new Card(Value.Ten, Suit.Hearts);
            Card card3 = new Card(Value.Jack, Suit.Hearts);
            Card card4 = new Card(Value.Queen, Suit.Diamonds);
            Card card5 = new Card(Value.King, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(false, HandEvaluator.StraightOrHighHand(hand));
        }

        [TestMethod]
        [TestCategory("Shift")]
        public void TestShift1()
        {
            Card card1 = new Card(Value.Two, Suit.Hearts);
            Card card2 = new Card(Value.Three, Suit.Hearts);
            Card card3 = new Card(Value.Four, Suit.Hearts);
            Card card4 = new Card(Value.Five, Suit.Diamonds);
            Card card5 = new Card(Value.Six, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(31, HandEvaluator.Shift(hand));
        }

        [TestMethod]
        [TestCategory("Shift")]
        public void TestShift2()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.King, Suit.Hearts);
            Card card3 = new Card(Value.Queen, Suit.Hearts);
            Card card4 = new Card(Value.Jack, Suit.Diamonds);
            Card card5 = new Card(Value.Nine, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(7808, HandEvaluator.Shift(hand));
        }

        [TestMethod]
        [TestCategory("PrimeMagic")]
        public void TestPrimeMagic1()
        {
            Card card1 = new Card(Value.Two, Suit.Clubs);
            Card card2 = new Card(Value.Two, Suit.Spades);
            Card card3 = new Card(Value.Two, Suit.Hearts);
            Card card4 = new Card(Value.Two, Suit.Diamonds);
            Card card5 = new Card(Value.Three, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(48, HandEvaluator.PrimeMagic(hand));
        }

        [TestMethod]
        [TestCategory("PrimeMagic")]
        public void TestPrimeMagic2()
        {
            Card card1 = new Card(Value.Ace, Suit.Spades);
            Card card2 = new Card(Value.Ace, Suit.Hearts);
            Card card3 = new Card(Value.King, Suit.Hearts);
            Card card4 = new Card(Value.Ace, Suit.Diamonds);
            Card card5 = new Card(Value.Ace, Suit.Clubs);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(104553157, HandEvaluator.PrimeMagic(hand));
        }
    }
}
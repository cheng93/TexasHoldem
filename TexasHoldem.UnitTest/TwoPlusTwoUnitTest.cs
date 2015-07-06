using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TexasHoldem.UnitTest
{
    [TestClass]
    public class TwoPlusTwoUnitTest
    {
        [TestMethod]
        [TestCategory("TwoPlusTwo")]
        public void TestLookupHand5_1()
        {
            Card card1 = new Card(Value.Ace, Suit.Spades);
            Card card2 = new Card(Value.Ace, Suit.Clubs);
            Card card3 = new Card(Value.Ace, Suit.Diamonds);
            Card card4 = new Card(Value.Five, Suit.Diamonds);
            Card card5 = new Card (Value.Five, Suit.Spades);
            Assert.AreEqual(28820, TwoPlusTwo.EvaluateCards(new int[] { card1.Number, card2.Number, card3.Number, card4.Number, card5.Number }));
        }

        [TestMethod]
        [TestCategory("TwoPlusTwo")]
        public void TestLookupHand5_2()
        {
            Card card1 = new Card(Value.Ace, Suit.Spades);
            Card card2 = new Card(Value.Ace, Suit.Clubs);
            Card card3 = new Card(Value.Ace, Suit.Diamonds);
            Card card4 = new Card(Value.Five, Suit.Diamonds);
            Card card5 = new Card(Value.Five, Suit.Spades);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };
            Assert.AreEqual(28820, TwoPlusTwo.EvaluateCards(hand));
        }

        [TestMethod]
        [TestCategory("TwoPlusTwo")]
        public void TestEvaluate1()
        {
            Card card1 = new Card(Value.Two, Suit.Hearts);
            Card card2 = new Card(Value.Three, Suit.Hearts);
            Card card3 = new Card(Value.Four, Suit.Hearts);
            Card card4 = new Card(Value.Five, Suit.Hearts);
            Card card5 = new Card(Value.Six, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(HandType.StraightFlush, hand.HandType);
        }

        [TestMethod]
        [TestCategory("TwoPlusTwo")]
        public void TestEvaluate2()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Queen, Suit.Hearts);
            Card card3 = new Card(Value.Ten, Suit.Hearts);
            Card card4 = new Card(Value.Jack, Suit.Hearts);
            Card card5 = new Card(Value.King, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(HandType.StraightFlush, hand.HandType);
        }

        [TestMethod]
        [TestCategory("TwoPlusTwo")]
        public void TestEvaluate3()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Ace, Suit.Clubs);
            Card card3 = new Card(Value.Ace, Suit.Diamonds);
            Card card4 = new Card(Value.Ace, Suit.Spades);
            Card card5 = new Card(Value.King, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(HandType.FourOfAKind, hand.HandType);
        }

        [TestMethod]
        [TestCategory("TwoPlusTwo")]
        public void TestEvaluate4()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Ace, Suit.Clubs);
            Card card3 = new Card(Value.Ace, Suit.Diamonds);
            Card card4 = new Card(Value.King, Suit.Spades);
            Card card5 = new Card(Value.King, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(HandType.FullHouse, hand.HandType);
        }

        [TestMethod]
        [TestCategory("TwoPlusTwo")]
        public void TestEvaluate5()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Queen, Suit.Hearts);
            Card card3 = new Card(Value.Jack, Suit.Hearts);
            Card card4 = new Card(Value.Nine, Suit.Hearts);
            Card card5 = new Card(Value.King, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(HandType.Flush, hand.HandType);
        }

        [TestMethod]
        [TestCategory("TwoPlusTwo")]
        public void TestEvaluate6()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.King, Suit.Clubs);
            Card card3 = new Card(Value.Queen, Suit.Diamonds);
            Card card4 = new Card(Value.Ten, Suit.Spades);
            Card card5 = new Card(Value.Jack, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(HandType.Straight, hand.HandType);
        }

        [TestMethod]
        [TestCategory("TwoPlusTwo")]
        public void TestEvaluate7()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Ace, Suit.Clubs);
            Card card3 = new Card(Value.Ace, Suit.Diamonds);
            Card card4 = new Card(Value.King, Suit.Spades);
            Card card5 = new Card(Value.Queen, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(HandType.ThreeOfAKind, hand.HandType);
        }

        [TestMethod]
        [TestCategory("TwoPlusTwo")]
        public void TestEvaluate8()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Ace, Suit.Clubs);
            Card card3 = new Card(Value.Queen, Suit.Diamonds);
            Card card4 = new Card(Value.King, Suit.Spades);
            Card card5 = new Card(Value.King, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(HandType.TwoPair, hand.HandType);
        }

        [TestMethod]
        [TestCategory("TwoPlusTwo")]
        public void TestEvaluate9()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Ace, Suit.Clubs);
            Card card3 = new Card(Value.Queen, Suit.Diamonds);
            Card card4 = new Card(Value.King, Suit.Spades);
            Card card5 = new Card(Value.Jack, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(HandType.OnePair, hand.HandType);
        }

        [TestMethod]
        [TestCategory("TwoPlusTwo")]
        public void TestEvaluate10()
        {
            Card card1 = new Card(Value.Ace, Suit.Hearts);
            Card card2 = new Card(Value.Nine, Suit.Clubs);
            Card card3 = new Card(Value.Queen, Suit.Diamonds);
            Card card4 = new Card(Value.King, Suit.Spades);
            Card card5 = new Card(Value.Jack, Suit.Hearts);
            Hand hand = new Hand { card1, card2, card3, card4, card5 };

            Assert.AreEqual(HandType.HighCard, hand.HandType);
        }

        [TestMethod]
        [TestCategory("TwoPlusTwo")]
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

            Assert.IsTrue(hand2.Rank > hand1.Rank);
            Assert.IsTrue(hand2.HandType == hand1.HandType);
            Assert.IsTrue(hand2.HandType == HandType.StraightFlush);
        }

        [TestMethod]
        [TestCategory("TwoPlusTwo")]
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

            Assert.AreEqual(HandType.StraightFlush, hand.HandType);
        }
    }
}

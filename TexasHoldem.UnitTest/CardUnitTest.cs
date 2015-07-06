using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TexasHoldem.UnitTest
{
    [TestClass]
    public class CardUnitTest
    {
        [TestMethod]
        [TestCategory("ToBytes")]
        public void TestToBytes_1()
        {
            Card card = new Card(Value.King, Suit.Diamonds);

            byte[] bytes =
            {
                Convert.ToByte("00100101", 2), Convert.ToByte("01001011", 2), Convert.ToByte("00000000", 2),
                Convert.ToByte("00001000", 2)
            };
            CollectionAssert.AreEqual(bytes, Card.ToBytes(card));
        }

        [TestMethod]
        [TestCategory("ToInt32")]
        public void TestToInt32_1()
        {
            Card card = new Card(Value.King, Suit.Diamonds);
            Assert.AreEqual(Convert.ToInt32("00001000000000000100101100100101", 2), Card.ToInt32(card));
        }

        [TestMethod]
        [TestCategory("ToInt32")]
        public void TestToInt32_2()
        {
            Card card = new Card(Value.Five, Suit.Spades);
            Assert.AreEqual(Convert.ToInt32("00000000000010000001001100000111", 2), Card.ToInt32(card));
        }

        [TestMethod]
        [TestCategory("ToInt32")]
        public void TestToInt32_3()
        {
            Card card = new Card(Value.Jack, Suit.Clubs);
            Assert.AreEqual(Convert.ToInt32("00000010000000001000100100011101", 2), Card.ToInt32(card));
        }
    }
}
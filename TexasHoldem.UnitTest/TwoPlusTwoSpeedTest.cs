using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TexasHoldem.UnitTest
{
    [TestClass]
    public class TwoPlusTwoSpeedTest
    {
        [TestMethod]
        [TestCategory("TwoPlusTwoSpeedTest")]
        public void EnumerateAllHands()
        {
            var handTypeSum = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var count = 0;

            Console.WriteLine("Enumerating and evaluating all 133,784,560 possible 7-card poker hands...");

            var startTime = DateTime.Now;

            for (var c0 = 1; c0 < 47; c0++)
            {
                var u0 = TwoPlusTwo.EvalCard(53 + c0);
                for (var c1 = c0 + 1; c1 < 48; c1++)
                {
                    var u1 = TwoPlusTwo.EvalCard(u0 + c1);
                    for (var c2 = c1 + 1; c2 < 49; c2++)
                    {
                        var u2 = TwoPlusTwo.EvalCard(u1 + c2);
                        for (var c3 = c2 + 1; c3 < 50; c3++)
                        {
                            var u3 = TwoPlusTwo.EvalCard(u2 + c3);
                            for (var c4 = c3 + 1; c4 < 51; c4++)
                            {
                                var u4 = TwoPlusTwo.EvalCard(u3 + c4);
                                for (var c5 = c4 + 1; c5 < 52; c5++)
                                {
                                    var u5 = TwoPlusTwo.EvalCard(u4 + c5);
                                    for (var c6 = c5 + 1; c6 < 53; c6++)
                                    {

                                        handTypeSum[TwoPlusTwo.EvalCard(u5 + c6) >> 12]++;
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            var endTime = DateTime.Now;

            Console.WriteLine("BAD:              {0}", handTypeSum[0]);
            Console.WriteLine("High Card:        {0}", handTypeSum[1]);
            Console.WriteLine("One Pair:         {0}", handTypeSum[2]);
            Console.WriteLine("Two Pair:         {0}", handTypeSum[3]);
            Console.WriteLine("Trips:            {0}", handTypeSum[4]);
            Console.WriteLine("Straight:         {0}", handTypeSum[5]);
            Console.WriteLine("Flush:            {0}", handTypeSum[6]);
            Console.WriteLine("Full House:       {0}", handTypeSum[7]);
            Console.WriteLine("Quads:            {0}", handTypeSum[8]);
            Console.WriteLine("Straight Flush:   {0}", handTypeSum[9]);

            var testCount = 0;
            for (var index = 0; index < 10; index++)
                testCount += handTypeSum[index];
            Assert.IsTrue(testCount == count);
            Assert.IsTrue(count == 133784560);
            Assert.IsTrue(handTypeSum[0] == 0);

            Console.WriteLine();
            Console.WriteLine("Enumerated " + count + " hands in " + (endTime - startTime) + " milliseconds!");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace TexasHoldem
{
    public class Hand : ICollection<Card>
    {
        private readonly ICollection<Card> _hand;

        private readonly string _name;

        private readonly int _opponents;

        private int _rank;

        private double _handStrength;

        public string Name
        {
            get { return _name; }
        }

        public bool Fold { get; set; }

        public bool Raise { get; set; }

        public bool IsReadOnly { get; set; }
        
        public double EffectiveHandStrength { get; set; }

        public double WinningProbability { get; set; }

        public double HandStrength
        {
            get { return _handStrength; }
            set { _handStrength = Math.Pow(value, _opponents); }
        }

        public HandType HandType { get; set; }
        
        public int Points { get; set; }
        
        public int Rank
        {
            get { return _rank; }
            set
            {
                _rank = value;
                HandType = AssignHandType(value);
            }
        }

        public int Count
        {
            get { return _hand.Count; }
        }

        public Hand()
            : this(null)
        {
        }

        public Hand(string name, int opponents = 0)
        {
            Rank = int.MaxValue;
            _name = name;
            _opponents = opponents;
            _hand = new List<Card>();
        }

        public override string ToString()
        {
            string output = null;
            for (int i = 0; i < _hand.Count; i++)
            {
                output += (i != 0 ? ", " : "") + _hand.ElementAt(i);
            }
            return output;
        }

        public int[] ToInts()
        {
            var output = new int[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                output[i] = this.ElementAt(i).Number;
            }
            return output;
        }

        public void Add(Card card)
        {
            _hand.Add(card);
        }

        public void Clear()
        {
            _hand.Clear();
        }

        public bool Contains(Card item)
        {
            return _hand.Any(card => card == item);
        }

        public void CopyTo(Card[] array, int arrayIndex)
        {
            _hand.CopyTo(array, arrayIndex);
        }

        public Hand Copy()
        {
            var output = new Hand();
            foreach (var card in _hand)
            {
                output.Add(card);
            }
            return output;
        }

        public bool Remove(Card item)
        {
            return _hand.Remove(item);
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return _hand.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Evaluate()
        {
            int value = TwoPlusTwo.EvaluateCards(this);
            Points = value;
            Rank = value & 0x00000fff;
            HandType = (HandType)((value >> 12) - 1);
        }

        private HandType AssignHandType(int rank)
        {
            if (rank >= 1 && rank < 11 )
                return HandType.StraightFlush;
            if (rank >= 11 && rank < 167)
                return HandType.FourOfAKind;
            if (rank >= 167 && rank < 323)
                return HandType.FullHouse;
            if (rank >= 323 && rank < 1600)
                return HandType.Flush;
            if (rank >= 1600 && rank < 1610)
                return HandType.Straight;
            if (rank >= 1610 && rank < 2468)
                return HandType.ThreeOfAKind;
            if (rank >= 2468 && rank < 3326)
                return HandType.TwoPair;
            if (rank >= 3326 && rank < 6186)
                return HandType.OnePair;
            return HandType.HighCard;
        }

        public static bool operator <(Hand a, Hand b)
        {
            return ((int)a.HandType < (int)b.HandType) || ((int)a.HandType == (int)b.HandType && a.Rank < b.Rank);
        }

        public static bool operator >(Hand a, Hand b)
        {
            return ((int)a.HandType > (int)b.HandType) || ((int) a.HandType == (int)b.HandType && a.Rank > b.Rank);
        }

        public static bool operator ==(Hand a, Hand b)
        {
            if ((object)a == null || (object)b == null)
            {
                return false;
            }
            return a.HandType == b.HandType && a.Rank == b.Rank;
        }

        public static bool operator !=(Hand a, Hand b)
        {
            return !(a == b);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Hand p = obj as Hand;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return this == p;
        }

        public bool Equals(Hand p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return this == p;
        }
    }

    public enum HandType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush
    }
}
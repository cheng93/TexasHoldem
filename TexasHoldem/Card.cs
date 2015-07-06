using System;

namespace TexasHoldem
{
    public struct Card
    {
        private readonly Suit _suit;

        private readonly Value _value;

        private readonly int _number;

        public Suit Suit
        {
            get { return _suit; }
        }

        public Value Value
        {
            get { return _value; }
        }

        public int Number
        {
            get { return _number; }
        }

        public Card(Value value, Suit suit)
        {
            _value = value;
            _suit = suit;
            _number = (int) value*4 + (int) suit + 1;
        }

        public override string ToString()
        {
            return _value + " " + _suit;
        }

        public static byte[] ToBytes(Card card)
        {
            var output = new byte[4];
            switch (card.Value)
            {
                case Value.Ace:
                    output[3] = 16;
                    break;

                case Value.King:
                    output[3] = 8;
                    break;

                case Value.Queen:
                    output[3] = 4;
                    break;

                case Value.Jack:
                    output[3] = 2;
                    break;

                case Value.Ten:
                    output[3] = 1;
                    break;

                default:
                    output[3] = 0;
                    break;
            }

            switch (card.Value)
            {
                case Value.Two:
                    output[2] = 1;
                    break;

                case Value.Three:
                    output[2] = 2;
                    break;

                case Value.Four:
                    output[2] = 4;
                    break;

                case Value.Five:
                    output[2] = 8;
                    break;

                case Value.Six:
                    output[2] = 16;
                    break;

                case Value.Seven:
                    output[2] = 32;
                    break;

                case Value.Eight:
                    output[2] = 64;
                    break;

                case Value.Nine:
                    output[2] = 128;
                    break;

                default:
                    output[2] = 0;
                    break;
            }

            output[1] = (byte)card.Value;
            switch (card.Suit)
            {
                case Suit.Clubs:
                    output[1] += 128;
                    break;

                case Suit.Diamonds:
                    output[1] += 64;
                    break;

                case Suit.Hearts:
                    output[1] += 32;
                    break;

                case Suit.Spades:
                    output[1] += 16;
                    break;
            }

            output[0] = (byte)Array.Primes[(int)card.Value];

            return output;
        }

        public static int ToInt32(Card card)
        {
            return BitConverter.ToInt32(ToBytes(card), 0);
        }

        public static bool operator ==(Card a, Card b)
        {
            return a.Value == b.Value && a.Suit == b.Suit;
        }

        public static bool operator !=(Card a, Card b)
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
            Card p = obj as Card? ?? new Card();
            // Return true if the fields match:
            return this == p;
        }

        public bool Equals(Card p)
        {
            // Return true if the fields match:
            return this == p;
        }

        public override int GetHashCode()
        {
            return (int)Value ^ (int)Suit;
        }
    }

    public enum Value
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
}
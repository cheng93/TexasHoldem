using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    public class Deck
    {
        private Stack<Card> _deck;

        public Stack<Card> Cards
        {
            get { return _deck; }
        }

        public Deck()
        {
            Init();
        }

        private void Init()
        {
            _deck = new Stack<Card>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    _deck.Push(new Card((Value)j, (Suit)i));
                }
            }
        }

        public Card Deal()
        {
            return _deck.Pop();
        }

        public void Burn()
        {
            _deck.Pop();
        }

        public void Shuffle()
        {
            for (int i = 0; i < 10000; i++)
            {
                ShuffleProcess();
            }
        }

        private void ShuffleProcess()
        {
            Random r = new Random();
            List<Card> shuffleDeck = new List<Card>();

            while (_deck.Any())
            {
                shuffleDeck.Add(_deck.Pop());
            }

            while (shuffleDeck.Any())
            {
                int i = r.Next(shuffleDeck.Count());
                _deck.Push(shuffleDeck.ElementAt(i));
                shuffleDeck.RemoveAt(i);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    public class PokerLogic
    {
        private ICollection<Hand> _hands;

        private ICollection<Hand> _bestHands;

        private Hand _boardCards;

        private ICollection<Card> _flop;

        private readonly int _numberOfPlayers;

        private Deck _deck;

        private Card _turn;

        private Card _river;

        public PokerLogic(int players)
        {
            _numberOfPlayers = players;
        }

        public void Run()
        {

            Init();

            Deal();

            PrintHands();

            Flop();

            EvaluateHands();

            PrintHandStrengths();

            _hands = AiLogic.Run(_hands, _boardCards);

            if (_hands.Count(hand => hand.Fold == false) > 1)
            {
                Turn();

                EvaluateHands();

                PrintHandStrengths();

                _hands = AiLogic.Run(_hands, _boardCards);
                if (_hands.Count(hand => hand.Fold == false) > 1)
                {
                    River();

                    EvaluateHands();

                    PrintHandStrengths();

                    _hands = AiLogic.Run(_hands, _boardCards);
                }
            }

            RateHands();

            PrintBestHands();

            PrintWinningHands();
        }

        private void Init()
        {
            _deck = new Deck();

            _hands = new List<Hand>();

            _bestHands = new List<Hand>();

            _flop = new List<Card>();

            _boardCards = new Hand();

            CreateHands();
        }

        private void CreateHands()
        {
            for (var i = 0; i < _numberOfPlayers; i++)
            {
                _hands.Add(new Hand((i == 0 ? "Player" : "CPU " + i), _numberOfPlayers -1));
            }
        }

        private void Deal()
        {
            _deck.Shuffle();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < _numberOfPlayers; j++)
                {
                    _hands.ElementAt(j).Add(_deck.Deal());
                }
            }
        }

        private void PrintHands()
        {
            foreach (var hand in _hands)
            {
                Console.WriteLine("{0} hand: {1}", hand.Name, hand);
            }
            Console.WriteLine();
        }

        private void AddToHands(Card card)
        {
            foreach (var hand in _hands)
            {
                hand.Add(card);
            }
        }

        private void Flop()
        {
            for (int i = 0; i < 3; i++)
            {
                _deck.Burn();
                var card = _deck.Deal();
                _flop.Add(card);
                _boardCards.Add(card);
                AddToHands(card);

                Console.Write("{0}{1}", (i == 0 ? "Flop: " : ", "), _flop.ElementAt(i));
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private void Turn()
        {
            _deck.Burn();
            _turn = _deck.Deal();
            _boardCards.Add(_turn);
            AddToHands(_turn);
            Console.WriteLine("Turn: " + _turn);
            Console.WriteLine();
        }

        private void River()
        {
            _deck.Burn();
            _river = _deck.Deal();
            _boardCards.Add(_river);
            AddToHands(_river);
            Console.WriteLine("River: " + _river);
            Console.WriteLine();
        }

        private void EvaluateHands()
        {
            foreach (var hand in _hands)
            {
                TwoPlusTwo.Evaluate(hand, _boardCards);
            }
        }

        private void RateHands()
        {
            foreach (var hand in _hands)
            {
                Console.WriteLine("{0} hand: {1}", hand.Name, hand.HandType);
                if (!_bestHands.Any())
                    _bestHands.Add(hand);
                else
                {
                    if (hand > _bestHands.First())
                    {
                        _bestHands.Clear();
                        _bestHands.Add(hand);
                    }
                    else if (hand == _bestHands.First())
                        _bestHands.Add(hand);
                }
            }
            Console.WriteLine();
        }

        private void PrintBestHands()
        {
            for (int i = 0; i < _bestHands.Count; i++)
            {
                Console.Write("{0} {1}", (i == 0 ? "Best hands:" : ","), _bestHands.ElementAt(i).Name);
            }
            Console.WriteLine();
        }

        private void PrintWinningHands()
        {
            var unfoldedHands = _bestHands.Where(hand => hand.Fold == false).ToList();
            for (int i = 0; i < unfoldedHands.Count; i++)
            {
                Console.Write("{0} {1}", (i == 0 ? "Winning hands:" : ","), unfoldedHands.ElementAt(i).Name);
            }
            Console.WriteLine();
        }
        
        private void PrintHandStrengths()
        {
            foreach (var hand in _hands)
            {
                if (hand.Fold == false)
                {
                    Console.WriteLine("{0} HS: {1} EFS: {2} P(Win): {3}", hand.Name,
                        hand.HandStrength.ToString("F"), hand.EffectiveHandStrength.ToString("F"),
                        hand.WinningProbability.ToString("F"));
                }
            }
            Console.WriteLine();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;

namespace Blackjack.Play.Entities
{
    public class Shoe
    {
        public int DecksInShoe { get; set; }
        private List<Card> _allCards;
        private static Random random = new Random();
        private bool _stageDealerHand;
        private bool _stagePlayerHand;
        public Shoe(int numberOfDecksInShoe)
        {
            DecksInShoe = numberOfDecksInShoe;
            _allCards = new List<Card>();
            for (var x = 0; x < numberOfDecksInShoe; x++)
                _allCards.AddRange(Card.GetAllCards().ToList());

            SetStagingConfiguration();

        }

        private void SetStagingConfiguration()
        {
            _stageDealerHand = bool.Parse(ConfigurationManager.AppSettings["PrototypeDealerHand"]);
            _stagePlayerHand = bool.Parse(ConfigurationManager.AppSettings["PrototypePlayerHand"]);

        }

        public Card GetCardFromShoe()
        {
            var card = _allCards[random.Next(_allCards.Count)];
            _allCards.Remove(card);
            return card;
        }

        public Card GetCardFromShoe(bool isDealer, bool firstCard)
        {
            if (isDealer && _stageDealerHand)
            {
                var hand = ConfigurationManager.AppSettings["DealerHand"].Split(',');
                return  GetCardFromConfiguration(firstCard ? hand[0] : hand[1]);
            }
            if (!isDealer && _stagePlayerHand)
            {
                var hand = ConfigurationManager.AppSettings["PlayerHand"].Split(',');
                return GetCardFromConfiguration(firstCard ? hand[0] : hand[1]);
            }
            return GetCardFromShoe();
        }

        private Card GetCardFromConfiguration(string configurationCard)
        {
            Card card;
            if (configurationCard.ToUpper() == "A")
            {
                card = _allCards.First(a=>a.IsAce);
                _allCards.Remove(card);
                return card;
            }
            card = _allCards.First(a => a.Value == int.Parse(configurationCard));
            _allCards.Remove(card);
            return card;
        }

        public bool IsDead()
        {
            var twoThirdsPenetration = (int)((decimal)DecksInShoe*(decimal)52*((decimal)1/(decimal)3));
            return _allCards.Count() < twoThirdsPenetration;
        }
    }
    public static class CardExtensions
    {
        public static string CardDescription(this Card card)
        {
            string suit = "";
            switch (card.Suit)
            {
                case Suit.Clubs:
                    suit = "Clubs";
                    break;
                case Suit.Diamonds:
                    suit = "Diamonds";
                    break;
                case Suit.Hearts:
                    suit = "Hearts";
                    break;
                case Suit.Spades:
                    suit = "Spades";
                    break;
            }
            return string.Format("{0} of {1}", card.Name, suit);
        }
    }
}
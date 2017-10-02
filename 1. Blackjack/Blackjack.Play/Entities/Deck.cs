using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Play.Entities
{
    public class Deck
    {
        private readonly List<Card> _cards;

        public Deck()
        {
            _cards = new List<Card>();
            for (var x = 0; x < 4; x++)
            {
                _cards.AddRange(Card.GetAllCards().ToList());
            }
        }

        public List<Card> GetAllCardsInDeck()
        {
            return _cards;
        } 
    }
}
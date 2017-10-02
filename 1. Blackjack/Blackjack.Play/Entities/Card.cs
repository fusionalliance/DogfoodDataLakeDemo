using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Play.Entities
{
    public class Card
    {
        public string Name { get; private set; }
        public int Value { get; private set; }
        public bool IsAce { get; private set; }
        public Suit Suit { get; private set; }

        private static Random random = new Random();

        public Card()
        {
            var card = allCards[random.Next(allCards.Count)];
            Name = card.Name;
            IsAce = card.IsAce;
            Value = card.Value;
            Suit = card.Suit;
        }

        static public IEnumerable<Card> GetAllCards()
        {
            foreach (var card in allCards)
                yield return new Card(card.Value, card.Suit, card.Name);
        }

        public override string ToString()
        {
            string suit = "";
            switch (this.Suit)
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
            return string.Format("{0} of {1}", Name, suit);
        }

        public Card(int value, Suit suit, string name)
        {
            var card = allCards.Single(a => a.Value == value && a.Suit == suit && a.Name == name);
            IsAce = card.IsAce;
            Value = value;
            Name = card.Name;
            Suit = card.Suit;
        }


        static List<AllCard> allCards = new List<AllCard>()
        {
            new AllCard()
            {
                IsAce = false,
                Name = "Deuce",
                Value = 2,
                Suit = Suit.Clubs,
            },
            new AllCard()
            {
                Suit = Suit.Clubs,
                IsAce = false,
                Name = "Three",
                Value = 3
            },
            new AllCard()
            {
                Suit = Suit.Clubs,
                IsAce = false,
                Name = "Four",
                Value = 4
            },
            new AllCard()
            {
                Suit = Suit.Clubs,
                IsAce = false,
                Name = "Five",
                Value = 5
            },
            new AllCard()
            {
                Suit = Suit.Clubs,
                IsAce = false,
                Name = "Six",
                Value = 6
            },
            new AllCard()
            {
                Suit = Suit.Clubs,
                IsAce = false,
                Name = "Seven",
                Value = 7
            },
            new AllCard()
            {
                Suit = Suit.Clubs,
                IsAce = false,
                Name = "Eight",
                Value = 8
            },
            new AllCard()
            {
                Suit = Suit.Clubs,
                IsAce = false,
                Name = "Nine",
                Value = 9
            },
            new AllCard()
            {
                Suit = Suit.Clubs,
                IsAce = false,
                Name = "Ten",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Clubs,
                IsAce = false,
                Name = "Jack",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Clubs,
                IsAce = false,
                Name = "Queen",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Clubs,
                IsAce = false,
                Name = "King",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Clubs,
                IsAce = true,
                Name = "Ace",
                Value = 1
            },
            /* */
            new AllCard()
            {
                IsAce = false,
                Name = "Deuce",
                Value = 2,
                Suit = Suit.Diamonds,
            },
            new AllCard()
            {
                Suit = Suit.Diamonds,
                IsAce = false,
                Name = "Three",
                Value = 3
            },
            new AllCard()
            {
                Suit = Suit.Diamonds,
                IsAce = false,
                Name = "Four",
                Value = 4
            },
            new AllCard()
            {
                Suit = Suit.Diamonds,
                IsAce = false,
                Name = "Five",
                Value = 5
            },
            new AllCard()
            {
                Suit = Suit.Diamonds,
                IsAce = false,
                Name = "Six",
                Value = 6
            },
            new AllCard()
            {
                Suit = Suit.Diamonds,
                IsAce = false,
                Name = "Seven",
                Value = 7
            },
            new AllCard()
            {
                Suit = Suit.Diamonds,
                IsAce = false,
                Name = "Eight",
                Value = 8
            },
            new AllCard()
            {
                Suit = Suit.Diamonds,
                IsAce = false,
                Name = "Nine",
                Value = 9
            },
            new AllCard()
            {
                Suit = Suit.Diamonds,
                IsAce = false,
                Name = "Ten",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Diamonds,
                IsAce = false,
                Name = "Jack",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Diamonds,
                IsAce = false,
                Name = "Queen",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Diamonds,
                IsAce = false,
                Name = "King",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Diamonds,
                IsAce = true,
                Name = "Ace",
                Value = 1
            },
            /* */
            new AllCard()
            {
                IsAce = false,
                Name = "Deuce",
                Value = 2,
                Suit = Suit.Hearts,
            },
            new AllCard()
            {
                Suit = Suit.Hearts,
                IsAce = false,
                Name = "Three",
                Value = 3
            },
            new AllCard()
            {
                Suit = Suit.Hearts,
                IsAce = false,
                Name = "Four",
                Value = 4
            },
            new AllCard()
            {
                Suit = Suit.Hearts,
                IsAce = false,
                Name = "Five",
                Value = 5
            },
            new AllCard()
            {
                Suit = Suit.Hearts,
                IsAce = false,
                Name = "Six",
                Value = 6
            },
            new AllCard()
            {
                Suit = Suit.Hearts,
                IsAce = false,
                Name = "Seven",
                Value = 7
            },
            new AllCard()
            {
                Suit = Suit.Hearts,
                IsAce = false,
                Name = "Eight",
                Value = 8
            },
            new AllCard()
            {
                Suit = Suit.Hearts,
                IsAce = false,
                Name = "Nine",
                Value = 9
            },
            new AllCard()
            {
                Suit = Suit.Hearts,
                IsAce = false,
                Name = "Ten",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Hearts,
                IsAce = false,
                Name = "Jack",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Hearts,
                IsAce = false,
                Name = "Queen",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Hearts,
                IsAce = false,
                Name = "King",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Hearts,
                IsAce = true,
                Name = "Ace",
                Value = 1
            },
            /* */
            new AllCard()
            {
                IsAce = false,
                Name = "Deuce",
                Value = 2,
                Suit = Suit.Spades,
            },
            new AllCard()
            {
                Suit = Suit.Spades,
                IsAce = false,
                Name = "Three",
                Value = 3
            },
            new AllCard()
            {
                Suit = Suit.Spades,
                IsAce = false,
                Name = "Four",
                Value = 4
            },
            new AllCard()
            {
                Suit = Suit.Spades,
                IsAce = false,
                Name = "Five",
                Value = 5
            },
            new AllCard()
            {
                Suit = Suit.Spades,
                IsAce = false,
                Name = "Six",
                Value = 6
            },
            new AllCard()
            {
                Suit = Suit.Spades,
                IsAce = false,
                Name = "Seven",
                Value = 7
            },
            new AllCard()
            {
                Suit = Suit.Spades,
                IsAce = false,
                Name = "Eight",
                Value = 8
            },
            new AllCard()
            {
                Suit = Suit.Spades,
                IsAce = false,
                Name = "Nine",
                Value = 9
            },
            new AllCard()
            {
                Suit = Suit.Spades,
                IsAce = false,
                Name = "Ten",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Spades,
                IsAce = false,
                Name = "Jack",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Spades,
                IsAce = false,
                Name = "Queen",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Spades,
                IsAce = false,
                Name = "King",
                Value = 10
            },
            new AllCard()
            {
                Suit = Suit.Spades,
                IsAce = true,
                Name = "Ace",
                Value = 1
            }
        };

    }

    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Blackjack.Play.Entities
{
    public abstract class Hand
    {
        protected internal List<Card> Cards = new List<Card>();
        public bool IsDoubled { get; set; }
        public bool WonBlackjack { get; set; }
        protected bool OutcomeAssigned;
        public HandOutcomes Outcome { get; set; }
        public bool IsKilled { get; set; }
        

        public int Value()
        {
            var count = Cards.Sum(a => a.Value);
            if (count <= 11 && Cards.Any(a => a.IsAce))
                count += 10;
            return count;
        }

        public string CardNames()
        {
            var cardNames = "(" + Value() + ") ";
            Cards.ForEach(a=>cardNames += (a.IsAce ? "A" : a.Value.ToString()) + ", " );
            cardNames = cardNames.Remove(cardNames.Length - 2);
            return cardNames;
        }

        public bool IsSoft()
        {
            return Cards.Sum(a => a.Value) <= 11 && Cards.Any(a => a.IsAce);

        }
        public bool IsBusted()
        {
            return Value() > 21;
        }

        public bool IsBlackjack()
        {
            return Cards.Any(a => a.IsAce) && Cards.Any(a => a.Value == 10);
        }

        public void AddCard(Card newCard)
        {
            Cards.Add(newCard);
        }

        public virtual void AwardOutcomes(int dealerCount)
        {
            if (OutcomeAssigned) return;
            if ((dealerCount > Value() || IsBusted()) && dealerCount <= 21)
            {
                Outcome = HandOutcomes.Loser;
            }
            else if (dealerCount == Value())
            {
                Outcome = HandOutcomes.Push;
            }
            else if (dealerCount < Value() || dealerCount > 21)
            {
                Outcome = HandOutcomes.Winner;
            }
            else
            {
                throw new Exception();
            }
            OutcomeAssigned = true;
        }

        public void Kill()
        {
            IsKilled = true;
            Outcome = HandOutcomes.Loser;
        }

    }

    public enum HandOutcomes
    {
        Winner,
        Loser,
        Push
    }
}
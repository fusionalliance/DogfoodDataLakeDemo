using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Play.Entities
{
    public class DealerHand : Hand
    {
        public DealerHand()
        {
            
        }

        public bool IsComplete()
        {
            if (!IsSoft() && Value() >= 17)
                return true;
            return false;
        }

        public Card GetShowCard()
        {
            return Cards[1];
        }
    }
}
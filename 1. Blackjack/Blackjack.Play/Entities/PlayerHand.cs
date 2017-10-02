using Blackjack.Play.Player_Strategy;

namespace Blackjack.Play.Entities
{

    public class PlayerHand : Hand
    {
        public bool IsComplete(PlayerStrategy strategy, Card dealerCard)
        {
            var playerValue = Value();
            var dealerShowValue = dealerCard.Value;
            if (!IsSoft() && playerValue >= 17)
                return true;

            if (IsSoft() && playerValue == 17)
                return !strategy.HitSoftSeventeen;

            if (dealerShowValue >= 2 && dealerShowValue <= 6)
            {
                if (playerValue >= 12)
                    return true;
            }


            return false;
        }

        public bool IsDouble(PlayerStrategy strategy, Card showCard)
        {
            var playerValue = this.Value();
            var dealerValue = showCard.Value;
            if (playerValue == 11 && !showCard.IsAce)
                return true;
            if (playerValue == 10)
            {
                return dealerValue != 10 && !showCard.IsAce;
            }
            if (playerValue == 9)
            {
                return dealerValue == 3 || dealerValue == 4 || dealerValue == 5 || dealerValue == 6;
            }
            if ((playerValue == 18 || playerValue == 17) && IsSoft())
            {
                return dealerValue == 3 || dealerValue == 4 || dealerValue == 5 || dealerValue == 6;
            }
            if ((playerValue == 16 || playerValue == 15) && IsSoft())
            {
                return dealerValue == 4 || dealerValue == 5 || dealerValue == 6;
            }
            if ((playerValue == 14 || playerValue == 13) && IsSoft())
            {
                return dealerValue == 5 || dealerValue == 6;
            }

            return false;

        }

        public void Double()
        {
            IsDoubled = true;
        }

        public decimal HandWinAmount()
        {
            if (IsDoubled)
            {
                return Outcome == HandOutcomes.Winner ? 2m : -2m;
            }
            if (this.IsBlackjack())
                return 1.5m;
            return Outcome == HandOutcomes.Winner ? 1m : -1;

        }
    }
}
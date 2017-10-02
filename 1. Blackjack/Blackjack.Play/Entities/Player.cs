using System.Collections.Specialized;
using Blackjack.Play.Player_Strategy;

namespace Blackjack.Play.Entities
{
    public class Player
    {

        public string Name { get; set; }
        public PlayerStrategy Strategy { get; private set; }
        public decimal BankRoll { get; private set; }

        public Player(PlayerStrategy strategy, string name = "Unknown", decimal startingBalance = 0)
        {
            Strategy = strategy;
            Name = name;
            BankRoll = startingBalance;
        }


        public void OfferInsurance()
        {
            if (Strategy.TakesInsurance)
            {
                
            }
        }


        public void ChargePlayer(decimal charge)
        {
            BankRoll -= charge;
        }

        public void AwardPlayer(decimal winnings)
        {
            BankRoll += winnings;
        }
    }
}
namespace Blackjack.Play.Entities
{
    public class HandOutcome
    {
        public Card DealerShowCard { get; set; }
        public Card PlayerCardOne { get; set; }
        public Card PlayerCardTwo { get; set; }
        public int DealerTotal { get; set; }
        public int PlayerTotal { get; set; }
        public decimal WinAmount { get; set; }
    }
}
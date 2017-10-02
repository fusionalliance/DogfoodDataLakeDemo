using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Blackjack.Play.Entities
{
    public class GameResult
    {
        public int DecksInShoe { get; set; }
        public int TotalHands { get; set; }
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
        public int TotalPushes { get; set; }
        public decimal TotalWinAmount { get; set; }
        public List<HandOutcome> HandOutcomes;

        public GameResult()
        {
            HandOutcomes = new List<HandOutcome>();
        }
        public override string ToString()
        {
            return string.Format("Total Decks: {0}\nTotal Wins: {1}\nTotal Pushes: {2}\nTotal Losses: {3}", DecksInShoe, TotalWins,
                TotalPushes, TotalLosses);
        }

        public void AddHandOutcome(PlayerHand playerCards, DealerHand dealerCards)
        {
            var outcome = new HandOutcome()
            {
                DealerShowCard = dealerCards.GetShowCard(),
                PlayerCardOne = playerCards.Cards[0],
                PlayerCardTwo = playerCards.Cards[1],
                DealerTotal = dealerCards.Value(),
                PlayerTotal = playerCards.Value(),
                WinAmount = playerCards.HandWinAmount()
            };
            HandOutcomes.Add(outcome);
        }
        
    }

    public static class GameResultExtensions
    {
        public static void GenerateCsvOfAllHands(this List<GameResult> results)
        {
            var sr = new StringBuilder();
            sr.AppendLine("PlayerCardOne,PlayerCardOneValue,PlayerCardTwo,PlayerCardTwoValue,PlayerCardsValue,DealerShowCard,DealerShowCardValue,DealerTotal,WinAmount");
            results.ForEach(b =>
            {
                b.HandOutcomes.ForEach(a =>
                {
                    sr.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", 
                        a.PlayerCardOne.ToString(),
                        a.PlayerCardOne.Value,
                        a.PlayerCardTwo.ToString(),
                        a.PlayerCardTwo.Value,
                        a.PlayerTotal,
                        a.DealerShowCard.ToString(),
                        a.DealerShowCard.Value,
                        a.DealerTotal,
                        a.WinAmount));
                });
            });
            File.WriteAllText(ConfigurationManager.AppSettings["DiskSaveLocation"], sr.ToString());
        }

        public static string CollectedOutcomes(this List<GameResult> results)
        {
            return string.Format("Total Decks: {0}\nTotal Wins: {1}\nTotal Pushes: {2}\nTotal Losses: {3}\nTotal Win Amount: {4}\nTotal Hands: {5}\nWin Percentage: {6}", results.First().DecksInShoe, results.Sum(a=>a.TotalWins),
                results.Sum(a => a.TotalPushes), results.Sum(a => a.TotalLosses), results.Sum(a=>a.TotalWinAmount), results.Sum(a=>a.TotalHands), CalculateWinPercentage(results));
        }

        private static string CalculateWinPercentage(List<GameResult> results)
        {
            return "?";
        }
    }
}
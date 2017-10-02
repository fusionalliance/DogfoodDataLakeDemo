using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Blackjack.Play.Dealer_Strategy;

namespace Blackjack.Play.Entities
{
    public class Game
    {
        private Shoe _shoe;
        private List<Player> _players;
        private DealerStrategy _dealerStrategy;
        private DealerHand _dealerHand = new DealerHand();
        private Dictionary<Player, PlayerHand> _playerCards = new Dictionary<Player, PlayerHand>();
        private Dictionary<Player, decimal> _beginningBalances = new Dictionary<Player, decimal>();
        private int _playerWins;
        private int _playerLosses;
        private int _playerPushes;
          
        Card DealerShowCard => _dealerHand.GetShowCard();

        public Game(Shoe shoe, List<Player> players, DealerStrategy dealerStrategy)
        {
            _dealerStrategy = dealerStrategy;
            _players = players;
            _shoe = shoe;
        }

        public GameResult Play()
        {
            var result = new GameResult();
            var hands = 0;
            while (!_shoe.IsDead())
            {
                SetBeginningBalance();
                Deal();
                OfferInsurance();
                DealerBlackjack();
                PlayerBlackJack();
                CompletePlayerHands();
                CompleteDealerHands();
                DetermineOutcomes();
                Pay();
                CalculateIntermediateOutcomes();
                ReportOutcomes();
                AddHandToResults(result);
                ClearHands();
                hands++;
            }
            return CalculateOutcomes(result,hands);
        }

        private void AddHandToResults(GameResult result)
        {
            _players.ForEach(a =>
            {
                result.AddHandOutcome(_playerCards[a],_dealerHand);
            });
        }

        private void SetBeginningBalance()
        {
            _beginningBalances = new Dictionary<Player, decimal>();
            foreach (var player in _players)
                _beginningBalances.Add(player,player.BankRoll);
        }

        private void ReportOutcomes()
        {
            if (!bool.Parse(ConfigurationManager.AppSettings["WriteOutcomesToDisk"]))
                return;
            var sb = new StringBuilder();

            sb.AppendLine("Dealer has: " + _dealerHand.CardNames());
            _players.ForEach(a => sb.AppendLine("Player " + a.Name + " has: " + _playerCards[a].CardNames()));
            sb.AppendLine("Dealer total: " + _dealerHand.Value());
            _players.ForEach(a => sb.AppendLine(PrintOutcomesToConsole(a)));
            _players.ForEach(a=> sb.AppendLine(string.Format("Player {0} began with {1} and ended with {2}", a.Name,_beginningBalances[a],a.BankRoll)));
            sb.AppendLine("----");
            File.AppendAllText(ConfigurationManager.AppSettings["DiskSaveLocation"], sb.ToString());
            //            Console.Write(sb);
        }

        private string PrintOutcomesToConsole(Player player)
        {
            var outcomeText = "(" + player.BankRoll + ") ";

            var cards = _playerCards[player];
            if (cards.Outcome == HandOutcomes.Winner)
                outcomeText += "Player " + (cards.IsDoubled ? " (DOUBLE) " : string.Empty) + player.Name + " WINS with " + cards.Value() + " versus " + _dealerHand.Value();

            if (cards.Outcome == HandOutcomes.Push)
                outcomeText += "Player " + (cards.IsDoubled ? " (DOUBLE) " : string.Empty) + player.Name + " PUSHES with " + cards.Value() + " versus " + _dealerHand.Value();

            if (cards.Outcome == HandOutcomes.Loser)
                outcomeText += "Player " + (cards.IsDoubled ? " (DOUBLE) " : string.Empty) + player.Name + " LOSES with " + cards.Value() + " versus " + _dealerHand.Value();


            return outcomeText;
        }


        private void PlayerBlackJack()
        {
            foreach (var player in _players)
            {
                if (_playerCards[player].IsBlackjack())
                {
                    _playerCards[player].WonBlackjack = true;
                    player.AwardPlayer(2.5m);
                    _playerCards[player].Kill();
                }
            }
        }

        private void CalculateIntermediateOutcomes()
        {
            _playerWins += _playerCards.Count(a => a.Value.Outcome == HandOutcomes.Winner);
            _playerLosses += _playerCards.Count(a => a.Value.Outcome == HandOutcomes.Loser);
            _playerPushes += _playerCards.Count(a => a.Value.Outcome == HandOutcomes.Push);
        }

        private GameResult CalculateOutcomes(GameResult result, int hands)
        {
            result.DecksInShoe = _shoe.DecksInShoe;
            result.TotalHands = hands;
            result.TotalWins = _playerWins;
            result.TotalLosses = _playerLosses;
            result.TotalPushes = _playerPushes;
            result.TotalWinAmount = _players.Sum(a => a.BankRoll);
            return result;
        }

        private void ClearHands()
        {
            _playerCards = new Dictionary<Player, PlayerHand>();
            _dealerHand = new DealerHand();
        }

        private void Pay()
        {
            foreach (var player in _players)
            {
                if (_playerCards[player].Outcome == HandOutcomes.Push)
                {
                    player.AwardPlayer(1);
                    if (_playerCards[player].IsDoubled)
                        player.AwardPlayer(1);
                }
                if (_playerCards[player].Outcome == HandOutcomes.Winner && !_playerCards[player].WonBlackjack)
                {
                    player.AwardPlayer(2);
                    if (_playerCards[player].IsDoubled)
                        player.AwardPlayer(2);
                }
            }

        }

        private void DetermineOutcomes()
        {
            foreach (var player in _players)
                _playerCards[player].AwardOutcomes(_dealerHand.Value());
        }

        private void CompleteDealerHands()
        {
            if (_playerCards.All(a => a.Value.IsBusted() || a.Value.IsKilled))
                return;
            while (!_dealerHand.IsComplete())
                _dealerHand.AddCard(_shoe.GetCardFromShoe());
        }

        private void CompletePlayerHands()
        {
            foreach (var player in _players)
            {
                var hand = _playerCards[player];
                if (!hand.IsDouble(player.Strategy, _dealerHand.GetShowCard()))
                {
                    while (!hand.IsKilled && !hand.IsComplete(player.Strategy, _dealerHand.GetShowCard()))
                        hand.AddCard(_shoe.GetCardFromShoe());
                }
                else
                {
                    player.ChargePlayer(1);
                    hand.Double();
                    hand.AddCard(_shoe.GetCardFromShoe());
                }
                if (hand.IsBusted())
                    hand.Kill();
            }
        }

        private void DealerBlackjack()
        {
            if (_dealerHand.IsBlackjack())
                CompleteHands();

        }

        private void CompleteHands()
        {
            foreach (var player in _players)
                _playerCards[player].Kill();
        }

        private void OfferInsurance()
        {
            if (DealerShowCard.IsAce)
                foreach (var player in _players)
                    player.OfferInsurance();
        }

        private void Deal()
        {
            foreach (var player in _players)
            {
                player.ChargePlayer(1m);
                var newHand = new PlayerHand();
                newHand.AddCard(_shoe.GetCardFromShoe(false, true));
                _playerCards.Add(player, newHand);
            };

            _dealerHand.AddCard(_shoe.GetCardFromShoe(true, true));

            foreach (var player in _players)
            {
                _playerCards[player].AddCard(_shoe.GetCardFromShoe(false, false));
            }

            _dealerHand.AddCard(_shoe.GetCardFromShoe(true, false));
        }
    }


}
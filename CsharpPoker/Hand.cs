using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpPoker
{
    public class Hand
    {
        private readonly List<Card> _cards = new List<Card>();
        
        public IEnumerable<Card> Cards
            => _cards;

        public void Draw(Card card)
            => _cards.Add(card);

        public Card HighCard()
            => _cards.Aggregate((a, b) => a.Value > b.Value ? a : b);

        public HandRank GetHandRank()
            => Rankings()
                .OrderByDescending(rule => rule.Rank)
                .First(r => r.Eval(Cards)).Rank;

        private IEnumerable<Ranker> Rankings()
            => new List<Ranker>
            {
                new Ranker(Cards => HasRoyalFlush(), HandRank.RoyalFlush),
                new Ranker(Cards => HasStraightFlush(), HandRank.StraightFlush),
                new Ranker(Cards => HasStraight(), HandRank.Straight),
                new Ranker(Cards => HasFlush(), HandRank.Flush),
                new Ranker(Cards => HasFullHouse(), HandRank.FullHouse),
                new Ranker(Cards => HasFourOfAKind(), HandRank.FourOfAKind),
                new Ranker(Cards => HasThreeOfAKind(), HandRank.ThreeOfAKind),
                new Ranker(Cards => HasTwoPair(), HandRank.TwoPair),
                new Ranker(Cards => HasPair(), HandRank.Pair),
                new Ranker(Cards => true, HandRank.HighCard)
            };

        private bool HasFlush()
            => Cards.All(c => c.Suit == Cards.First().Suit);

        private bool HasRoyalFlush()
            => HasFlush() 
               && Cards.All(c => c.Value > CardValue.Nine);

        private bool HasPair()
            => HasManyOfAKind(2);

        private bool HasThreeOfAKind() 
            => HasManyOfAKind(3);

        private bool HasFourOfAKind() 
            => HasManyOfAKind(4);

        private bool HasFullHouse() 
            => HasThreeOfAKind() && HasPair();
        
        private bool HasManyOfAKind(int howManyCards)
            => Cards
                .GroupBy(card => card.Value)
                .Any(group => group.Count() == howManyCards);

        private bool HasTwoPair()
            => Cards.GroupBy(card => card.Value)
                    .Count(group => group.Count() == 2) == 2;

        private bool HasStraight()
            => _cards.GetMinMaxDifference() == _cards.Count - 1;

        private bool HasStraightFlush()
            => HasStraight() && HasFlush();        
    }
}
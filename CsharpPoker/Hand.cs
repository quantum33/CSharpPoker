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

        public Hand Draw(Card card)
        {
            _cards.Add(card);
            return this;
        }

        public Card HighCard()
            => _cards.Aggregate((a, b) => a.Value > b.Value ? a : b);

        public HandRank GetHandRank()
            => HasRoyalFlush() ? HandRank.RoyalFlush
                : HasStraightFlush() ? HandRank.StraightFlush
                : HasStraight() ? HandRank.Straight
                : HasFlush() ? HandRank.Flush
                : HasFullHouse() ? HandRank.FullHouse
                : HasFourOfAKind() ? HandRank.FourOfAKind
                : HasThreeOfAKind() ? HandRank.ThreeOfAKind
                : HasPair() ? HandRank.Pair
                : HandRank.HighCard;

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

        private bool HasStraight()
            => _cards.GetMinMaxDifference() == _cards.Count - 1;

        private bool HasStraightFlush()
            => HasStraight() && HasFlush();
    }

    internal static class CardListExtensions
    {
        internal static int GetMinMaxDifference(this List<Card> cards)
            => cards.Max(card => card.Value) - cards.Min(card => card.Value);
    }
}
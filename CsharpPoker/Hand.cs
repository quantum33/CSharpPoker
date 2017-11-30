using System.Collections.Generic;
using System.Linq;

namespace CsharpPoker
{
    public class Hand
    {
        private readonly List<Card> _cards = new List<Card>();
        
        public IEnumerable<Card> Cards =>
            _cards;

        public Hand Draw(Card card)
        {
            _cards.Add(card);
            return this;
        }

        public Card HighCard() =>
            _cards.Single(card => card.Value == _cards.Max(c => c.Value));

        public HandRank GetHandRank() =>
            HasRoyalFlush() ? HandRank.RoyalFlush
            : HasFlush() ? HandRank.Flush
            : HandRank.HighCard;

        private bool HasFlush() =>
            Cards.All(c => c.Suit == Cards.First().Suit);

        private bool HasRoyalFlush() =>
            HasFlush()
            && Cards.All(c => c.Value > CardValue.Nine);
    }
}

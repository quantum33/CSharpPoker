using System;

namespace CsharpPoker
{
    public class Card
    {
        public Card(CardValue value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public CardValue Value { get; }
        public CardSuit Suit { get; }

        public override string ToString()
            => $"{Value} of {Suit}";

        public override bool Equals(object o)
        {
            if (!(o is Card card)) return false;
            if (card.Suit != this.Suit) return false;
            if (card.Value != this.Value) return false;

            return true;
        }
    }
}

using CsharpPoker;
using System;
using Xunit;

namespace CsharpPokerTests
{
    public class CardTests
    {
        [Fact]
        public void CanCreateCardWithValue()
        {
            var card = new Card(CardValue.Ace, CardSuit.Clubs);

            Assert.Equal(CardValue.Ace, card.Value);
            Assert.Equal(CardSuit.Clubs, card.Suit);
        }

        [Fact]
        public void CanDescribeCard()
        {
            var card = new Card(CardValue.Ace, CardSuit.Spades);
            Assert.Equal("Ace of Spades", card.ToString());
        }
    }
}

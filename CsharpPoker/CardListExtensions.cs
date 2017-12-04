using System.Collections.Generic;
using System.Linq;

namespace CsharpPoker
{
    internal static class CardListExtensions
    {
        internal static int GetMinMaxDifference(this List<Card> cards)
            => cards.Max(card => card.Value) - cards.Min(card => card.Value);
    }
}
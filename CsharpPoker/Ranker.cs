using System;
using System.Collections.Generic;

namespace CsharpPoker
{
    public class Ranker
    {
        public Func<IEnumerable<Card>, bool> Eval { get; }
        public HandRank Rank { get; }

        public Ranker(Func<IEnumerable<Card>, bool> eval, HandRank rank)
        {
            Eval = eval;
            Rank = rank;
        }
    }
}
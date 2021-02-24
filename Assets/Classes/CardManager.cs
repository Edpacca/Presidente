using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes
{
    public static class CardManager
    {
        public static void ShuffleCards<T>(IList<T> deck)
        {
            int count = deck.Count;
            int last = count - 1;

            for (int i = 0; i < last; i++)
            {
                var r = Random.Range(i, count);
                var buffer = deck[i];
                deck[i] = deck[r];
                deck[r] = buffer;
            }
        }
    }
}

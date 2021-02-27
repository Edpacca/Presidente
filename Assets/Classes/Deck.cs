using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Deck
    {
        public List<int> cards = new List<int>();

        public Deck(int deckSize)
        {
            for (int i = 0; i < deckSize; i++)
            {
                cards.Add(i);
            }
        }

        public void Shuffle()
        {
            int count = cards.Count;
            int last = count - 1;

            for (int i = 0; i < last; i++)
            {
                int r = Random.Range(i, count);
                int buffer = cards[i];
                cards[i] = cards[r];
                cards[r] = buffer;
            }
        }
    }
}

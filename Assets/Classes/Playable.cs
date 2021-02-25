using Assets.Classes;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes
{
    public static class Playable
    {
        public static bool IsValidPlay(Card card, int lastCardValue, int roundPlaySize)
        {
            return (card.Value > lastCardValue || card.Value == 1) && roundPlaySize == 1;
        }

        public static bool IsValidPlay(IEnumerable<Card> cards, int lastCardValue, int roundPlaySize)
        {
            int value;
            int currentPlaySize = cards.Count();
            bool isValidPlay = false;

            if (cards != null || currentPlaySize != 0)
            {
                value = cards.ElementAt(0).Value;

                if (cards.Select(c => c.Value == value).Count() == currentPlaySize && currentPlaySize == roundPlaySize)
                {
                    isValidPlay = (value == 1 || value > lastCardValue) ? true : false;
                }
            }

            return isValidPlay;
        }
    }
}

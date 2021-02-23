using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Card
    {
        public GameObject CardGameObject { get; set; }
        public int Value { get; }
        public int Index { get; }

        public Card(int index)
        {
            Index = index;
            Value = (Index + 1) % 13;
        }

        public void SetCardGameObject(GameObject card)
        {
            CardGameObject = card;
        }
    }
}

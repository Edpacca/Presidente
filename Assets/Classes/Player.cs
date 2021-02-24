using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Classes
{
    public class Player
    {
        public List<Card> Hand { get; }
        public int Score { get; set; }


        public void DealCard(Card card)
        {
            Hand.Add(card);
        }
    }
}

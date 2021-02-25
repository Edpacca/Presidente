using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Classes
{
    public class Player
    {
        public List<Card> Hand { get; } = new List<Card>();
        public List<Card> SelectedCards { get; set; } = new List<Card>();
        public int Score { get; set; }

        public void DealCard(Card card)
        {
            Hand.Add(card);
        }

        public void PlayCards()
        {
            foreach (var card in Hand)
            {
                if (SelectedCards.Contains(card))
                    Hand.Remove(card);
            }

            SelectedCards.Clear();
        }

        public void SetSelectedCards()
        {
            foreach (var card in Hand.Where(c => c.CardGameObject.GetComponent<SelectCard>().IsSelected))
            {
                SelectedCards.Add(card);
            }
        }
    }
}

using Assets.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Classes
{
    public class PlayManager
    {
        public int ActivePlayer { get; set; } = 0;
        public int NumberOfPlayers { get; }

        public PlayManager(int numberOfPlayers)
        {
            NumberOfPlayers = numberOfPlayers;
        }

        public void Update()
        {

        }


        public void TryPlayCards()
        {

        }

        private void PlayCard(Card card)
        {

        }

        private void PlayCards(List<Card> cards)
        {

        }

    }
}

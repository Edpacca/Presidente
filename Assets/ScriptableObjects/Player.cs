using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayer", menuName ="Objects/Player")]
public class Player : ScriptableObject
{
    public List<Card> hand = new List<Card>();
    public bool isActivePlayer;

    public void DealCard(Card card)
    {
        hand.Add(card);

        if (card.Index == GameSettings.startCardIndex)
            isActivePlayer = true;
    }
}

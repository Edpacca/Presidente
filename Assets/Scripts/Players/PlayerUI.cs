using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text cardsInHand;
    public Text playerName;
    private GameObject _handArea;

    public void Initialise(GameObject handArea)
    {
        _handArea = handArea;
    }

    public void UpdateCardsInHand(int number)
    {
        cardsInHand.text = number.ToString();
    }

    public void SetName(string name)
    {
        playerName.text = name;
    }

}

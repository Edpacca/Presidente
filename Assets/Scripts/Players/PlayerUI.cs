using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets;

public class PlayerUI : MonoBehaviour
{
    public Text cardsInHand;
    public Text playerName;
    private bool _isPassed;
    private string _playerName;

    private void OnEnable()
    {
        _playerName = playerName.text;
    }

    public void UpdateCardsInHand(int number)
    {
        cardsInHand.text = number.ToString();
    }

    public void SetName(string name)
    {
        playerName.text = name;
    }

    public void PassTurn(bool isPassed)
    {
        _isPassed = isPassed;

        if (_isPassed)
        {
            playerName.text = "PASSED";
            playerName.color = GameResources.playerRed;
        }
        else
        {
            playerName.text = _playerName;
            playerName.color = GameResources.playerGold;
        }
    }

    public void SetOutOfGame()
    {
        playerName.text = "OUT!";
        playerName.color = GameResources.playerGreen;
    }
}

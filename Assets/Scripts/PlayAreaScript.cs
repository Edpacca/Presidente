﻿using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAreaScript : MonoBehaviour
{
    public int lastPlayedValue = 0;
    public Multiplicity cardMultiplicity = Multiplicity.Singles;

    public Image image;
    public Text lastPlayedValueText;
    public GameController gameController;

    private void Update()
    {


        UpdateText();
    }

    public void SetNewSprite(Sprite sprite)
    {
        image.sprite = sprite;
        image.color = Color.white;
    }

    public void PlayCard(Card card)
    {
        SetNewSprite(card.cardSprite);
        lastPlayedValue = card.Value;
        CheckForBurn();
    }

    public void UpdateText()
    {
        int value = lastPlayedValue;
        string text = lastPlayedValue.ToString();

        if (value > 10)
        {
            switch(value)
            {
                case 11:
                    text = "J";
                    break;
                case 12:
                    text = "Q";
                    break;
                case 13:
                    text = "K";
                    break;
                case 14:
                    text = "A";
                    break;
            }
        }

        lastPlayedValueText.text = text;
    }

    public void BurnCardsInPlay()
    {
        lastPlayedValue = 0;
        SetNewSprite(null);
        image.color = GameResources.alphaWhite40;
    }

    private void CheckForBurn()
    {
        if (lastPlayedValue > 13)
        {
            BurnCardsInPlay();
            gameController.WinningPlayMade();
        }
        else
            gameController.PlayMade();
    }
}

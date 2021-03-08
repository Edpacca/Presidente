using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Card card;
    public Image image;
    public bool isPlayable = false;
    public bool isPlayed = false;

    public void SetPlayable(bool playable)
    {
        isPlayable = playable;

        if (isPlayable)
        {
            image.color = Color.white;
        }
        else
        {
            MarkUnplayable();
        }
    }

    public void ResetCard()
    {
        image.color = Color.white;
        isPlayable = false;
    }

    public void MarkUnplayable()
    {
        image.color = GameResources.fadedCard;
    }
}

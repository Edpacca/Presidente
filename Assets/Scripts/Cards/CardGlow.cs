using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGlow : MonoBehaviour
{
    public Image cardGlow;
    public CardScript cardScript;

    void Start()
    {
        cardGlow.enabled = false;        
    }

    public void OnEnterHover()
    {
        if (cardScript.isPlayable)
            cardGlow.enabled = true;
    }

    public void OnExitHover()
    {
        if (cardScript.isPlayable)
            cardGlow.enabled = false;
    }
}

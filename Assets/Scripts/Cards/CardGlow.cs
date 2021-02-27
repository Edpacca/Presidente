using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGlow : MonoBehaviour
{
    public Image cardGlow;

    void Start()
    {
        cardGlow.enabled = false;        
    }

    public void OnEnterHover()
    {
        cardGlow.enabled = true;
    }

    public void OnExitHover()
    {
        cardGlow.enabled = false;
    }
}

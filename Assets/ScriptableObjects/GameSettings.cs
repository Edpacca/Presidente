using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="GameSettings", menuName ="Controllers/GameSettings")]
public class GameSettings : ScriptableObject
{
    public int numberOfPlayers = 3;
    public readonly int deckSize = 52;
    public static readonly int startCardIndex = 27;

    public void SetNumberOfPlayers(string newValue)
    {
        numberOfPlayers = Convert.ToInt32(newValue);
    }
}

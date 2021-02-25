using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    private const int singleDeckSize = 52;
    public int numberOfPlayers = 5;
    public int numberOfDecks = 1;
    public int DeckSize { get => singleDeckSize * numberOfDecks; }

    public int DeckIndex(int index)
    {
        return index % singleDeckSize;
    }

    public Color32 activePlayColour = new Color(1.000f, 0.600f, 0.000f, 0.250f);
    public Color32 defaultAreaColour = new Color(1.000f, 1.000f, 1.000f, 0.250f);
}
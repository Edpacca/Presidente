using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewCard", menuName ="Objects/Player")]
public class Card : ScriptableObject
{
    public int Value { get; private set; }
    public int Index { get; private set; }
    public string Name { get; private set; }
    public Sprite cardSprite;

    public void Initialise(int index)
    {
        Index = index;
        int value = (index + 1) % 13;

        Value = value == 1 ? 14 : value == 0 ? 13 : value;
    }

    public void SetValue(int value)
    {
        Value = value;
    }

    public void SetIndex(int index)
    {
        Index = index;
    }

    public void SetName(string name)
    {
        Name = name;
    }
}

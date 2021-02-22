using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class DrawCards : MonoBehaviour
{
    public List<GameObject> Deck;
    public GameObject HandArea;
    public List<GameObject> Hand = new List<GameObject>();

    private int _handSize = 5;

    private bool _isHandDealt;

    void Start()
    {
    }

    public void StartGameClick()
    {
        if (!_isHandDealt)
        {
            for (int i = 0; i < _handSize; i++)
            {
                int index = GetRandomCard();

                while (Hand.Contains(Deck[index]))
                {
                    index = GetRandomCard();
                }

                GameObject cardInstance = Instantiate(Deck[index], new Vector3(i, 0, 0), Quaternion.identity);
                cardInstance.transform.SetParent(HandArea.transform);
                Hand.Add(cardInstance);
            }
            _isHandDealt = true;
        }
        else
        {
            foreach (var card in Hand)
            {
                Destroy(card);
            }
            
            _isHandDealt = false;
        }
    }

    private int GetRandomCard()
    {
        return Random.Range(0, Deck.Count - 1);
    }

}

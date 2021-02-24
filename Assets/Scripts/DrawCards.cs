using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Classes;
using UnityEngine.UI;
using System;

public class DrawCards : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject playerHand;
    public GameObject opponentHand;
    public GameObject opponentArea;

    public List<Player> Players = new List<Player>();
    public int NumberOfPlayers { get; } = 5;

    private List<Card> _cardDeck = new List<Card>();
    private List<GameObject> _playerHandAreas = new List<GameObject>();

    private readonly int _deckSize = 52;
    private readonly string _spritePath = "CardSprites/TempCards_";
    private bool _isHandDealt;

    void Start()
    {
        GeneratePlayers();
        _playerHandAreas.Add(playerHand);
        GenerateOpponentAreas();
        GenerateCards();
    }

    private void GeneratePlayers()
    {
        for (int i = 0; i < NumberOfPlayers; i++)
        {
            Players.Add(new Player());
        }
    }

    void Update()
    {
        
    }

    public void StartGameClick()
    {
        DealCards();
    }

    private void GenerateOpponentAreas()
    {
        for (int i = 0; i < NumberOfPlayers - 1; i++)
        {
            GameObject opponentHandArea = Instantiate(opponentHand, Vector3.zero, Quaternion.identity);
            opponentHandArea.transform.SetParent(opponentArea.transform, false);
            _playerHandAreas.Add(opponentHandArea);
        }
    }

    private void GenerateCards()
    {
        for (int i = 0; i < _deckSize; i++)
        {
            string path = _spritePath + i;
            SpriteRenderer cardSpriteRenderer = Resources.Load<SpriteRenderer>(path);

            _cardDeck.Add(new Card(i));
            _cardDeck[i].CardGameObject = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
            _cardDeck[i].CardGameObject.GetComponent<Image>().sprite = cardSpriteRenderer.sprite;
        }
    }

    private void DealCards()
    {
        CardManager.ShuffleCards(_cardDeck);

        if (!_isHandDealt)
        {
            for (int i = 0; i < _cardDeck.Count; i++)
            {
                for (int j = 0; j < Players.Count; j++)
                {
                    if (i < _cardDeck.Count)
                    {
                        GameObject dealtCard = Instantiate(_cardDeck[i].CardGameObject, Vector3.zero, Quaternion.identity);
                        dealtCard.transform.SetParent(_playerHandAreas[j].transform);
                        Players[j].DealCard(_cardDeck[i]);
                        i++;
                    }

                    i--;
                }
            }

            _isHandDealt = true;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using Assets.Classes;
using UnityEngine.UI;
using System.Collections;

public class GameSetup : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject playerHand;
    public GameObject opponentHand;
    public GameObject opponentArea;
    public GameObject deckArea;
    public GameObject deckImage;
    public GameStateManager gameState;
    public GameSettings gameSettings;

    private List<GameObject> _playerHandAreas = new List<GameObject>();
    private readonly string _spritePath = "CardSprites/TempCards_";
    private Vector2 _initialCardDimensions;

    private int _deckSize;
    private int _numberOfPlayers;
    private bool _isDeckDealt;

    void Start()
    {
        _deckSize = gameSettings.DeckSize;
        _numberOfPlayers = gameSettings.numberOfPlayers;

        GeneratePlayers();
        GeneratePlayerAreas();
        GenerateCards();

        gameState.GameIsSetUp();        
    }

    private void GeneratePlayers()
    {
        for (int i = 0; i < _numberOfPlayers; i++)
        {
            gameState.Players.Add(new Player());
        }
    }

    private void GeneratePlayerAreas()
    {
        _playerHandAreas.Add(playerHand);

        for (int i = 0; i < _numberOfPlayers - 1; i++)
        {
            GameObject newPlayerHandArea = Instantiate(opponentHand, Vector3.zero, Quaternion.identity);
            newPlayerHandArea.transform.SetParent(opponentArea.transform, false);
            _playerHandAreas.Add(newPlayerHandArea);
        }
    }

    private void GenerateCards()
    {
        for (int i = 0; i < _deckSize; i++)
        {
            int cardIndex = gameSettings.DeckIndex(i);
            string path = _spritePath + cardIndex;
            SpriteRenderer cardSpriteRenderer = Resources.Load<SpriteRenderer>(path);

            Card card = new Card(cardIndex);
            card.CardGameObject = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
            card.CardGameObject.GetComponent<Image>().sprite = cardSpriteRenderer.sprite;
            card.CardGameObject.transform.SetParent(deckArea.transform, false);

            gameState.AddCardToDeck(card);
        }

        _initialCardDimensions = gameState.Deck[0].CardGameObject.GetComponent<Image>().rectTransform.sizeDelta;
    }

    public void DealCards()
    {
        if (gameState.isGameSetup && !_isDeckDealt)
        {
            CardManager.ShuffleCards(gameState.Deck);
            int deckSize = gameSettings.DeckSize;
            int numberOfPlayers = gameSettings.numberOfPlayers;

            for (int i = 0; i < deckSize; i++)
            {
                for (int j = 0; j < numberOfPlayers; j++)
                {
                    if (i < deckSize)
                    {
                        GameObject dealtCard = gameState.Deck[i].CardGameObject;
                        dealtCard.transform.SetParent(_playerHandAreas[j].transform);
                        gameState.Players[j].DealCard(gameState.Deck[i]);
                        i++;
                    }
                }
                i--;
            }

            _isDeckDealt = true;
            deckImage.SetActive(!_isDeckDealt);
        }
        else if (_isDeckDealt)
        {
            foreach (var card in gameState.Deck)
            {
                card.CardGameObject.transform.SetParent(deckArea.transform, false);
                card.CardGameObject.transform.position = deckArea.transform.position;
                card.CardGameObject.GetComponent<Image>().rectTransform.sizeDelta = _initialCardDimensions;
            }

            _isDeckDealt = false;
            deckImage.SetActive(!_isDeckDealt);
        }
    }

    public void SetActivePlayerArea()
    {
        int lastActivePlayer = gameState.activePlayer - 1 < 0 ? gameSettings.numberOfPlayers - 1 : gameState.activePlayer - 1;
        _playerHandAreas[lastActivePlayer].GetComponent<Image>().color = gameSettings.defaultAreaColour;
        _playerHandAreas[gameState.activePlayer].GetComponent<Image>().color = gameSettings.activePlayColour;
    }
}

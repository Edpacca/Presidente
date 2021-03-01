using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameSetup : MonoBehaviour
{
    public GameSettings gameSettings;
    public GameController gameController;
    public GameLoop gameLoop;

    // Scene objects
    public GameObject opponentHandsArea;
    public GameObject localPlayerHand;
    public GameObject localPlayerUi;

    // Prefabs
    public GameObject playerUiPrefab;
    public GameObject localPlayerCardPrefab;
    public GameObject opponentHandPrefab;
    public GameObject opponentCardPrefab;

    private List<GameObject> _playerHands;
    private List<GameObject> _playerUis;
    private List<Player> _players;
    private Sprite[] cardSprites;
    private Deck _deck;
    private readonly string _cardSpriteFilePath = "CardSprites/TempCards";
    private int _deckSize;

    private void Start()
    {
        Initialise();
        LoadNewGame();
    }

    private void Initialise()
    {
        _players = new List<Player>();
        _playerHands = new List<GameObject>();
        _playerUis = new List<GameObject>();
        _deckSize = gameSettings.deckSize;
        _deck = new Deck(_deckSize);
        cardSprites = Resources.LoadAll<Sprite>(_cardSpriteFilePath);
    }

    public void LoadNewGame()
    {
        Initialise();
        int numberOfPlayers = gameSettings.numberOfPlayers;
        GeneratePlayers(numberOfPlayers);
        DealCards(numberOfPlayers);
        gameLoop.gameState = GameState.boardLoaded;
        gameController.StartPlay();
    }

    private void GeneratePlayers(int numberOfPlayers)
    {
        _playerHands.Add(localPlayerHand);
        _playerUis.Add(localPlayerUi);
        GeneratePlayAreas(numberOfPlayers);

        for (int i = 0; i < numberOfPlayers; i++)
        {
            _players.Add(ScriptableObject.CreateInstance<Player>());
            _players[i].Initialise(_playerHands[i], _playerUis[i]);
        }

        gameController.AssignPlayers(_players);
    }

    private void GeneratePlayAreas(int numberOfPlayers)
    {

        for (int i = 0; i < numberOfPlayers - 1; i++)
        {
            GameObject handArea = Instantiate(opponentHandPrefab, Vector3.zero, Quaternion.identity);
            GameObject playerUi = Instantiate(playerUiPrefab, Vector3.zero, Quaternion.identity);
            handArea.transform.SetParent(opponentHandsArea.transform);
            playerUi.transform.SetParent(opponentHandsArea.transform);
            _playerHands.Add(handArea);
            _playerUis.Add(playerUi);
        }
    }

    private void DealCards(int numberOfPlayers)
    {
        _deck.Shuffle();

        for (int i = 0; i < _deckSize; i++)
        {
            GameObject card0 = GeneratePlayerCard(_deck.cards[i], localPlayerCardPrefab, localPlayerHand);
            gameController.DealCardToPlayer(0, card0);
            i++;

            for (int j = 1; j < numberOfPlayers && i < _deckSize; j++, i++)
            {
                GameObject card1 = GenerateCard(_deck.cards[i], opponentCardPrefab, _playerHands[j]);
                gameController.DealCardToPlayer(j, card1);
            }
            i--;
        }

        UpdateCardsInHand();
    }

    private GameObject GenerateCard(int index, GameObject prefab, GameObject parentObject)
    {
        Card card = ScriptableObject.CreateInstance<Card>();
        card.Initialise(index);

        card.cardSprite = cardSprites[index];
        GameObject cardObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        cardObject.transform.SetParent(parentObject.transform);
        cardObject.GetComponent<CardScript>().card = card;
        cardObject.GetComponent<Image>().sprite = card.cardSprite;

        return cardObject;
    }

    private GameObject GeneratePlayerCard(int index, GameObject prefab, GameObject parentObject)
    {
        Card card = ScriptableObject.CreateInstance<Card>();
        card.Initialise(index);

        card.cardSprite = cardSprites[index];
        GameObject cardObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        cardObject.transform.SetParent(parentObject.transform);
        cardObject.GetComponent<CardScript>().card = card;
        cardObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = card.cardSprite;

        return cardObject;
    }

    private void UpdateCardsInHand()
    {
        foreach (var player in _players)
        {
            player.UpdateHandSize();
        }
    }
}

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

    public GameObject opponentHandsArea;
    public GameObject localPlayerHand;
    public GameObject opponentHandPrefab;
    private List<GameObject> _playerHands;

    public GameObject localPlayerCard;
    public GameObject opponentCard;

    private readonly string _cardSpriteFilePath = "CardSprites/TempCards";
    private Sprite[] cardSprites;
    private Deck _deck;
    private int _deckSize;

    private void Start()
    {
        Initialise();
        LoadNewGame();
    }

    private void Initialise()
    {
        _playerHands = new List<GameObject>();
        _deckSize = gameSettings.deckSize;
        _deck = new Deck(_deckSize);
        cardSprites = Resources.LoadAll<Sprite>(_cardSpriteFilePath);
    }

    public void LoadNewGame()
    {
        Initialise();
        int numberOfPlayers = gameSettings.numberOfPlayers;
        GeneratePlayers(numberOfPlayers);
        GeneratePlayAreas(numberOfPlayers);
        DealCards(numberOfPlayers);
        gameLoop.gameState = GameState.boardLoaded;
    }

    private void GeneratePlayers(int numberOfPlayers)
    {
        List<Player> players = new List<Player>();
        _playerHands.Add(localPlayerHand);

        for (int i = 0; i < numberOfPlayers; i++)
        {
            players.Add(ScriptableObject.CreateInstance<Player>());
        }

        gameController.AssignPlayers(players);
    }

    private void GeneratePlayAreas(int numberOfPlayers)
    {

        for (int i = 0; i < numberOfPlayers - 1; i++)
        {
            GameObject handArea = Instantiate(opponentHandPrefab, Vector3.zero, Quaternion.identity);
            handArea.transform.SetParent(opponentHandsArea.transform);
            _playerHands.Add(handArea);
        }
    }

    private void DealCards(int numberOfPlayers)
    {
        _deck.Shuffle();

        for (int i = 0; i < _deckSize; i++)
        {
            Card card0 = GeneratePlayerCard(_deck.cards[i], localPlayerCard, localPlayerHand);
            gameController.DealCardToPlayer(0, card0);
            i++;

            for (int j = 1; j < numberOfPlayers && i < _deckSize; j++, i++)
            {
                Card card1 = GenerateCard(_deck.cards[i], opponentCard, _playerHands[j]);
                gameController.DealCardToPlayer(j, card1);
            }
            i--;
        }
    }

    private Card GenerateCard(int index, GameObject prefab, GameObject parentObject)
    {
        Card card = ScriptableObject.CreateInstance<Card>();
        card.Initialise(index);

        card.cardSprite = cardSprites[index];
        GameObject cardObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        cardObject.transform.SetParent(parentObject.transform);
        cardObject.GetComponent<CardScript>().card = card;
        cardObject.GetComponent<Image>().sprite = card.cardSprite;

        return card;
    }

    private Card GeneratePlayerCard(int index, GameObject prefab, GameObject parentObject)
    {
        Card card = ScriptableObject.CreateInstance<Card>();
        card.Initialise(index);

        card.cardSprite = cardSprites[index];
        GameObject cardObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        cardObject.transform.SetParent(parentObject.transform);
        cardObject.GetComponent<CardScript>().card = card;
        cardObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = card.cardSprite;

        return card;
    }
}

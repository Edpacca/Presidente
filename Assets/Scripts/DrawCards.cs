using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.UI;

public class DrawCards : MonoBehaviour
{
    public List<GameObject> GameObjectDeck;
    public GameObject PlayerHand;

    public GameObject OpponentHand;
    public GameObject Canvas;

    private List<GameObject> _playerHandAreas = new List<GameObject>();
    private List<Card> _cardDeck = new List<Card>();

    private bool _isHandDealt;

    public int NumberOfPlayers = 5;
    public int HandSize;

    private List<Vector3> opponentHandPositions;

    void Start()
    {
        opponentHandPositions = GenerateOpponentHandPositions(NumberOfPlayers - 1, 3.5f, 20);
        _playerHandAreas.Add(PlayerHand);
        GenerateOpponentAreas();
        GenerateCards();
    }

    public void StartGameClick()
    {
        ShuffleCards(_cardDeck);

        if (!_isHandDealt)
        {
            for (int i = 0; i < _cardDeck.Count; i++)
            {
                foreach (var playerArea in _playerHandAreas)
                {
                    if (i < _cardDeck.Count)
                    {
                        GameObject dealtCard = Instantiate(GameObjectDeck[_cardDeck[i].Index], Vector3.zero, Quaternion.identity);
                        dealtCard.transform.SetParent(playerArea.transform);
                        dealtCard.transform.localScale = new Vector3(1, 1, 1);
                        _cardDeck[i].SetCardGameObject(dealtCard);
                        i++;
                    }
                }
            }
            _isHandDealt = true;
        }
        else
        {
            foreach (var card in _cardDeck)
            {
                Destroy(card.CardGameObject);
            }
            
            _isHandDealt = false;
        }
    }

    private void GenerateOpponentAreas()
    {
        for (int i = 0; i < NumberOfPlayers - 1; i++)
        {
            GameObject opponentHandArea = Instantiate(OpponentHand, opponentHandPositions[i], Quaternion.identity);
            opponentHandArea.transform.SetParent(Canvas.transform);
            opponentHandArea.transform.localScale = new Vector3(1, 1, 1);
            _playerHandAreas.Add(opponentHandArea);
        }
    }

    private List<Vector3> GenerateOpponentHandPositions(int numberOfOpponents, float height, float maxWidth)
    {
        List<Vector3> handPositions = new List<Vector3>();

        float xInterval = maxWidth / (numberOfOpponents + 1);
        float xPosition = -(maxWidth / 2) + xInterval;

        for (int i = 0; i < numberOfOpponents; i++)
        {
            handPositions.Add(new Vector3(xPosition, height, 0));
            xPosition += xInterval;
        }

        return handPositions;
    }

    private void GenerateCards()
    {
        for (int i = 0; i < GameObjectDeck.Count; i++)
        {
            _cardDeck.Add(new Card(i));
        }
    }

    private static void ShuffleCards<T>(IList<T> deck)
    {
        int count = deck.Count;
        int last = count - 1;

        for (int i = 0; i < last; i++)
        {
            var r = Random.Range(i, count);
            var buffer = deck[i];
            deck[i] = deck[r];
            deck[r] = buffer;
        }
    }
}

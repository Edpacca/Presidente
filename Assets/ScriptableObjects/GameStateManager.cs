using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="GameState", menuName="States/GameState")]
public class GameStateManager : ScriptableObject
{
    public int lastPlayedCardValue = 0;
    public bool isDeckDealt = false;
    public bool isGameSetup = false;

    public int activePlayer = 0;

    public GameObject mainPlayArea;
    public GameSettings gameSettings;

    private Vector2 _mainCardDimensions;

    public List<Card> Deck { get; } = new List<Card>();
    public List<Player> Players { get; set; } = new List<Player>();

    private void OnEnable()
    {
        mainPlayArea = GameObject.Find("MainPlayArea");
    }

    public void AddCardToDeck(Card card)
    {
        Deck.Add(card);
    }

    public void GameIsSetUp()
    {
        isGameSetup = true;
        activePlayer = Random.Range(0, gameSettings.numberOfPlayers);
        _mainCardDimensions = Deck[0].CardGameObject.GetComponent<Image>().rectTransform.sizeDelta;
    }

    public void SetNewTurn()
    {
        foreach (var card in Players[activePlayer].Hand)
            card.CardGameObject.GetComponent<SelectCard>().IsPlayable = false;

        NextPlayer();

        foreach (var card in Players[activePlayer].Hand)
            card.CardGameObject.GetComponent<SelectCard>().IsPlayable = true;

    }

    private void NextPlayer()
    {
        activePlayer = (activePlayer + 1) % gameSettings.numberOfPlayers;
    }

    public void PlaySelectedCards()
    {
        Players[activePlayer].SetSelectedCards();
        SubmitSelectedCards();
        Players[activePlayer].PlayCards();
    }

    private void SubmitSelectedCards()
    {
        var selectedCards = Players[activePlayer].SelectedCards;

        foreach (var card in selectedCards)
        {
            card.CardGameObject.transform.SetParent(mainPlayArea.transform);
            card.CardGameObject.transform.position = mainPlayArea.transform.position;
            card.CardGameObject.GetComponent<Image>().rectTransform.sizeDelta = _mainCardDimensions;
        }

        SetNewTurn();
    }
}

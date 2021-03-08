using System.Collections;
using Assets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName ="newPlayer", menuName ="Objects/Player")]
public class Player : ScriptableObject
{

    public List<GameObject> hand = new List<GameObject>();
    public bool isActivePlayer;
    public bool isOutOfGame;
    public GameObject playerUi;
    private PlayerUI _uiScript;
    private GameObject _handArea;
    private int _highestValueInPlay;
    public bool isPresident;
    public bool isScum;

    public void DealCard(GameObject card)
    {
        hand.Add(card);
        CheckIfFirstPlayer(card);
    }

    public void Initialise(GameObject handArea, GameObject playerUiObject)
    {
        _handArea = handArea;
        playerUi = playerUiObject;
        _uiScript = playerUi.GetComponent<PlayerUI>();
    }

    public void UpdateHandSize()
    {
        int handSize = hand.Count;
        _uiScript.UpdateCardsInHand(handSize);

        if (handSize == 0)
            SetOutOfGame();
    }

    private void CheckIfFirstPlayer(GameObject card)
    {
        if (GetCard(card).Index == GameSettings.startCardIndex)
        {
            isActivePlayer = true;
            GetScript(card).SetPlayable(true);
            _handArea.GetComponent<Image>().color = GameResources.alphaPink;
        }
    }

    public void SetStartHand()
    {
        foreach (var card in hand)
        {
            if (GetCard(card).Value != 2)
                GetScript(card).MarkUnplayable();
        }
    }

    private void CheckForPlayedCards()
    {
        List<GameObject> playedCards = new List<GameObject>();

        foreach (var card in hand)
        {
            if (GetScript(card).isPlayed == true)
                playedCards.Add(card);
        }

        foreach (var card in playedCards)
        {
            hand.Remove(card);
        }

        UpdateHandSize();
    }

    public void SetActivePlayer(int highestCardInPlay)
    {
        _highestValueInPlay = highestCardInPlay;
        isActivePlayer = true;
        _handArea.GetComponent<Image>().color = GameResources.alphaPink;
        _uiScript.PassTurn(false);
        ActivateHand();
    }

    public void UnsetActivePlayer()
    {
        isActivePlayer = false;
        _handArea.GetComponent<Image>().color = GameResources.alphaWhite30;
        CheckForPlayedCards();
        DeactivateHand(hand);
    }

    private void ActivateHand()
    {
        foreach (var card in hand)
        {
            bool isPlayable = false;

            if (GetCard(card).Value > _highestValueInPlay)
                isPlayable = true;

            GetScript(card).SetPlayable(isPlayable);
        }
    }

    private void DeactivateHand(List<GameObject> playerHand)
    {
        foreach (var card in playerHand)
        {
            GetScript(card).ResetCard();
        }
    }

    public void DEBUG_SplurgeHand()
    {
        foreach (var card in hand)
        {
            card.transform.parent = null;
        }

        hand.Clear();
    }

    public void PassTurn()
    {
        _uiScript.PassTurn(true);
    }

    public void Reset()
    {
        _uiScript.PassTurn(false);
        _highestValueInPlay = 0;

        if (isActivePlayer)
        {
            ActivateHand();
        }
    }

    public void SetOutOfGame()
    {
        isOutOfGame = true;
        _uiScript.SetOutOfGame();
    }

    private Card GetCard(GameObject cardObject) => cardObject.GetComponent<CardScript>().card;
    private CardScript GetScript(GameObject cardObject) => cardObject.GetComponent<CardScript>();
}

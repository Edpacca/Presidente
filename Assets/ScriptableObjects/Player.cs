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
    private GameObject _handArea;
    public bool isActivePlayer;
    public GameObject playerUi;
    private PlayerUI UiScript;

    public void DealCard(GameObject card)
    {
        hand.Add(card);
        CheckIfFirstPlayer(card);
    }

    public void Initialise(GameObject handArea, GameObject playerUiObject)
    {
        _handArea = handArea;
        playerUi = playerUiObject;
        UiScript = playerUi.GetComponent<PlayerUI>();
    }

    public void UpdateHandSize()
    {
        UiScript.UpdateCardsInHand(hand.Count);
    }

    public void CheckIfFirstPlayer(GameObject card)
    {
        if (GetCard(card).Index == GameSettings.startCardIndex)
        {
            isActivePlayer = true;
            GetScript(card).isPlayable = true;
            _handArea.GetComponent<Image>().color = GameResources.alphaPink;
        }
    }

    public void CheckForPlayedCards()
    {
        foreach (var card in hand)
        {
            if (GetScript(card).isPlayed == true)
                hand.Remove(card);
        }

        UpdateHandSize();
    }

    public void SetActivePlayer()
    {
        isActivePlayer = true;
        _handArea.GetComponent<Image>().color = GameResources.alphaPink;
        ActivateHand();
    }

    public void UnsetActivePlayer()
    {
        isActivePlayer = false;
        _handArea.GetComponent<Image>().color = GameResources.alphaWhite;
        CheckForPlayedCards();
        DeactivateHand(hand);
    }

    private void ActivateHand()
    {
        foreach (var card in hand)
            GetScript(card).isPlayable = true;
    }

    private void DeactivateHand(List<GameObject> playerHand)
    {
        foreach (var card in playerHand)
            GetScript(card).isPlayable = false;
    }

    private Card GetCard(GameObject cardObject) => cardObject.GetComponent<CardScript>().card;
    private CardScript GetScript(GameObject cardObject) => cardObject.GetComponent<CardScript>();
}

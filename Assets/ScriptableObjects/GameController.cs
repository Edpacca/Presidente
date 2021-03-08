using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="GameController", menuName ="Controllers/GameController")]
public class GameController : ScriptableObject
{
    public GameSettings gameSettings;
    public GameLoop gameLoop;

    private PlayAreaScript playAreaScript;
    private PassButtonScript passButtonScript;

    protected List<Player> players;
    public int ActivePlayer;
    public int numberOfPlayers;
    private int numberOfPassedPlayers;

    public void AssignPlayers(List<Player> newPlayers)
    {
        if (players != null)
            players.Clear();

        players = newPlayers;
        numberOfPlayers = players.Count;
        GetGameObjectReferences();
    }

    public void DealCardToPlayer(int playerId, GameObject card)
    {
        if (playerId >= players.Count)
            return;

        players[playerId].DealCard(card);
    }

    public void StartPlay()
    {
        Player startPlayer = players.SingleOrDefault(p => p.isActivePlayer);
        ActivePlayer = players.IndexOf(startPlayer);
        startPlayer.SetStartHand();
        gameLoop.gameState = GameState.newGame;
        passButtonScript.SetActive(false);
    }

    private void TurnTaken()
    {
        players[ActivePlayer].UnsetActivePlayer();
        NextTurn();
        players[ActivePlayer].SetActivePlayer(playAreaScript.lastPlayedValue);
    }

    private void NextTurn()
    {
        ActivePlayer = (ActivePlayer + 1) % numberOfPlayers;
        if (players[ActivePlayer].isOutOfGame)
            NextTurn();
    }

    public void PassTurn()
    {
        players[ActivePlayer].PassTurn();
        numberOfPassedPlayers++;
        CheckForBurn();
        TurnTaken();
    }

    private void CheckForBurn()
    {
        if (numberOfPassedPlayers == numberOfPlayers - 1)
            playAreaScript.BurnCardsInPlay();
    }

    public void PlayMade()
    {
        if (gameLoop.gameState != GameState.inPlay)
        {
            gameLoop.gameState = GameState.inPlay;
            passButtonScript.SetActive(true);
        }

        numberOfPassedPlayers = players.Where(p => p.isOutOfGame).Count();
        TurnTaken();
        CheckForEndOfGame();
    }

    private void GetGameObjectReferences()
    {
        playAreaScript = GameObject.Find("PlayArea").GetComponent<PlayAreaScript>();
        passButtonScript = GameObject.Find("PassButton").GetComponent<PassButtonScript>();
    }

    public void WinningPlayMade()
    {
        gameLoop.gameState = GameState.newRound;
        passButtonScript.SetActive(false);

        foreach (var player in players)
        {
            player.Reset();
        }
    }

    public void DEBUG_Splurge()
    {
        players[ActivePlayer].DEBUG_SplurgeHand();
        PlayMade();
    }

    public void CheckForEndOfGame()
    {
        if (players.Where(p => p.isOutOfGame).Count() == numberOfPlayers - 1)
            gameLoop.gameState = GameState.gameOver;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="GameController", menuName ="Controllers/GameController")]
public class GameController : ScriptableObject
{
    public GameSettings gameSettings;
    protected List<Player> players;
    public int ActivePlayer;
    public int numberOfPlayers;

    public void AssignPlayers(List<Player> newPlayers)
    {
        if (players != null)
            players.Clear();

        players = newPlayers;
        numberOfPlayers = players.Count;
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
    }

    public void TurnTaken()
    {
        players[ActivePlayer].UnsetActivePlayer();
        NextTurn();
        players[ActivePlayer].SetActivePlayer();
    }

    private void NextTurn()
    {
        ActivePlayer = (ActivePlayer + 1) % numberOfPlayers;
    }

    private void CheckForPresident(Player player)
    {

    }
}

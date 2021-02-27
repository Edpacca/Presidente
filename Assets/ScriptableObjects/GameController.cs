using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameController", menuName ="Controllers/GameController")]
public class GameController : ScriptableObject
{
    public GameSettings gameSettings;
    protected List<Player> players; 

    public void AssignPlayers(List<Player> newPlayers)
    {
        if (players != null)
            players.Clear();

        players = newPlayers;
    }

    public void DealCardToPlayer(int playerId, Card card)
    {
        if (playerId >= players.Count)
            return;

        players[playerId].DealCard(card);
    }
}

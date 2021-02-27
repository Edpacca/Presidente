using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameLoop", menuName ="Controllers/GameLoop")]
public class GameLoop : ScriptableObject
{
    public GameState gameState = GameState.inMenu;

    public void GameFlow(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.newGame:
                {
                    break;
                }
            case GameState.boardLoaded:
                {
                    break;
                }
        }
    }

}

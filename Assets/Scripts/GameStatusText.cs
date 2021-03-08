using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using UnityEngine.UI;

public class GameStatusText : MonoBehaviour
{
    public GameLoop gameLoop;
    public Text statusText;
    private string newGame = "New Game!";
    private string gameOver = "Game Over!";

    void Update()
    {
        if (gameLoop.gameState == GameState.inPlay)
            gameObject.SetActive(false);

        if (gameLoop.gameState == GameState.gameOver)
        {
            gameObject.SetActive(true);
            statusText.text = gameOver;
        }
    }
}

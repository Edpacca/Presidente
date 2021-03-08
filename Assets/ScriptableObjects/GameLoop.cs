using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="GameLoop", menuName ="Controllers/GameLoop")]
public class GameLoop : ScriptableObject
{
    public Text statusText;
    public GameState gameState = GameState.inMenu;

}

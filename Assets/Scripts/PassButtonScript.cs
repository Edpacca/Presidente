using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class PassButtonScript : MonoBehaviour
{
    public GameLoop gameLoop;
    public GameController gameController;

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

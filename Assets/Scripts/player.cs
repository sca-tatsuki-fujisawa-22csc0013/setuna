using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] GameController gameController;
    public static bool play;

    void Start()
    {
        play = false;
    }

    void Update()
    {
        //if (gameController.State == GameController.Game.wait)
        //{
        //    play = false;
        //}
        if (gameController.State == GameController.Game.play)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                play = true;
            }
        }
    }
}

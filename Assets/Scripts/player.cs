using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] GameController gameController;
    public static bool play;

    void Start()
    {
        
    }

    void Update()
    {
        if(gameController.State == GameController.Game.play)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                play = true;
            }
        }

        if (gameController.State == GameController.Game.start)
        {
            Debug.Log("start");
            play = false;
        }
    }
}

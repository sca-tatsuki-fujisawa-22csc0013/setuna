using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] GameController gameController;
    public static bool play;
    float waitTime;
    bool wait;
    
    void Start()
    {
        play = false;
    }

    void Update()
    {
        if (gameController.State == GameController.Game.wait)
        {
            //play = false;
            wait = false;
            waitTime = 0;
        }
        else if (gameController.State == GameController.Game.play)
        {
            switch (Title.Select)
            {
                case 1:
                    if (!wait)
                    {
                        wait = true;
                        waitTime = Random.Range(0.21f, 0.23f);
                        Debug.Log(waitTime);
                    }
                    CPU();
                    break;
                case 2:
                    OnPlay();
                    break;
            }
        }
    }

    private void CPU()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0.0f)
        {
            play = true;
        }
    }

    private void OnPlay()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            play = true;
        }
    }
}

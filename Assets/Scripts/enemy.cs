using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] GameController gameController;
    public static bool play;
    int waitFrame;
    bool wait;
    int nowFrame;
    
    void Start()
    {
        
    }

    void Update()
    {
        switch (Title.Select)
        {
            case 1:
                CPU();
                break;
            case 2:
                OnPlay();
                break;
        }
        if (gameController.State == GameController.Game.start)
        {
            play = false;
            wait = false;
            waitFrame = 0;
        }
    }

    private void CPU()
    {
        if (gameController.State == GameController.Game.play)
        {
            if (!wait)
            {
                wait = true;
                waitFrame = Random.Range(15,46);
                nowFrame = Time.frameCount;
            }
            if (waitFrame <= Time.frameCount - nowFrame)
            {
                play = true;
                wait = false;
            }
        }
    }

    private void OnPlay()
    {
        if (gameController.State == GameController.Game.play)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                play = true;
            }
        }
    }
}

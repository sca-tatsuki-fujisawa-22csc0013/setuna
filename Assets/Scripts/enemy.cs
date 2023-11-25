using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    [SerializeField] GameController gameController;
    public static bool play;
    float waitTime;
    bool wait;
    private GameController _GC;
    [SerializeField] Text tx;

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
                        _GC = GameObject.Find("GameController").GetComponent<GameController>();
                        if (_GC.EScore < _GC.PScore)
                        {
                            waitTime = Random.Range(0.225f, 0.235f);
                        }
                        else if (_GC.EScore == _GC.PScore)
                        {
                            waitTime = Random.Range(0.235f, 0.24f);
                        }
                        else if (_GC.EScore > _GC.PScore)
                        {
                            waitTime = Random.Range(0.24f, 0.245f);
                        }
                        tx.text = waitTime.ToString("f3");
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
        if (player.play == false)
        {
            waitTime -= Time.deltaTime;
            tx.text = waitTime.ToString("f3");
            if (waitTime <= 0.0f)
            {
                play = true;
            }
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

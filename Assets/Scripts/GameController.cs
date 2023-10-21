using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public enum Game
    {
        start,
        wait,
        play,
        judge,
    };

    private enum Result
    {
        Draw,
        Win,
        Lose,
    };

    Game state;
    public Game State => state;
    float waitTime;
    bool chack;
    int frameCount;
    int nowFrame;
    [SerializeField] Text frameText;
    [SerializeField] Image[] judgeImage;
    [SerializeField] GameObject[] Enemy;


    // Start is called before the first frame update
    void Start()
    {
        state = Game.start;
        if (Title.Select == 1)
        {
            Enemy[1].SetActive(false);
        }
        else if(Title.Select == 2)
        {
            Enemy[0].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case Game.start:
                start();
                break;
            case Game.wait:
                wait();
                break;
            case Game.play:
                play();
                break;
            case Game.judge:
                break;
        }
    }

    void start()
    {
        chack = false;
        frameCount = 0;
        frameText.text = "";
        for (int i = 0; i < 3; ++i)
        {
            judgeImage[i].enabled = false;
        }
        state = Game.wait;
    }

    void wait()
    {
        if (!chack)
        {
            waitTime = Random.Range(1.0f, 1.5f);
            chack = true;
        }
        waitTime -= Time.deltaTime;
        if(waitTime < 0.0f)
        {
            state = Game.play;
            nowFrame = Time.frameCount;
        }
    }

    void play()
    {
        frameCount = Time.frameCount - nowFrame;
        frameText.text = frameCount.ToString();
        if(player.play == true || enemy.play == true)
        {
            state = Game.judge;
            StartCoroutine(judge());
        }
    }

    IEnumerator judge()
    {
        var result = Result.Draw;
        if(player.play == true && enemy.play == true)
        {
            result = Result.Draw;
        }
        else if (player.play == true && enemy.play == false)
        {
            result = Result.Win;
        }
        else if (player.play == false && enemy.play == true)
        {
            result = Result.Lose;
        }

        if (result == Result.Draw)
        {
            judgeImage[0].enabled = true;
        }
        else if (result == Result.Win)
        {
            judgeImage[1].enabled = true;
        }
        else if (result == Result.Lose)
        {
            judgeImage[2].enabled = true;
        }
        yield return new WaitForSeconds(0.5f);

        state = Game.start;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public enum Game
    {
        start,
        wait,
        play,
        judge,
        end,
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
    [SerializeField] Text TimeText;
    [SerializeField] Image[] judgeImage;
    [SerializeField] Text[] PointText;
    [SerializeField] Text _text;

    [SerializeField] Image[] PImage;
    [SerializeField] Image[] EImage;

    public int EScore;
    public int PScore;
    int GamePoint;

    int _1POtetuki;
    int _2POtetuki;

    [SerializeField] AudioClip[] Sounds;
    [SerializeField] AudioSource _audio;

    [SerializeField] GameObject button;
    [SerializeField] Text _Text;
    [SerializeField] Text[] PointTextBack;
    [SerializeField] Text _timetext;


    // Start is called before the first frame update
    private void Awake()
    {
        _Text.text = "";
        _timetext.text = "";
        for(int i = 0;i < 2; ++i)
        {
            PointTextBack[i].text = PointText[i].text;
        }
    }

    void Start()
    {
        state = Game.start;
        EScore = 0;
        PScore = 0;
        GamePoint = 0;
        PointText[0].text = PScore.ToString();
        PointText[1].text = EScore.ToString();
        _1POtetuki = 0;
        _2POtetuki = 0;
        _audio = GetComponent<AudioSource>();
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _Text.text = _text.text;
        _timetext.text = TimeText.text;
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
            case Game.end:
                break;
        }
    }

    void start()
    {
        chack = false;
        frameCount = 0;
        TimeText.text = "";
        _text.text = "";
        for (int i = 0; i < 3; ++i)
        {
            judgeImage[i].enabled = false;
        }
        for(int i = 0; i < 2; ++i)
        {
            PImage[i].enabled = false;
            EImage[i].enabled = false;
        }
        _audio.PlayOneShot(Sounds[4]);
        state = Game.wait;
    }

    void wait()
    {
        if (!chack)
        {
            waitTime = Random.Range(1.5f, 3.0f);
            chack = true;
            player.play = false;
            enemy.play = false;
        }
        waitTime -= Time.deltaTime;
        if (waitTime > 0.0f)
        {
            if (Title.Select == 2)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    _audio.PlayOneShot(Sounds[0]);
                    ++_2POtetuki;
                    _text.text = "お手付き！";
                    if (_2POtetuki == 2)
                    {
                        _2POtetuki = 0;
                        player.play = true;
                    }
                    state = Game.judge;
                    StartCoroutine(judge());
                    return;
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _audio.PlayOneShot(Sounds[0]);
                ++_1POtetuki;
                _text.text = "お手付き！";
                if (_1POtetuki == 2)
                {
                    _1POtetuki = 0;
                    enemy.play = true;
                }
                state = Game.judge;
                StartCoroutine(judge());
                return;
            }
        }
        if (waitTime <= 0.0f)
        {
            TimeText.text = "!";
            _audio.PlayOneShot(Sounds[5]);
            state = Game.play;
        }
    }

    void play()
    {
        if(player.play == true || enemy.play == true)
        {
            state = Game.judge;
            StartCoroutine(judge());
        }
    }

    IEnumerator judge()
    {
        var result = Result.Draw;
        if (player.play == true && enemy.play == false)
        {
            result = Result.Win;
        }
        else if (player.play == false && enemy.play == true)
        {
            result = Result.Lose;
        }
        else if (player.play == true && enemy.play == true)
        {
            result = Result.Draw;
        }

        if (result == Result.Win)
        {
            judgeImage[0].enabled = true;
            PImage[0].enabled = true;
            EImage[1].enabled = true;
            ++PScore;
            PointText[0].text = PScore.ToString();
            PointTextBack[0].text = PointText[0].text;
            ++GamePoint;
            _audio.PlayOneShot(Sounds[1]);
        }
        else if (result == Result.Lose)
        {
            judgeImage[1].enabled = true;
            PImage[1].enabled = true;
            EImage[0].enabled = true;
            ++EScore;
            PointText[1].text = EScore.ToString();
            PointTextBack[1].text = PointText[1].text;
            ++GamePoint;
            _audio.PlayOneShot(Sounds[2]);
        }
        else if (result == Result.Draw)
        {
            judgeImage[2].enabled = true;
            PImage[1].enabled = true;
            EImage[1].enabled = true;
            if (_text.text != "お手付き！")
            {
                _text.text = "御見合";
            }
            _audio.PlayOneShot(Sounds[0]);
        }
            yield return new WaitForSeconds(0.8f);

        _audio.PlayOneShot(Sounds[3]);
        if (result == Result.Draw)
        {
            _audio.PlayOneShot(Sounds[3]);
        }

        if (GamePoint == 7)
        {
            for (int i = 0; i < 2; ++i)
            {
                PImage[i].enabled = false;
                EImage[i].enabled = false;
            }
            if (Title.Select == 1)
            {
                if (PScore > EScore)
                {
                    _text.text = "プレイヤーの勝ち！";
                }
                else
                {
                    _text.text = "CPUの勝ち！";
                }
            }
            else
            {
                if (PScore > EScore)
                {
                    _text.text = "1Pの勝ち！";
                }
                else
                {
                    _text.text = "2Pの勝ち！";
                }
            }

            if (PScore > EScore)
            {
                PImage[0].enabled = true;
                EImage[1].enabled = true;
            }
            else
            {
                PImage[1].enabled = true;
                EImage[0].enabled = true;
            }
            StartCoroutine(EndWave());
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            state = Game.start;
        }
    }

    IEnumerator EndWave()
    {
        yield return new WaitForSeconds(1.5f);
        button.SetActive(true);
    }

    public void GameEnd(int num)
    {
        switch (num)
        {
            case 1:
                SceneManager.LoadScene("MainScene");
                break;
            case 2:
                SceneManager.LoadScene("TitleScene");
                break;
        }
    }
}

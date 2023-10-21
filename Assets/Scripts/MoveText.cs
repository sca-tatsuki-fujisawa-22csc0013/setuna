using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveText : MonoBehaviour
{
    float Width,Height;
    float x,y;
    float startX,startY;
    float endX,endY;
    float nowTime,moveTime;
    float time;
    [SerializeField] GameObject[] button;

    RectTransform _rT;

    // Start is called before the first frame update
    void Start()
    {
        Width = Screen.width;
        Height = Screen.height;
        _rT = GetComponent<RectTransform>();
        x = -Width;
        y = 0;
        MoveStart(0.0f, 0.0f, 1.5f);
        time = 0.0f;
        for(int i = 0; i < 2; ++i)
        {
            button[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nowTime < moveTime)
        {
            nowTime += Time.deltaTime;
            if (nowTime > moveTime)
            {
                nowTime = moveTime;
            }
            x = easeOutBack(nowTime / moveTime) * (endX - startX) + startX;
            //y = easeOutBounce(nowTime / moveTime) * (endY - startY) + startY;
            _rT.anchoredPosition = new Vector2(x, y);
        }
        else
        {
            time += Time.deltaTime;
            y = Mathf.Sin(time) * 20;
            _rT.anchoredPosition = new Vector2(x, y);
            for (int i = 0; i < 2; ++i)
            {
                button[i].SetActive(true);
            }
        }
    }

    void MoveStart(float toX, float toY,float time)
    {
        if(time <= 0.0f)
        {
            _rT.anchoredPosition = new Vector2(toX,toY);
            return;
        }
        else
        {
            startX = x;
            startY = y;
            endX = toX;
            endY = toY;
        }
        moveTime = time;
        nowTime = 0.0f;
        return;
    }

    float easeOutBack(float x)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1.0f;

        return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
    }
}

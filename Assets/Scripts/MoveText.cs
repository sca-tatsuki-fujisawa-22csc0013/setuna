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

    RectTransform _rT;

    // Start is called before the first frame update
    void Start()
    {
        Width = Screen.width;
        Height = Screen.height;
        _rT = GetComponent<RectTransform>();
        x = -Width / 2;
        y = Height;
        MoveStart(0.0f, 0.0f, 1.5f);
        time = 0.0f;
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
            x = easeOutSine(nowTime / moveTime) * (endX - startX) + startX;
            y = easeOutBounce(nowTime / moveTime) * (endY - startY) + startY;
            _rT.anchoredPosition = new Vector2(x, y);
        }
        else
        {
            time += Time.deltaTime;
            y = Mathf.Sin(time) * 20;
            _rT.anchoredPosition = new Vector2(x, y);
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

    float easeOutSine(float x)
    {
        return Mathf.Sin((x * Mathf.PI) / 2);
    }

    float easeOutBounce(float x)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;

        if (x < 1 / d1)
        {
            return n1 * x * x;
        }
        else if (x < 2 / d1)
        {
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        }
        else if (x < 2.5 / d1)
        {
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        }
        else
        {
            return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }
    }
}

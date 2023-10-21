using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public static int Select;

    public void GameStart(int num)
    {
        Select = num;
        SceneManager.LoadScene("MainScene");
    }
}

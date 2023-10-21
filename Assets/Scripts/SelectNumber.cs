using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectNumber : MonoBehaviour
{
    private Button button;
    [SerializeField] int Select;
    private Title title;

    void Start()
    {
        title = GameObject.Find("Title").GetComponent<Title>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectNum);
    }

    public void SelectNum()
    {
        title.GameStart(Select);
    }
}

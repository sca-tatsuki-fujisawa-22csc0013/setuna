using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    private Button button;
    [SerializeField] int Select;
    private GameControllerÅ@_GC;

    void Start()
    {
        _GC = GameObject.Find("GameController").GetComponent<GameController>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectNum);
    }

    public void SelectNum()
    {
        _GC.GameEnd(Select);
    }
}

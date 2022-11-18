using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartPanelUI : MonoBehaviour
{
    private Button _startButton;
    [SerializeField] private UIHandler _uiHandler;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _startButton = root.Q<Button>("StartTimer");

        _startButton.clicked += StartGame;
    }

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _startButton = root.Q<Button>("StartTimer");

        _startButton.clicked += StartGame;
    }

    void StartGame()
    {
        this.gameObject.SetActive(false);
        _uiHandler.StartGame();
    }
 
}

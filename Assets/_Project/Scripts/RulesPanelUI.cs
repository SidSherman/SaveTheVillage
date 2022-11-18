using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RulesPanelUI : MonoBehaviour
{
    private Button _startButton;
    [SerializeField] private UIHandler _uiHandler;
   

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _startButton = root.Q<Button>("ResumeTimer");

        _startButton.clicked += ResumeGame;
    }

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _startButton = root.Q<Button>("ResumeTimer");

        _startButton.clicked += ResumeGame;
    }
    void ResumeGame()
    {

        _uiHandler.ResumeGame();
        gameObject.SetActive(false);

       
    }
 
}

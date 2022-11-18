using UnityEngine;
using UnityEngine.UIElements;

public class FinishPanelUI : MonoBehaviour
{
    private Button _startButton;
    private Label _message;
    [SerializeField] private UIHandler _uiHandler;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _message = root.Q<Label>("MessageLabel");
        _startButton = root.Q<Button>("StartTimer");
        _startButton.clicked += StartGame;
    }

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _message = root.Q<Label>("MessageLabel");
        _startButton = root.Q<Button>("StartTimer");
        _startButton.clicked += StartGame;
    }

    void StartGame()
    {
        Debug.Log("StartGame");
        gameObject.SetActive(false);
        _uiHandler.StartGame();
    }


    public void SetMessage(string message)
    {  
            _message.text = message;
    }
}

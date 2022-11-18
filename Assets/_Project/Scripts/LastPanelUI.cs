using UnityEngine;
using UnityEngine.UIElements;

public class LastPanelUI : MonoBehaviour
{
    private Button _startButton;
    [SerializeField] private UIHandler _uiHandler;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _startButton = root.Q<Button>("Restart");
        _startButton.clicked += RestartGame;
    }

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _startButton = root.Q<Button>("Restart");
        _startButton.clicked += RestartGame;
    }

    void RestartGame()
    {

        gameObject.SetActive(false);
        _uiHandler.RestartGame();
    }

}

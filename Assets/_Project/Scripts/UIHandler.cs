using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class UIHandler : MonoBehaviour
{


    private Button _hammerButton;
    private Button _picklockButton;
    private Button _screwButton;
    private Button _rulesButton;
    private Button _restartButton;
    private Label _pin1Label;
    private Label _pin2Label;
    private Label _pin3Label;
    private Label _targetValue;
    private ProgressBar _timeProgressBar;

    [SerializeField] private GameObject _rulesPanel;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private GameObject _lastLevelPanel;
    [SerializeField] private GameObject _blockPanel;
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private FinishPanelUI _finishPanelUI;


    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _hammerButton = root.Q<Button>("hammer");
        _picklockButton = root.Q<Button>("picklock");
        _screwButton = root.Q<Button>("screwdriver");
        _rulesButton = root.Q<Button>("Rules");
        _restartButton = root.Q<Button>("Restart");
        _timeProgressBar = root.Q<ProgressBar>("Time");
        _timeProgressBar.value = 100;

        _targetValue = root.Q<Label>("TargetValue");
        _pin1Label = root.Q<Label>("Pin1Value");
        _pin2Label = root.Q<Label>("Pin2Value");
        _pin3Label = root.Q<Label>("Pin3Value");



        _hammerButton.clicked += UseHammer;
        _picklockButton.clicked += UsePicklock;
        _screwButton.clicked += UseScrew;
        _rulesButton.clicked += ShowRules;
        _restartButton.clicked += RestartLevel;

    }

    /// <summary>
    /// Update current pins values, time progress bar
    /// </summary>
    /// <param name="pin1"></param>
    /// <param name="pin2"></param>
    /// <param name="pin3"></param>
    /// <param name="timePercent"></param>
    public void UpdateUI(string pin1, string pin2, string pin3, float timePercent )
    {
        _pin1Label.text = pin1;
        _pin2Label.text = pin2;
        _pin3Label.text = pin3;
        
        _timeProgressBar.value = 100 * timePercent;
       
    }

    /// <summary>
    /// Update pins target value, hammers value, screw value, picklock value
    /// </summary>
    /// <param name="hammer"></param>
    /// <param name="screw"></param>
    /// <param name="picklock"></param>
    /// <param name="target"></param>
    public void SetUIOnTheStart(InstrumentsValue hammer, InstrumentsValue screw, InstrumentsValue picklock, string target)
    {
        _targetValue.text = target;
        _hammerButton.text = $"Молоток\n{hammer.Pin1} {hammer.Pin2} {hammer.Pin3}";
        _screwButton.text = $"Отвертка\n{screw.Pin1} {screw.Pin2} {screw.Pin3}";
        _picklockButton.text = $"Отмычка\n{picklock.Pin1} {picklock.Pin2} {picklock.Pin3}";

    }

    public void ResumeGame()
    {
        SetBlockPanel(false);
        _rulesPanel.SetActive(false);
        _startPanel.SetActive(false);
        _finishPanel.SetActive(false);
        _gameManager.ResumeGame();

    }

    public void ShowFinishPanel(bool isWin)
    {
        _finishPanel.SetActive(true);
     
        SetBlockPanel(true);

        if (isWin)
        {
            _finishPanelUI.SetMessage("Замок взломан!");
        }
        else
        { 
            _finishPanelUI.SetMessage("Время вышло :(");

        }
    }

    public void SetBlockPanel(bool value)
    {
        _blockPanel.SetActive(value);
    }

    public void RestartLevel()
    {
        SetBlockPanel(false);
        _rulesPanel.SetActive(false);
        _startPanel.SetActive(false);
        _finishPanel.SetActive(false);
        _gameManager.RestartGame();
      
    }

    public void RestartGame()
    {
       
        _gameManager.SetCurrentLevel(0);
        StartGame();

    }

    public void StartGame()
    {
        SetBlockPanel(false);
        _rulesPanel.SetActive(false);
        _startPanel.SetActive(false);
        _finishPanel.SetActive(false);
        _gameManager.StartGame();
    }

    public void ShowLastPanel()
    {
        _rulesPanel.SetActive(false);
        _startPanel.SetActive(false);
        _finishPanel.SetActive(false);
        _lastLevelPanel.SetActive(true);
        SetBlockPanel(true);
        _gameManager.PauseGame();
    }



    private void ShowRules()
    {
        _rulesPanel.SetActive(true);
        _startPanel.SetActive(false);
        _finishPanel.SetActive(false);
        SetBlockPanel(true);
        _gameManager.PauseGame();
    }

    
    private void UseScrew()
    {
        _gameManager.ChangePinsValues(_gameManager.Screw);
        _gameManager.CheckPins();
    }

    private void UsePicklock()
    {
        _gameManager.ChangePinsValues(_gameManager.Picklock);
        _gameManager.CheckPins();
    }
    private void UseHammer()
    {
        _gameManager.ChangePinsValues(_gameManager.Hammer);
        _gameManager.CheckPins();
    }
}

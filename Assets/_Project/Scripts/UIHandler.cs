using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UIHandler : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _kmetCountLabel;
    [SerializeField] private TextMeshProUGUI _knightCountLabel;
    [SerializeField] private TextMeshProUGUI _FoodProductionFullLabel;
    [SerializeField] private TextMeshProUGUI _FoodDemandslabel;
    [SerializeField] private TextMeshProUGUI _FoodProductionLabel;
    [SerializeField] private TextMeshProUGUI _FoodCountLabel;
    [SerializeField] private TextMeshProUGUI _BanditsCountLabel;

    [SerializeField] private Button _trainKmetButton;
    [SerializeField] private Button _trainKnightButton;

    [SerializeField] private GameObject _rulesPanel;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private GameObject _lastLevelPanel;
    [SerializeField] private GameObject _blockPanel;


    [SerializeField] private FinishPanelUI _finishPanelUI;

    [SerializeField] private GameManager _manager;


 
    void Start()
    {

    }


    public void UpdateUI(int kmentCount, int knightCount,int foodProductionFull, int foodDemands, int foodProduction, int foodCount, int banditCount)
    {
        _kmetCountLabel.text = kmentCount.ToString();
        _knightCountLabel.text = knightCount.ToString();
        _FoodProductionFullLabel.text = foodProductionFull.ToString();
        _FoodDemandslabel.text = foodDemands.ToString();
        _FoodProductionLabel.text = foodProduction.ToString();
        _FoodCountLabel.text = foodCount.ToString();
        _BanditsCountLabel.text = banditCount.ToString();

    }

    public void TrainKmet()
    {
        _manager.TrainKmet();
        _trainKmetButton.interactable = false;
    }

    public void TrainKnight()
    {
        _manager.TrainKnights();
        _trainKnightButton.interactable = false;
    }

    public void ResetTrainKmet()
    { 
        _trainKmetButton.interactable = true;
    }

    public void ResetTrainKnight()
    {
        _trainKnightButton.interactable = true;
    }


    public void SetUIOnTheStart()
    {
      

    }

    public void ResumeGame()
    {
        SetBlockPanel(false);
        _rulesPanel.SetActive(false);
        _startPanel.SetActive(false);
        _finishPanel.SetActive(false);
        _manager.ResumeGame();

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
        _manager.RestartGame();
      
    }

    public void RestartGame()
    {
       
        _manager.SetCurrentLevel(0);
        StartGame();

    }

    public void StartGame()
    {
        SetBlockPanel(false);
        _rulesPanel.SetActive(false);
        _startPanel.SetActive(false);
        _finishPanel.SetActive(false);
        _manager.StartGame();
    }

    public void ShowLastPanel()
    {
        _rulesPanel.SetActive(false);
        _startPanel.SetActive(false);
        _finishPanel.SetActive(false);
        _lastLevelPanel.SetActive(true);
        SetBlockPanel(true);
        _manager.PauseGame();
    }

    private void ShowRules()
    {
        _rulesPanel.SetActive(true);
        _startPanel.SetActive(false);
        _finishPanel.SetActive(false);
        SetBlockPanel(true);
        _manager.PauseGame();
    }

}


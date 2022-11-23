using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _failPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _blockPanel;

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
        _startPanel.SetActive(false);
        _menuPanel.SetActive(true);
    }

    public void ResumePauseGame(bool shouldPause)
    {
        if (shouldPause)
        {
            Time.timeScale = 0f;
            _blockPanel.SetActive(true);
            _menuPanel.SetActive(true);
            _startPanel.SetActive(true);
        }

        else
        {
            Time.timeScale = 1f;
            _blockPanel.SetActive(false);
            _menuPanel.SetActive(false);
            _startPanel.SetActive(false);
        }
       
    }

    public void ShowWinPanel()
    {
        _blockPanel.SetActive(true);
        _winPanel.SetActive(true);
    }

    public void ShowFailPanel()
    {
        _blockPanel.SetActive(true);
        _failPanel.SetActive(true);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}


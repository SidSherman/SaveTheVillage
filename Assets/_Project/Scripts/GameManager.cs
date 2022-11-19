using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{

    [SerializeField] private UIHandler _uiHandler;

    [Header("Timers")]
    [SerializeField] private TimeManager _kmetTrainingtimeManager;
    [SerializeField] private TimeManager _knightTrainingTimeManager;
    [SerializeField] private TimeManager _raidTimeManager;
    [SerializeField] private TimeManager _foodProductionTimeManager;

    [Header("Civilliance Info")]
    [SerializeField] private int kmetsCount = 1;
    [SerializeField] private int knightsCount = 0;

    [Header("RaidInfo")]
    [SerializeField] private float raidTime = 20;
    [SerializeField] private int raidDelay = 0;
    [SerializeField] private int banditsCount = 0;

    [Header("Training Civilliance Info")]
    [SerializeField] private float kmetTrainignTime = 5;
    [SerializeField] private float knightTrainingTime = 10;
    [SerializeField] private int kmetTrainignCost = 5;
    [SerializeField] private int knightTrainingCost = 10;

    [Header("Food Production Info")]
    [SerializeField] private int foodCount;
    [SerializeField] private float foodProductionTime = 10;
    [SerializeField] private int foodProductionByOneKmet;
    [SerializeField] private int foodProduction;
    [SerializeField] private int foodDemandsByOneKnight;


    private void Start()
    {
        _foodProductionTimeManager.StartTimer(foodProductionTime, 0f);
        _raidTimeManager.StartTimer(raidTime, raidDelay * foodProductionTime);
        CalculateFoodProduction();
        UpdateUI();
    }

    void Update()
    {

        if (_foodProductionTimeManager)
        {
            if (_foodProductionTimeManager.IsValidTimer)
            {
                if (_foodProductionTimeManager.IsFinishedTimer)
                {
                    CalculateFood();
                    _foodProductionTimeManager.StartTimer(foodProductionTime, 0f);
                }
            }

        }
        if (_kmetTrainingtimeManager)
        {
            if (_kmetTrainingtimeManager.IsValidTimer)
            {
                if (_kmetTrainingtimeManager.IsFinishedTimer)
                {
                    
                    _kmetTrainingtimeManager.InvalidateTimer();
                    SetKmets(1);
                    CalculateFoodProduction();
                    _uiHandler.ResetTrainKmet();
                }
            }

        }

        if (_knightTrainingTimeManager)
        {
            if (_knightTrainingTimeManager.IsValidTimer)
            {
                if (_knightTrainingTimeManager.IsFinishedTimer)
                {
                    
                    _knightTrainingTimeManager.InvalidateTimer();
                    SetKnights(1);
                    CalculateFoodProduction();
                    _uiHandler.ResetTrainKnight();
                }
            }

        }
    }


    private void UpdateUI()
    {
        _uiHandler.UpdateUI(kmetsCount, knightsCount, foodProduction, foodDemandsByOneKnight * knightsCount, foodProductionByOneKmet * kmetsCount, foodCount, banditsCount);

    }
    public void CheckKmetsStatus() {

            if (kmetsCount <= 0 && foodCount < kmetTrainignCost)
            {
                FailGame();
            }

        }

        public void CalculateFood()
        {
            
            foodCount = foodCount + foodProduction;
        Debug.Log(foodCount);
            UpdateUI();
        }

        public void CalculateFoodProduction()
        {
            foodProduction = foodProductionByOneKmet* kmetsCount - foodDemandsByOneKnight * knightsCount;
            UpdateUI();
        }


    public void TrainKmet()
    {
        if(!_kmetTrainingtimeManager.IsValidTimer)
        {
            _kmetTrainingtimeManager.StartTimer(kmetTrainignTime, 0f);
        }

    }
    public void TrainKnights()
    {
        if(!_knightTrainingTimeManager.IsValidTimer)
        {
            _knightTrainingTimeManager.StartTimer(knightTrainingTime, 0f);
        }
     
    }

    public void SetKnights(int value)
        {
            knightsCount += value;
            UpdateUI();
        }

        public void SetKmets(int value)
        {
            kmetsCount += value;
            UpdateUI();
            CheckKmetsStatus();

        }


    public void FinishGame()
        {

            _uiHandler.ShowFinishPanel(true);

        }

        public void FailGame()
        {
            _uiHandler.ShowFinishPanel(false);
        }

        public void PauseGame()
        {

        }

        public void ResumeGame()
        {

        }

        public void StartGame()
        {

        }

        public void RestartGame()
        {

            _uiHandler.SetBlockPanel(false);
        }

        public void SetCurrentLevel(int level)
        {

        }


    }



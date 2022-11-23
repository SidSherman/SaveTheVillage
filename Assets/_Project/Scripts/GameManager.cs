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
    [SerializeField] private int banditsIncreasingRate = 3;
    [SerializeField] private int bandNeedsToKillKnight= 2;
    [SerializeField] private int kmetsNeedsToKillBandit = 3;

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

    //Statistics
    private int foodAmount;
    private int foodWasted;
    private int raidAmount;
    private int kmetsAmount;
    private int knightAmount;
    private int banditsKilled;


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

        if (_raidTimeManager)
        {
            if (_raidTimeManager.IsValidTimer)
            {
                if (_raidTimeManager.IsFinishedTimer)
                {
                    _raidTimeManager.InvalidateTimer();
                    Fight();
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
           UpdateUI();
        }

        public void CalculateFoodProduction()
        {
            foodProduction = foodProductionByOneKmet* kmetsCount - foodDemandsByOneKnight * knightsCount;
            UpdateUI();
        }


    public void TrainKmet()
    {
        if(!_kmetTrainingtimeManager.IsValidTimer && foodCount > kmetTrainignCost)
        {
            _kmetTrainingtimeManager.StartTimer(kmetTrainignTime, 0f);
        }

    }
    public void TrainKnights()
    {
        if(!_knightTrainingTimeManager.IsValidTimer && foodCount > knightTrainingCost)
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

    public void WinGame()
        {
        _uiHandler.ShowWinPanel();
    }

    public void FailGame()
    {
        _uiHandler.ShowFailPanel();
    }

    public void Fight()
    {
        int knightsCountTemp = knightsCount;
        int kmetsCountTemp = kmetsCount;

        if (knightsCount >= banditsCount)
        {
            for(int i = 1; i < banditsCount; i += bandNeedsToKillKnight)
            {
                knightsCountTemp--;
            }
            SetKnights(knightsCountTemp);
        }

        else
        {
            kmetsCountTemp = kmetsCount - kmetsNeedsToKillBandit * (banditsCount - knightsCount * bandNeedsToKillKnight);
            SetKnights(0);
            SetKmets(kmetsCountTemp);
               
        }
        IncreaseBanditsCount();


    }
    public void IncreaseBanditsCount()
    {
        if(raidAmount % banditsIncreasingRate == 0)
        {
            banditsCount++;
        }

    }

}



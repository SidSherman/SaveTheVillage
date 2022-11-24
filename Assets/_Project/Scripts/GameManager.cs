using Newtonsoft.Json.Linq;
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
    [SerializeField] private int kmetCost = 5;
    [SerializeField] private int knightCost = 10;

    [Header("Food Production Info")]
    [SerializeField] private int foodCount;
    [SerializeField] private float foodProductionTime = 10;
    [SerializeField] private int foodProductionByOneKmet;
    [SerializeField] private int foodProduction;
    [SerializeField] private int foodDemandsByOneKnight;
    [SerializeField] private int foodToBuldWalls = 500;

    [SerializeField] private int daysToWin = 100;
    private int currentRaid = 0;
    private int currentDay = 1;

    //Statistics
    private int foodAmount;
    private int foodWasted;
    private int kmetsAmount;
    private int knightAmount;
    private int banditsKilled;

    public int RaidDelay { get => raidDelay; set => raidDelay = value; }
    public int FoodProductionByOneKmet { get => foodProductionByOneKmet; set => foodProductionByOneKmet = value; }
    public int FoodDemandsByOneKnight { get => foodDemandsByOneKnight; set => foodDemandsByOneKnight = value; }
    public int FoodToBuldWalls { get => foodToBuldWalls; set => foodToBuldWalls = value; }
    public int DaysToWin { get => daysToWin; set => daysToWin = value; }
    public int KmetsNeedsToKillBandit { get => kmetsNeedsToKillBandit; set => kmetsNeedsToKillBandit = value; }
    public int BandNeedsToKillKnight { get => bandNeedsToKillKnight; set => bandNeedsToKillKnight = value; }
    public int BanditsIncreasingRate { get => banditsIncreasingRate; set => banditsIncreasingRate = value; }

    private void Start()
    {
        _foodProductionTimeManager.StartTimer(foodProductionTime, 0f);
        _raidTimeManager.StartTimer(raidTime, RaidDelay * foodProductionTime);
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
                    currentDay++;
                    CalculateFood();
                    if (currentDay >= DaysToWin)
                    {
                        WinGame();
                    }
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
                    SetKmets(kmetsCount + 1);
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
                    SetKnights(knightsCount+1);
                    SetKmets(kmetsCount - 1);
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
                    currentRaid++;

                    Fight();
                    _raidTimeManager.StartTimer(raidTime, 0f);
                }
            }
        }
    }


    private void UpdateUI()
    {
        _uiHandler.UpdateUI(kmetsCount, 
            knightsCount, 
            foodProduction, 
            FoodDemandsByOneKnight * knightsCount, 
            FoodProductionByOneKmet * kmetsCount, 
            foodCount, banditsCount, 
            kmetCost, 
            knightCost,
            currentRaid,
            currentDay
            );

    }

    public void CalculateFood()
    {
        foodCount = foodCount + foodProduction;

        if(foodCount <= 0)
        {
            if(knightsCount > 0)
            {
                SetKnights(knightsCount - 1);
            }
            else
            {
                SetKmets(kmetsCount - 1);
            }

        }

        if(foodCount >= foodToBuldWalls)
        {
            WinGame();
        }
        UpdateUI();
    }

    public void CalculateFoodProduction()
    {
        foodProduction = FoodProductionByOneKmet* kmetsCount - FoodDemandsByOneKnight * knightsCount;
        UpdateUI();
    }


    public bool TrainKmet()
    {
        if(!_kmetTrainingtimeManager.IsValidTimer)
        {
            _kmetTrainingtimeManager.StartTimer(kmetTrainignTime, 0f);
            return true;
        }
        else return false;

    }
    public bool TrainKnights()
    {
        if(!_knightTrainingTimeManager.IsValidTimer && kmetsCount > 0)
        {
            _knightTrainingTimeManager.StartTimer(knightTrainingTime, 0f);
            return true;
        }
        else return false;
     
    }

    public void RecruitKmet()
    {
        if(kmetCost <= foodCount)
        {
            foodCount -= kmetCost;

            SetKmets(kmetsCount + 1);
        }
    }
    public void RecruitKnights()
    {
        if (knightCost <= foodCount)
        {
            foodCount -= knightCost;

            SetKnights(knightsCount + 1);
        }

    }


    public void SetKnights(int value)
    {
        knightsCount = value;
        CalculateFoodProduction();
        UpdateUI();
       
    }

    public void SetKmets(int value)
    {

        
        if (value < 0)
        {
            FailGame();
        }
        else
        {
            kmetsCount = value;
        }
        CalculateFoodProduction();
        UpdateUI();

    }

    public void WinGame()
        {
        _uiHandler.ShowWinPanel(true);
    }

    public void FailGame()
    {

        _uiHandler.ShowFailPanel(true);
    }

    public void Fight()
    {
        int currentBanditsCount = banditsCount;

        while(currentBanditsCount > 0 && knightsCount > 0)
        {
            if (currentBanditsCount < BandNeedsToKillKnight)
            {
                break;
            }
            SetKnights(knightsCount -1);
            currentBanditsCount -= BandNeedsToKillKnight;
        }

        if(knightsCount <= 0)
        {

            while (currentBanditsCount > 0 && kmetsCount > 0)
            {
                SetKmets(kmetsCount - KmetsNeedsToKillBandit);
                currentBanditsCount--;
                     
            }
        }

        UpdateUI();
        IncreaseBanditsCount();

    }

    public void IncreaseBanditsCount()
    {
        
        if(currentRaid >= BanditsIncreasingRate)
        {
         
            if (currentRaid % BanditsIncreasingRate == 0)
            {
                banditsCount++;
            }
        }
      

    }

}



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
    [SerializeField] private float _raidTime = 20;
    [SerializeField] private int _raidDelay = 0;
    [SerializeField] private int _banditsCount = 0;
    [SerializeField] private int _banditsIncreasingRate = 3;
    [SerializeField] private int _bandNeedsToKillKnight= 2;
    [SerializeField] private int _kmetsNeedsToKillBandit = 3;

    [Header("Training Civilliance Info")]
    [SerializeField] private float _kmetTrainignTime = 5;
    [SerializeField] private float _knightTrainingTime = 10;
    [SerializeField] private int _kmetCost = 5;
    [SerializeField] private int _knightCost = 10;

    [Header("Food Production Info")]
    
    [SerializeField] private float _foodProductionTime = 10;
    [SerializeField] private int _foodProductionByOneKmet;
    [SerializeField] private int _foodProduction;
    [SerializeField] private int _foodDemandsByOneKnight;
    [SerializeField] private int _foodToBuldWalls = 500;

    [SerializeField] private int _daysToWin = 100;
    private int _currentRaid = 0;
    private int _currentDay = 1;

    //Statistics
    private int _foodCount;
    private int _foodAmount;
    private int _foodWasted;
    private int _kmetsAmount;
    private int _knightAmount;
    private int _banditsKilled;

    public int RaidDelay { get => _raidDelay; set => _raidDelay = value; }
    public int FoodProductionByOneKmet { get => _foodProductionByOneKmet; set => _foodProductionByOneKmet = value; }
    public int FoodDemandsByOneKnight { get => _foodDemandsByOneKnight; set => _foodDemandsByOneKnight = value; }
    public int FoodToBuldWalls { get => _foodToBuldWalls; set => _foodToBuldWalls = value; }
    public int DaysToWin { get => _daysToWin; set => _daysToWin = value; }
    public int KmetsNeedsToKillBandit { get => _kmetsNeedsToKillBandit; set => _kmetsNeedsToKillBandit = value; }
    public int BandNeedsToKillKnight { get => _bandNeedsToKillKnight; set => _bandNeedsToKillKnight = value; }
    public int BanditsIncreasingRate { get => _banditsIncreasingRate; set => _banditsIncreasingRate = value; }

    private void Start()
    {
        _foodProductionTimeManager.StartTimer(_foodProductionTime, 0f);
        _raidTimeManager.StartTimer(_raidTime, RaidDelay * _foodProductionTime);
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
                    _currentDay++;
                    CalculateFood();
                    if (_currentDay >= DaysToWin)
                    {
                        WinGame();
                    }
                    _foodProductionTimeManager.StartTimer(_foodProductionTime, 0f);
                    
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
                    _currentRaid++;

                    Fight();
                    _raidTimeManager.StartTimer(_raidTime, 0f);
                }
            }
        }
    }

    private void UpdateUI()
    {
        _uiHandler.UpdateUI(kmetsCount, 
            knightsCount, 
            _foodProduction, 
            FoodDemandsByOneKnight * knightsCount, 
            FoodProductionByOneKmet * kmetsCount, 
            _foodCount, _banditsCount, 
            _kmetCost, 
            _knightCost,
            _currentRaid,
            _currentDay
            );
    }

    public void CalculateFood()
    {
        _foodCount = _foodCount + _foodProduction;

        if(_foodCount <= 0)
        {
                SetKnights(knightsCount - 1);
                SetKmets(kmetsCount - 1);
                
            _foodCount = 0;
        }

        if(_foodCount >= _foodToBuldWalls)
        {
            WinGame();
        }
        UpdateUI();
    }

    public void CalculateFoodProduction()
    {
        _foodProduction = FoodProductionByOneKmet* kmetsCount - FoodDemandsByOneKnight * knightsCount;
        UpdateUI();
    }

    public bool TrainKmet()
    {
        if(!_kmetTrainingtimeManager.IsValidTimer)
        {
            _kmetTrainingtimeManager.StartTimer(_kmetTrainignTime, 0f);
            return true;
        }
        else return false;
    }

    public bool TrainKnights()
    {
        if(!_knightTrainingTimeManager.IsValidTimer && kmetsCount > 0)
        {
            _knightTrainingTimeManager.StartTimer(_knightTrainingTime, 0f);
            return true;
        }
        else return false;
    }

    public void RecruitKmet()
    {
        if(_kmetCost <= _foodCount)
        {
            _foodCount -= _kmetCost;

            SetKmets(kmetsCount + 1);
        }
    }
    public void RecruitKnights()
    {
        if (_knightCost <= _foodCount)
        {
            _foodCount -= _knightCost;

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
        int currentBanditsCount = _banditsCount;

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
        if(_currentRaid >= BanditsIncreasingRate)
        {
            if (_currentRaid % BanditsIncreasingRate == 0)
            {
                _banditsCount++;
            }
        }
    }
}



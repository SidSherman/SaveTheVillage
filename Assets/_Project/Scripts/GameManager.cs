using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Pin[] _pins;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private UIHandler _uiHandler;
    [SerializeField] private InstrumentsValue _hammer;
    [SerializeField] private InstrumentsValue _screw;
    [SerializeField] private InstrumentsValue _picklock;
    [SerializeField] private LevelValue[] levels;

    [SerializeField] private int kmetsCount = 1;
    [SerializeField] private int knightsCount = 0;
    [SerializeField] private int foodProductionByOneKmet;
    [SerializeField] private int foodProductionFull;
    [SerializeField] private int foodProduction;
   [SerializeField] private int foodDemands;
   [SerializeField] private int foodDemandsByOneKnight;
    [SerializeField] private int _currentLevel;
    [SerializeField] private float _gameTime;

    private int _properPinsValue;

    public InstrumentsValue Hammer { get => _hammer; set => _hammer = value; }
    public InstrumentsValue Screw { get => _screw; set => _screw = value; }
    public InstrumentsValue Picklock { get => _picklock; set => _picklock = value; }

 
    void Update()
    {
      

        if(_timeManager.IsFinishedTimer)
        {
           
            FailGame();
        }


    }
    
    public void CheckPins(){

        foreach(Pin pin in _pins)
        {
            if(pin.PinValue != _properPinsValue)
            {
                return;
            }
        }

        FinishGame();
    }

    public void FinishGame()
    {
        
        _timeManager.StopTimer();
        _timeManager.InvalidateTimer();
       
        _currentLevel++;
        _uiHandler.ShowFinishPanel(true);

        if (_currentLevel >= levels.Length)
        {
            _uiHandler.ShowLastPanel();
        }
       
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

    public void ChangePinsValues(int firstValue, int secondValue, int thirdValue)
    {
 
        
    }
    public void ChangePinsValues(InstrumentsValue instrument)
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


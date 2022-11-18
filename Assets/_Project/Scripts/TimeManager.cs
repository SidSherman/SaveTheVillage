using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TimeManager : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private bool _shouldRepeat;
    private float _startedTime;
    private float _elapsedTime;
    private  float _remainingTime;
    private bool _isValidTimer;
    private bool _isPausedTimer;
    private bool _isFinishedTimer;

    public float TimerValue {get {return _time;} set { _time = value;}}
    public float StartedTime {get {return _startedTime;}}
    public float ElapsedTime {get {return _elapsedTime;}}
    public float RemainingTime {get {return _remainingTime;}}
    public bool IsValidTimer {get {return _isValidTimer;}}
    public bool IsFinishedTimer { get => _isFinishedTimer; }


    private void Start()
    {
        _isFinishedTimer = false;
        _isValidTimer = false;
        _isPausedTimer = false;
    }
    
    void Update()
    {
       
        if (_isValidTimer)
        {
            if(!_isPausedTimer)
            {
              
                if (_remainingTime > 0)
                {
                    _remainingTime -= Time.deltaTime;
                    _elapsedTime += Time.deltaTime;
                }
                else
                {
                    StopTimer();
                }
            }
        }   
    }

    public TimeManager(float timerValue)
    {
        _time = timerValue;
    }

    public void PauseTimer()
    {
        _isPausedTimer = true;
    }

    public void ResumeTimer()
    {
        _isPausedTimer = false;
    }

    public void StartTimer()
    {
        _isFinishedTimer = false;
        _isValidTimer = true;
        _isPausedTimer = false;
        _remainingTime = _time;
        _elapsedTime = 0;
        _startedTime = Time.time;
    }

    public void StartTimer(float timerValue)
    {

        _time = timerValue;
        _isValidTimer = true;
        _isPausedTimer = false;
        _remainingTime = timerValue;
        _elapsedTime = 0;
        _startedTime = Time.time;
    }

    public void StopTimer()
    {
        _isFinishedTimer = true;
        _isPausedTimer = false;

        if(_shouldRepeat)
        {
            StartTimer();
        }        
    }

    public void InvalidateTimer()
    {     
        _isValidTimer = false;
        _isFinishedTimer = false;
        _isPausedTimer = false;
    }


}

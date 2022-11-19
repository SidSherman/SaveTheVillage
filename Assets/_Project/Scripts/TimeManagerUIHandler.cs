using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeManagerUIHandler : MonoBehaviour
{
    [SerializeField] private TimeManager _timer;
    [SerializeField] private Image _timerImage;


    private void Start()
    {
       
    }
    private void Update ()
    {
        if(_timer)
        {
            if (_timer.IsValidTimer)
            {
                _timerImage.fillAmount = _timer.RemainingTime / _timer.TimerValue;
            }
            else if(_timer.IsFinishedTimer || !_timer.IsValidTimer)
            {
                _timerImage.fillAmount = 1;
            }
        }
       

    }

}

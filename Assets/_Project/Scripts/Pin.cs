using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private int _pinValue;
    [SerializeField] private int _minValue;
    [SerializeField] private int _maxValue;

    public int PinValue {get {return _pinValue;} set {_pinValue = value;}}  

    public void SetValue(int newValue)
    {
        _pinValue = Mathf.Clamp(newValue, _minValue, _maxValue); ;
    }
}

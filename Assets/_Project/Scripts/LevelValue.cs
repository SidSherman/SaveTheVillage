using UnityEngine;

[System.Serializable]

public struct LevelValue
{
    [SerializeField] InstrumentsValue _pins;
    [SerializeField] int _targetValue;

    public LevelValue(InstrumentsValue pins, int targetValue)
    {
        _pins = pins;
        _targetValue = targetValue;
    }

    public InstrumentsValue Pins { get => _pins; set => _pins = value; }
    public int TargetValue { get => _targetValue; set => _targetValue = value; }
}
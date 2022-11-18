
using UnityEngine;

[System.Serializable]
public struct InstrumentsValue
{
    [SerializeField] int _pin1;
    [SerializeField] int _pin2;
    [SerializeField] int _pin3;

    public int Pin1 { get => _pin1; set => _pin1 = value; }
    public int Pin2 { get => _pin2; set => _pin2 = value; }
    public int Pin3 { get => _pin3; set => _pin3 = value; }

    public InstrumentsValue(int pin1, int pin2, int pin3)
    {
        _pin1 = pin1;
        _pin2 = pin2;
        _pin3 = pin3;
    }
}

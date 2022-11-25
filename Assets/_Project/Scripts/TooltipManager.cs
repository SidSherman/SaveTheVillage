using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class TooltipManager : MonoBehaviour
{

    public static TooltipManager _instance;
    [SerializeField] private TextMeshProUGUI _tooltip;

    private void Start() {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Mouse.current.position.ReadValue();
    }

    public void SetAndShowTip(string message)
    {
        //gameObject.SetActive(true);
        _tooltip.text = message;
    }

    
    public void ClearAndHideTip()
    {
        //gameObject.SetActive(false);
        _tooltip.text = string.Empty;
    }
}

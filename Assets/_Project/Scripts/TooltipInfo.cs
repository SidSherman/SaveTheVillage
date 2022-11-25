using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TooltipInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string message;

  
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager._instance.SetAndShowTip(message);
    }
      void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
          TooltipManager._instance.ClearAndHideTip();
    }

}

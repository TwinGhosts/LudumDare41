using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject toolTipObject;

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTipObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTipObject.SetActive(true);
    }
}

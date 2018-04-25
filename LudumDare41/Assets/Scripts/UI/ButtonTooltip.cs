using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTooltip : MonoBehaviour
{
    public GameObject toolTipObject;

    public void OnMouseOver()
    {
        toolTipObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        toolTipObject.SetActive(false);
    }
}

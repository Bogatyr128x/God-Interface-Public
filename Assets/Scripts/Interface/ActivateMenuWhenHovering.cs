using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActivateMenuWhenHovering : MonoBehaviour, IPointerEnterHandler
{
    // Start is called before the first frame update
    public uint MenuToActivate;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        InterfaceListener.SetUIDOfCurrentlyActiveMenu(MenuToActivate);
    }
}

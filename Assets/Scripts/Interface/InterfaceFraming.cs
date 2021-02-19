using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceFraming : MonoBehaviour
{
    // Start is called before the first frame update

    public enum BaseUIFrames
    {
        Top,
        Bottom,
        Left,
        Right
    }

    private static Transform TopFrame;
    private static Transform BottomFrame;
    private static Transform LeftFrame;
    private static Transform RightFrame;


    public static void PlaceObjectInUIFrame(BaseUIFrames frame, InterfaceFabricator.GodInterfaceMenu GIToMove)
    {
        if (frame == BaseUIFrames.Top)
        {
            GIToMove.gameObject.transform.SetParent(TopFrame, false);
        }
        if (frame == BaseUIFrames.Bottom)
        {
            GIToMove.gameObject.transform.SetParent(BottomFrame, false);
        }
        if (frame == BaseUIFrames.Left)
        {
            GIToMove.gameObject.transform.SetParent(LeftFrame, false);
        }
        if (frame == BaseUIFrames.Right)
        {
            GIToMove.gameObject.transform.SetParent(RightFrame, false);
        }
    }


    void Start()
    {
        TopFrame = GameObject.Find("ui-TopFrame").transform;
        BottomFrame = GameObject.Find("ui-BottomFrame").transform;
        LeftFrame = GameObject.Find("ui-LeftFrame").transform;
        RightFrame = GameObject.Find("ui-RightFrame").transform;
    }
}

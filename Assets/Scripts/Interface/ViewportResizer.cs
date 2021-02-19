using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ViewportResizer : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{


    private static Vector2 ViewportUpRightOffsets;
    private static Vector2 ViewportDownLeftOffsets;


    private static bool[] UIFramesToHide = new bool[4];
    private static bool[] UIFramesToBlockResizing = new bool[4]; // top bottom left right


    private static bool[] IsDragging = new bool[4];
    private static float MaximumClickDistance = 24f;
    private static float MinimumViewportSize = 128;
    private static float MaximumViewportMargin = 12f;

    private static Vector4 savedCoords = new Vector4();

    internal class ViewportXYABAnimator
    {
        internal Vector4 startPos;
        internal Vector4 endPos;
        internal Vector4 currPos;
        internal float startTime;
        internal float endTime;
        internal float progress;
        internal bool[] sidesToBlockAfterAnimationEnd;
        internal void Animate()
        {
            progress = 1 - ((endTime - Time.time) / (endTime - startTime));   // How much of the animation has happened, from 0 to 1.
            //Debug.Log(progress);
            currPos[0] = Mathf.SmoothStep(startPos[0], endPos[0], progress);
            currPos[1] = Mathf.SmoothStep(startPos[1], endPos[1], progress);
            currPos[2] = Mathf.SmoothStep(startPos[2], endPos[2], progress);
            currPos[3] = Mathf.SmoothStep(startPos[3], endPos[3], progress);
            savedCoords = currPos;
            if (progress >= 1.0f)
            {
                UIFramesToBlockResizing = sidesToBlockAfterAnimationEnd;
                UIFramesToHide = sidesToBlockAfterAnimationEnd;
                currPos[0] = endPos[0];
                currPos[1] = endPos[1];
                currPos[2] = endPos[2];
                currPos[3] = endPos[3];
                ForceViewportIntoTheseOffsets(HideHiddenCornersFromThisVector4(currPos));
                InterfaceListener.BlockAllButtonPresses = false;
                ViewportSizeAnimator = null;
            }
            //Vector2 output = new Vector2(currPos[0], currPos[1]);
            //animations[index].animatingRect.offsetMin = output;
            //output = new Vector2(currPos[2], currPos[3]);
            //animations[index].animatingRect.offsetMax = output;
        }
    }


    private static ViewportXYABAnimator ViewportSizeAnimator = null;

    public static void ResizeViewport(Vector4 startingPositions, Vector4 endPositions, float duration, bool[] blockTheseAfterAnimEnd)
    {
        ViewportSizeAnimator = new ViewportXYABAnimator();
        savedCoords = startingPositions;
        ViewportSizeAnimator.sidesToBlockAfterAnimationEnd = blockTheseAfterAnimEnd;
        ViewportSizeAnimator.startPos = startingPositions;
        ViewportSizeAnimator.endPos = endPositions;
        ViewportSizeAnimator.startTime = Time.time;
        ViewportSizeAnimator.endTime = Time.time + duration;
    }

    public static Vector4 GetViewportOffsets()
    {
        return new Vector4(ViewportDownLeftOffsets.x, ViewportDownLeftOffsets.y, ViewportUpRightOffsets.x, ViewportUpRightOffsets.y);
    }

    private enum ViewportCorners
    {
        Downer,
        Lefty,
        Righto,
        Up
    }
    private static GameObject thisGameObject;
    private static void ForceViewportIntoTheseOffsets(Vector4 xyabCoords)
    {
        xyabCoords = MakeSureViewportIsInsideScreen(xyabCoords);
        xyabCoords = HideHiddenCornersFromThisVector4(xyabCoords);
        thisGameObject.GetComponent<RectTransform>().offsetMin = new Vector2(xyabCoords[0], xyabCoords[1]);
        thisGameObject.GetComponent<RectTransform>().offsetMax = new Vector2(xyabCoords[2], xyabCoords[3]);
    }

    private static Vector4 MakeSureViewportIsInsideScreen(Vector4 xyabCorners)
    {
        if (xyabCorners[0] < MaximumViewportMargin)
        {
            xyabCorners[0] = MaximumViewportMargin;
            if (xyabCorners[0] + MinimumViewportSize > xyabCorners[2])
            {
                xyabCorners[2] = MinimumViewportSize;
            }
        }
        if (xyabCorners[1] < MaximumViewportMargin)
        {
            xyabCorners[1] = MaximumViewportMargin;
            if (xyabCorners[1] + MinimumViewportSize > xyabCorners[3])
            {
                xyabCorners[3] = MinimumViewportSize;
            }
        }
        if (xyabCorners[2] > Screen.width - MaximumViewportMargin)
        {
            xyabCorners[2] = Screen.width - MaximumViewportMargin;
            if (xyabCorners[0] > xyabCorners[2] - MinimumViewportSize)
            {
                xyabCorners[0] = xyabCorners[2] - MinimumViewportSize;
            }
        }
        if (xyabCorners[3] > Screen.height - MaximumViewportMargin)
        {
            xyabCorners[3] = Screen.height - MaximumViewportMargin;
            if (xyabCorners[1] > xyabCorners[3] - MinimumViewportSize)
            {
                xyabCorners[1] = xyabCorners[3] - MinimumViewportSize;
            }
        }
        return xyabCorners;
    }

    public void OnDrag(PointerEventData pointerData)
    {
        Vector4 xyabCorners = new Vector4(ViewportDownLeftOffsets.x, ViewportDownLeftOffsets.y, ViewportUpRightOffsets.x, ViewportUpRightOffsets.y);
        if (IsDragging[(int)ViewportCorners.Lefty] && UIFramesToBlockResizing[(int)InterfaceFraming.BaseUIFrames.Left] == false)
        {
            xyabCorners[0] = pointerData.position.x;
            // The second if condition in each corner allows us to "push" the viewport somewhere else.
            if(xyabCorners[2] - xyabCorners[0] < MinimumViewportSize)
            {
                xyabCorners[2] = xyabCorners[0] + MinimumViewportSize;
            }
        }
        if (IsDragging[(int)ViewportCorners.Righto] && UIFramesToBlockResizing[(int)InterfaceFraming.BaseUIFrames.Right] == false)
        {
            xyabCorners[2] = pointerData.position.x; 
            if (xyabCorners[2] - xyabCorners[0] < MinimumViewportSize)
            {
                xyabCorners[0] = xyabCorners[2] - MinimumViewportSize;
            }
        }
        if (IsDragging[(int)ViewportCorners.Up] && UIFramesToBlockResizing[(int)InterfaceFraming.BaseUIFrames.Top] == false)
        {
            xyabCorners[3] = pointerData.position.y;
            if (xyabCorners[3] - xyabCorners[1] < MinimumViewportSize)
            {
                xyabCorners[1] = xyabCorners[3] - MinimumViewportSize;
            }
        }
        if (IsDragging[(int)ViewportCorners.Downer] && UIFramesToBlockResizing[(int)InterfaceFraming.BaseUIFrames.Bottom] == false)
        {
            xyabCorners[1] = pointerData.position.y;
            if (xyabCorners[3] - xyabCorners[1] < MinimumViewportSize)
            {
                xyabCorners[3] = xyabCorners[1] + MinimumViewportSize;
            }
        }
        ForceViewportIntoTheseOffsets(xyabCorners);
    }

    private static Vector4 GetDistanceToViewportCorners(Vector2 pointerPosition)
    {
        Vector4 Distances = new Vector4();
        Distances[0] = Mathf.Abs(pointerPosition.x - ViewportDownLeftOffsets.x);
        Distances[1] = Mathf.Abs(pointerPosition.y - ViewportDownLeftOffsets.y);
        Distances[2] = Mathf.Abs(pointerPosition.x - ViewportUpRightOffsets.x);
        Distances[3] = Mathf.Abs(pointerPosition.y - ViewportUpRightOffsets.y);
        return Distances;
    }

    public void OnBeginDrag(PointerEventData pointerData)
    {
        Vector4 distancesToCorners = GetDistanceToViewportCorners(pointerData.position);// First corner is left, then top, then right, then bottom

        if (distancesToCorners[0] < MaximumClickDistance && UIFramesToBlockResizing[(int)InterfaceFraming.BaseUIFrames.Left] == false)
        {
            IsDragging[(int)ViewportCorners.Lefty] = true;
        }
        if (distancesToCorners[1] < MaximumClickDistance && UIFramesToBlockResizing[(int)InterfaceFraming.BaseUIFrames.Bottom] == false)
        {
            IsDragging[(int)ViewportCorners.Downer] = true;
        }
        if (distancesToCorners[2] < MaximumClickDistance && UIFramesToBlockResizing[(int)InterfaceFraming.BaseUIFrames.Right] == false)
        {
            IsDragging[(int)ViewportCorners.Righto] = true;
        }
        if (distancesToCorners[3] < MaximumClickDistance && UIFramesToBlockResizing[(int)InterfaceFraming.BaseUIFrames.Top] == false)
        {
            IsDragging[(int)ViewportCorners.Up] = true;
        }
    }
    public void OnEndDrag(PointerEventData pointerData)
    {
        for(int i = 0; i < 4; i++)
        {
            IsDragging[i] = false;
        }
    }

    public static void ChangeSizeOfThisMenuFrame(InterfaceFraming.BaseUIFrames whichFrame, float menuSize)
    {
        Vector4 newSize = new Vector4();
        switch (whichFrame)
        {
            case InterfaceFraming.BaseUIFrames.Right:
                newSize = GetViewportOffsets();
                newSize[2] = Screen.width - menuSize;
                break;
            case InterfaceFraming.BaseUIFrames.Left:
                newSize = GetViewportOffsets();
                newSize[2] = Screen.width - menuSize;
                break;
        }
        ChangeViewportSize(newSize, new bool[4], 1.0f);
    }


    // The following function allows us to animate the viewport over time, then if desired, block those corners from moving again.
    public static void ChangeViewportSize(Vector4 viewportXYAB, bool[] blockTheseSides, float resizeTime)
    {

        ResizeViewport(GetViewportOffsets(), viewportXYAB, resizeTime, blockTheseSides);
    }
    private static Vector4 HideHiddenCornersFromThisVector4 (Vector4 xyabCoords)
    {
        //Vector4 xyabCoords = new Vector4(ViewportDownLeftOffsets.x, ViewportDownLeftOffsets.y, ViewportUpRightOffsets.x, ViewportUpRightOffsets.y);
        if (UIFramesToHide[(int)InterfaceFraming.BaseUIFrames.Top])
        {
            xyabCoords[3] = Screen.height;
        }
        if (UIFramesToHide[(int)InterfaceFraming.BaseUIFrames.Right])
        {
            xyabCoords[2] = Screen.width;
        }
        if (UIFramesToHide[(int)InterfaceFraming.BaseUIFrames.Left])
        {
            xyabCoords[0] = 0;
        }
        if (UIFramesToHide[(int)InterfaceFraming.BaseUIFrames.Bottom])
        {
            xyabCoords[1] = 0;
        }
        return xyabCoords;
    }

    public static void UnblockThisCorner(InterfaceFraming.BaseUIFrames frame)
    {
        UIFramesToBlockResizing[(int)frame] = false;
    }

    public static void UnhideThisCorner(InterfaceFraming.BaseUIFrames frame)
    {
        UIFramesToHide[(int)frame] = false;
    }


    void Start()
    {
        thisGameObject = this.gameObject;
        ViewportDownLeftOffsets = gameObject.GetComponent<RectTransform>().offsetMin;
        ViewportUpRightOffsets = gameObject.GetComponent<RectTransform>().offsetMax;
        UIFramesToHide[(int)InterfaceFraming.BaseUIFrames.Top] = true;
        UIFramesToHide[(int)InterfaceFraming.BaseUIFrames.Right] = true;
        UIFramesToHide[(int)InterfaceFraming.BaseUIFrames.Bottom] = true;
        UIFramesToBlockResizing[(int)InterfaceFraming.BaseUIFrames.Top] = true;
        UIFramesToBlockResizing[(int)InterfaceFraming.BaseUIFrames.Right] = true;
        UIFramesToBlockResizing[(int)InterfaceFraming.BaseUIFrames.Bottom] = true;
        UIFramesToBlockResizing[(int)InterfaceFraming.BaseUIFrames.Left] = false;
        gameObject.GetComponent<Image>().raycastPadding = new Vector4
            (-MaximumClickDistance, -MaximumClickDistance, -MaximumClickDistance, -MaximumClickDistance);


        gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width / 2 - Screen.width / 6, 0);
        gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(Screen.width, Screen.height);

    }



    // Update is called once per frame
    void Update()
    {
        ViewportDownLeftOffsets = gameObject.GetComponent<RectTransform>().offsetMin;
        ViewportUpRightOffsets = gameObject.GetComponent<RectTransform>().offsetMax;
        //Vector4 xyabPositionsOfViewport = GetViewportOffsets();
        if (ViewportSizeAnimator != null)
        {
            InterfaceListener.BlockAllButtonPresses = true;
            //Debug.Log("currPos" + savedCoords);
            ForceViewportIntoTheseOffsets(savedCoords);
            ViewportSizeAnimator.Animate();
        }
    }
}

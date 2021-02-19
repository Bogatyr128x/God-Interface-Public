using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceAnimation : InterfaceListener
{
    public enum AnimationType
    {
        basic,
        offset
    }
    public enum TransitionTo
    {
        RightOut,
        RightIn,
        LeftIn,
        LeftOut
    }



    public class MenuAnimation
    {
        internal Vector3 startPos;
        internal Vector3 endPos;
        internal Vector3 currPos;
        internal float startTime;
        internal float endTime;
        internal float progress;
        internal bool setActiveAfterEnding = false;

        public void Animate(InterfaceFabricator.GodInterfaceMenu menuBeingAnimated)
        {
            if (progress >= 1.0f)
            {
                menuBeingAnimated.gameObject.SetActive(setActiveAfterEnding);
                menuBeingAnimated.menuAnimation = null;
                return;
            }
            menuBeingAnimated.gameObject.SetActive(true);
            progress = 1 - ((endTime - Time.time) / (endTime - startTime));   // How much of the animation has happened, from 0 to 1.
            currPos.x = Mathf.SmoothStep(startPos.x, endPos.x, progress);
            currPos.y = Mathf.SmoothStep(startPos.y, endPos.y, progress);
            menuBeingAnimated.gameObject.transform.localPosition = currPos;
            BlockAllInteractionsInThisFrameDueToAnimations = true;
        }
    }
}

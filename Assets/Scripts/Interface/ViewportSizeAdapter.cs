using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewportSizeAdapter : MonoBehaviour
{

    private static RectTransform TopFrame;
    private static RectTransform RightFrame;
    private static RectTransform BottomFrame;
    private static RectTransform LeftFrame;
    // Start is called before the first frame update
    void Start()
    {
        TopFrame = GameObject.Find("ui-TopFrame").GetComponent<RectTransform>();
        RightFrame = GameObject.Find("ui-RightFrame").GetComponent<RectTransform>();
        BottomFrame = GameObject.Find("ui-BottomFrame").GetComponent<RectTransform>();
        LeftFrame = GameObject.Find("ui-LeftFrame").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector4 viewportXYABCoords = ViewportResizer.GetViewportOffsets();
        TopFrame.offsetMax = new Vector2(viewportXYABCoords[2], Screen.height);
        TopFrame.offsetMin = new Vector2(viewportXYABCoords[0], viewportXYABCoords[3]);

        RightFrame.offsetMax = new Vector2(Screen.width, Screen.height);
        RightFrame.offsetMin = new Vector2(viewportXYABCoords[2], 0);

        BottomFrame.offsetMax = new Vector2(viewportXYABCoords[2], viewportXYABCoords[1]);
        BottomFrame.offsetMin = new Vector2(viewportXYABCoords[0], 0);

        LeftFrame.offsetMax = new Vector2(viewportXYABCoords[0], Screen.height);
        LeftFrame.offsetMin = new Vector2(0, 0);


    }
}

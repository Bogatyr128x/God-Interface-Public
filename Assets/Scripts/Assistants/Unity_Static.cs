using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unity_Static : MonoBehaviour
{
    public static List<int> GetListOfNumbersBetweenAandB(int A, int B)
    {
        List<int> output = new List<int>();
        for (int i = A; i <= B; i++)
        {
            output.Add(i);
        }
        return output;
    }

    public static GameObject[] GetChildrenOfGameObject(GameObject inputGameObject, bool includeInactive = false)
    {
        Transform[] childTransforms = inputGameObject.GetComponentsInChildren<Transform>(includeInactive);

        List<GameObject> childrenGameObjs = new List<GameObject>();
        for (int i = 0; i < childTransforms.Length; i++)
        {
            GameObject parent = childTransforms[i].parent.gameObject;
            if (parent == inputGameObject)
            {
                childrenGameObjs.Add(childTransforms[i].gameObject);
            }
        }
        return childrenGameObjs.ToArray();
    }

    public static GameObject[] GetALLChildrenOfGameObject(GameObject inputGameObject, bool includeInactive = false)
    {
        Transform[] childTransforms = inputGameObject.GetComponentsInChildren<Transform>(includeInactive);
        List<GameObject> childrenGameObjs = new List<GameObject>();
        for (int i = 0; i < childTransforms.Length; i++)
        {
            childrenGameObjs.Add(childTransforms[i].gameObject);
        }
        return childrenGameObjs.ToArray();
    }


    public static GameObject GetChildWithGameObjectName(GameObject inputParent, string inputGOName)
    {
        GameObject[] children = GetALLChildrenOfGameObject(inputParent);
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].name == inputGOName)
            {
                return children[i];
            }
        }
        return null;
    }

    public static Vector2 GetWidthAndHeightOfUIObject(GameObject inputObj)
    {
        Vector2 output = new Vector2(0, 0);
        RectTransform rectTrans = inputObj.GetComponent<RectTransform>();
        output[0] = (rectTrans.rect.width);
        output[1] = (rectTrans.rect.height);
        return (output);
    }

    public static int GetSecondsSinceEpoch()
    {   // Credits to sonnyyap · 14 de Jan de 2015 07:26
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        return cur_time;
    }

    public static GameObject GetInstantiatedChild(GameObject inputParent, GameObject inputToInstantiate, bool firstChild = false)
    {
        if(firstChild == true)
        {
            GameObject output = (Instantiate(inputToInstantiate, inputParent.transform));
            output.transform.SetAsFirstSibling();
            return output;
        }
        else
        {
            return (Instantiate(inputToInstantiate, inputParent.transform));
        }
    }
}

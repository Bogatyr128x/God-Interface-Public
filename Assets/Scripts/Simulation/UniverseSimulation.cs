using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseSimulation : MonoBehaviour
{
    private static UniverseData CurrentUniverse;
    internal class UniverseData
    {
        public string UniverseName;
    }

    public static void SetCurrentUniverseName(string newUniverseName)
    {
        CurrentUniverse.UniverseName = newUniverseName;
    }

    public static void CreateUniverse(string universeName)
    {
        UniverseData newUniverse = new UniverseData();
        newUniverse.UniverseName = universeName;
        CurrentUniverse = newUniverse;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentUniverse != null)
        {
            Debug.Log(CurrentUniverse.UniverseName);
        }
    }
}

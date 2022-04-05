using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManagerScript : MonoBehaviour
{
    public GameObject[] dontDestroy;
    // Update is called once per frame
   
    private void Awake()
    {
        foreach (GameObject item in dontDestroy)
        {
            DontDestroyOnLoad(item);
        }
    }
}

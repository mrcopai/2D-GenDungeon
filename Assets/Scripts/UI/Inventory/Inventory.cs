using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory
{
    public static bool[] isFull = new bool[3] 
    { 
        false, false, false 
    };
    public static GameObject[] slots = new GameObject[3] 
    {
        GameObject.Find("Slot (0)"), 
        GameObject.Find("Slot (1)"), 
        GameObject.Find("Slot (2)") 
    };
    public static GameObject[] spriteHolder = new GameObject[3] 
    { 
        GameObject.Find("ItemLocation(0)"), 
        GameObject.Find("ItemLocation(1)"), 
        GameObject.Find("ItemLocation(2)") 
    };

}

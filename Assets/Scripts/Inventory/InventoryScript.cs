using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject[] spriteHolder;
    public GameObject WeaponHolder;
    private GameObject currentWeapon;
    //selected last inventory item
    GameObject lastselect;

    private void Start()
    {
        //make item to hold last selected item
        lastselect = new GameObject();
        lastselect.name = "LastSelected";
        DontDestroyOnLoad(lastselect);

        currentWeapon = WeaponHolder.transform.GetChild(0).gameObject;
    }
    void Update()
    {
        //if lost focus. refocus
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
        
    }
    public void WeaponShift()
    {
        GameObject SelectedItem = EventSystem.current.currentSelectedGameObject;
        if (SelectedItem != null)
        {
            int select = 0;
            if (SelectedItem.name == "Slot (1)")
            {
                select = 1;
            }
            else if (SelectedItem.name == "Slot (2)")
            {
                select = 2;
            }
            if (currentWeapon != slots[select] && slots[select] != SelectedItem)
            {
                if (currentWeapon.transform.parent != null)
                {
                    currentWeapon.SetActive(false);
                }
                slots[select].SetActive(true);
                currentWeapon = slots[select];
            }
        }
    }
}

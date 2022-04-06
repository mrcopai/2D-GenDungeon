using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public GameObject WeaponHolder;
    public GameObject currentWeapon = null;
    public GameObject[] itemLocations;
    //selected last inventory item
    GameObject lastselect;

    private void Start()
    {
        //make item to hold last selected item
        lastselect = new GameObject();
        lastselect.name = "LastSelected";
        bool firstweapon = true;
        for (int i = 0; i < WeaponHolder.transform.childCount; i++)
        {
            WeaponHolder.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < Inventory.isFull.Length; i++)
        {
            if (Inventory.isFull[i] == true)
            {
                Inventory.slots[i].gameObject.transform.SetParent(WeaponHolder.transform);
                Inventory.slots[i].gameObject.transform.localPosition = new Vector3(0, 0, -1);
                Inventory.slots[i].gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

                itemLocations[i].gameObject.GetComponent<SpriteRenderer>().sprite = Inventory.spriteHolder[i].gameObject.GetComponent<SpriteRenderer>().sprite;

                if (firstweapon)
                {
                    WeaponHolder.transform.GetChild(0).gameObject.SetActive(true);
                    currentWeapon = WeaponHolder.transform.GetChild(i).gameObject;
                    firstweapon = false;
                }
            }
        }
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
        WeaponHolder = GameObject.Find("WeaponHolder");
        if (WeaponHolder.transform.childCount>=1)
        {
            for (int i = 0; i < WeaponHolder.transform.childCount; i++)
            {
                if (WeaponHolder.transform.GetChild(i).gameObject.activeSelf == true)
                {
                    currentWeapon = WeaponHolder.transform.GetChild(i).gameObject;
                }
            }
        }
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
            if (currentWeapon != Inventory.slots[select] && Inventory.slots[select] != SelectedItem)
            {
                if (Inventory.slots[select] != null)
                {
                    if (currentWeapon != null)
                    {
                        currentWeapon.SetActive(false);
                    }
                    Inventory.slots[select].gameObject.SetActive(true);
                    currentWeapon = Inventory.slots[select];
                }
            }
        }
    }
}

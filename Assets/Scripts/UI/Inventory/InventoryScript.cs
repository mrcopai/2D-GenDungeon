using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public GameObject WeaponHolder;
    private GameObject currentWeapon = null;
    public GameObject[] itemLocations;
    //selected last inventory item
    GameObject lastselect;

    private void Awake()
    {
        //make item to hold last selected item
        lastselect = new GameObject();
        lastselect.name = "LastSelected";

        for (int i = 0; i < Inventory.isFull.Length; i++)
        {
            if (Inventory.isFull[i] == true)
            {
                Inventory.slots[i].gameObject.transform.SetParent(WeaponHolder.transform);
                Inventory.slots[i].gameObject.transform.localPosition = new Vector3(0, 0, -1);
                Inventory.slots[i].gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

                itemLocations[i].gameObject.GetComponent<SpriteRenderer>().sprite = Inventory.spriteHolder[i].gameObject.GetComponent<SpriteRenderer>().sprite;
                if (i == 0)
                {
                    Inventory.slots[i].gameObject.SetActive(true);
                }
            }
            else
            {
                break;
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
                if (currentWeapon != null)
                {
                    currentWeapon.SetActive(false);
                }
                Inventory.slots[select].SetActive(true);
                currentWeapon = Inventory.slots[select];
            }
        }
    }
}

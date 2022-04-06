using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectScript : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private InventoryScript inventory;
    [SerializeField]
    private GameObject Tooltiptext;
    private GameObject Weapon;
    private GameObject RemoveWeapon;
    private GameObject SelectedItem;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryScript>();
        Weapon = gameObject.transform.GetChild(0).gameObject;
        Tooltiptext = gameObject.transform.GetChild(1).gameObject;
        RemoveWeapon = gameObject.transform.GetChild(2).gameObject;
    }
    public void OnSelect(BaseEventData eventData)
    {
        inventory.WeaponShift();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Weapon.GetComponent<SpriteRenderer>().sprite != null)
        {
            int full = 0;
            foreach (bool item in Inventory.isFull)
            {
                if (item == true)
                {
                    full++;
                }
            }
            if (full > 1)
            {
                RemoveWeapon.SetActive(true);
            }
            SelectedItem = eventData.pointerEnter;
            int select = GetWeaponSpot(SelectedItem);
            GroundWeaponScript CurWeapon = Inventory.slots[select].gameObject.GetComponent<GroundWeaponScript>();
            Tooltiptext.GetComponent<Text>().text =
                    "Damage 	= " + CurWeapon.Damage.ToString() +
                    "\n Critrate = " + CurWeapon.CritRate.ToString() +
                    "\n Recharge speed = " + CurWeapon.Recharge.ToString() +
                    "\n Rarity = " + CurWeapon.rarety.ToString()
                .ToString();
            Tooltiptext.SetActive(true);
        }
    }

    private int GetWeaponSpot(GameObject Item)
    {
        int select = 0;
        if (Item != null)
        {
            if (Item.name == "Slot (1)")
            {
                select = 1;
            }
            else if (Item.name == "Slot (2)")
            {
                select = 2;
            }
        }
        return select;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Weapon.GetComponent<SpriteRenderer>().sprite != null)
        {
            RemoveWeapon.SetActive(false);
            Tooltiptext.SetActive(false);
        }
    }
    public void RemoveWeaponAction()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryScript>();
        int select = GetWeaponSpot(SelectedItem);
        GroundWeaponScript CurWeapon = Inventory.slots[select].gameObject.GetComponent<GroundWeaponScript>();
        CurWeapon.transform.SetParent(null);
        CurWeapon.gameObject.SetActive(true);
        CurWeapon.transform.localPosition = new Vector3(inventory.gameObject.transform.position.x - 2, inventory.gameObject.transform.position.y);
        CurWeapon.PickedUp = false;
        Inventory.slots[select] = GameObject.Find("Slot ("+select+")");
        Inventory.spriteHolder[select] = GameObject.Find("ItemLocation("+select+")");
        Inventory.isFull[select] = false;
        Weapon.GetComponent<SpriteRenderer>().sprite = null;
        inventory.currentWeapon = null;

        Tooltiptext.SetActive(false);
        RemoveWeapon.SetActive(false);
    }
}

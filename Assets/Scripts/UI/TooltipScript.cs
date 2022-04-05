using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipScript : MonoBehaviour
{
    [SerializeField]
    public Text tooltipText;
    private GameObject tooltipGameObject;
    private void Awake()
    {
        tooltipGameObject = GameObject.Find("Tooltip");
        tooltipText = tooltipGameObject.GetComponent<Text>();
    }

    private void OnMouseEnter()
    {
        GroundWeaponScript gw = gameObject.GetComponent<GroundWeaponScript>();
        if (gw.PickedUp == false)
        {
            tooltipGameObject.transform.position = Input.mousePosition;
            tooltipText.text =
                  " Damage = " + gw.Damage +
                "\n Critrate = " + gw.CritRate +
                "\n Recharge speed = " + gw.Recharge +
                "\n Rarity = " + gw.rarety;
        }
    }
    private void OnMouseExit()
    {
        tooltipText.text = "";
    }

}

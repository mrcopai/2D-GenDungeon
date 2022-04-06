using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TooltipScript : MonoBehaviour
{
    [SerializeField]
    public Text tooltipText;
    private GameObject tooltipGameObject;
    private void OnMouseEnter()
    {
        if (gameObject.GetComponent<GroundWeaponScript>() != null)
        {
            GroundWeaponScript gw = gameObject.GetComponent<GroundWeaponScript>();
            if (gw.PickedUp == false)
            {
                StartCoroutine(GetTooltip());
                tooltipGameObject.transform.position = Input.mousePosition;
                tooltipText.text =
                      " Damage = " + gw.Damage +
                    "\n Critrate = " + gw.CritRate +
                    "\n Recharge speed = " + gw.Recharge +
                    "\n Rarity = " + gw.rarety;
            }
        }
        
    }
    private void OnMouseExit()
    {
        StartCoroutine(GetTooltip());
        tooltipText.text = "";
    }
    public IEnumerator GetTooltip()
    {
        while (tooltipText == null)
        {
            tooltipGameObject = GameObject.FindGameObjectWithTag("Tooltip");
            tooltipText = tooltipGameObject.GetComponent<Text>();
            tooltipText.text = "";
        }
        yield return null;
    }


}

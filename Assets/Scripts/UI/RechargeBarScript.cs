using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RechargeBarScript : MonoBehaviour
{
    [SerializeField]
    private float MaxRecharge;
    [SerializeField]
    private Slider recharger;

    // Update is called once per frame
    void Update()
    {
        if (recharger.value >= recharger.maxValue || recharger.value ==0)
        {
            recharger.gameObject.SetActive(false);
        }
        else
        {
            recharger.gameObject.SetActive(true);
        }
        if (GameObject.Find("Player").GetComponent<InventoryScript>().currentWeapon != null)
        {
            MaxRecharge = GameObject.Find("Player").GetComponent<InventoryScript>().currentWeapon.GetComponent<GroundWeaponScript>().Recharge;
            recharger.maxValue = MaxRecharge;
            recharger.value = GameObject.Find("Player").GetComponent<InventoryScript>().currentWeapon.GetComponent<GroundWeaponScript>().Recharged;
        }
    }
}

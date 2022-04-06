using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    public int totalWeapons;
    public Sprite[] WeaponSprites;
    public GameObject WeaponPrefab;

    private void Start()
    {
        Instantiate(WeaponPrefab, transform.position = new Vector3(3,0,-1), Quaternion.identity);
    }

}

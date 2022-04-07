using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSript : MonoBehaviour
{
    [SerializeField]
    private float MaxHealth;
    [SerializeField]
    private Slider HealthBar;
    private void Start()
    {
        MaxHealth = GameObject.Find("Player").GetComponent<PlayerScript>().Health;
    }
    // Update is called once per frame
    void Update()
    {
        HealthBar.value = GameObject.Find("Player").GetComponent<PlayerScript>().Health;
    }
}

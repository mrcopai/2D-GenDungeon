using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameObject Target;
    public float health;

    private void Awake()
    {
        Target = GameObject.Find("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Target)
        {
            //deal Damage
        }
    }

    //flip Sprite when facing left
    public bool facingRight = true;
    private void Update()
    {
        GetComponent<SpriteRenderer>().flipX = Target.transform.position.x < transform.position.x;

    }
}

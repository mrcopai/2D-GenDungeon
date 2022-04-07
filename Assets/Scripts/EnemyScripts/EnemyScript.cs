using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameObject Target;
    public float health;
    public float damage;
    private void Awake()
    {
        Target = GameObject.Find("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Target)
        {
            Target.gameObject.GetComponent<PlayerScript>().Health -= damage;
        }
    }

    //flip Sprite when facing left
    public bool facingRight = true;
    private void Update()
    {
        GetComponent<SpriteRenderer>().flipX = Target.transform.position.x < transform.position.x;
        Vector3 pos = transform.position;
        pos.z = -1;
        transform.position = pos;
    }
}

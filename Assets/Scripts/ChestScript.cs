using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public Animator animator;
    public GameObject WeaponPrefab;
    private bool isopend = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameObject.Find("Player") && isopend == false)
        {
            isopend = true;
            animator.Play("ChestOpen");

            StartCoroutine(AnimationWaight());

        }
    }

    IEnumerator AnimationWaight()
    {

        yield return new WaitForSeconds(1f);
        Instantiate(WeaponPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

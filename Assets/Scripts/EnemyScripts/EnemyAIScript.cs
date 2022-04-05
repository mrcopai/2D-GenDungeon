using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask IgnoreMe;
    [SerializeField]
    Transform Target;
    [SerializeField]
    GameObject body;
    [SerializeField]
    Vector3 lastloc;
    [SerializeField]
    public float speed;
    public bool canMove = true;

    private void Awake()
    {
        Target = GameObject.Find("Player").transform;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.transform == Target)
        {
            Vector3 VectorToTarget = Target.position - transform.position;
            float angle = Mathf.Atan2(VectorToTarget.y, VectorToTarget.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            RaycastHit2D hit = Physics2D.Raycast(body.transform.position , transform.right , Mathf.Infinity, ~IgnoreMe );
            try
            {
                if (hit.transform.gameObject == Target.gameObject)
                {
                    lastloc = collision.gameObject.transform.position;
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
    private void Update()
    {
        if (lastloc != new Vector3(0,0,0) && 
            Vector2.Distance(body.transform.position,lastloc) > 1 &&
            canMove == true)
        {
            body.transform.position += (lastloc - body.transform.position).normalized * speed * Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public float speed;
    public Animator animator;
    private GameObject player;

    private void Start()
    {
        //find players
        player = GameObject.Find("Player");
        Application.targetFrameRate = 60;

    }

    private void Update()
    {
        //get input player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Movement(horizontalInput, verticalInput,player);

        animator.SetFloat("Hor Speed", Mathf.Abs(horizontalInput));
        animator.SetFloat("Ver Speed", Mathf.Abs(verticalInput));

        if (horizontalInput > 0 && !facingRight)
            Flip();
        else if (horizontalInput < 0 && facingRight)
            Flip();

    }
    private void Movement(float horizontalInput,float verticalInput, GameObject player)
    {
        //movement
        Vector2 movementDir = new Vector2(horizontalInput, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDir.magnitude);
        movementDir.Normalize();

        player.transform.Translate(movementDir * speed * inputMagnitude * Time.deltaTime, Space.World);
    }

    //flip Sprite when facing left
    public bool facingRight = true;
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
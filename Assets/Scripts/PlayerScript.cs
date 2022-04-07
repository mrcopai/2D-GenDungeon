using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float Health;
    public Animator animator;

    private void Start()
    {
        Application.targetFrameRate = 60;

    }

    private void Update()
    {
        //get input player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Movement(horizontalInput, verticalInput);

        animator.SetFloat("Hor Speed", Mathf.Abs(horizontalInput));
        animator.SetFloat("Ver Speed", Mathf.Abs(verticalInput));

        if (horizontalInput > 0 && !facingRight)
            Flip();
        else if (horizontalInput < 0 && facingRight)
            Flip();
        if (Health <=0)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            Health = 100;
            Inventory.isFull = new bool[3]
            { false, false, false };
            Inventory.slots = new GameObject[3]
            {
                GameObject.Find("Slot (0)"),
                GameObject.Find("Slot (1)"),
                GameObject.Find("Slot (2)")
            };
            Inventory.spriteHolder = new GameObject[3]
            {
                GameObject.Find("ItemLocation(0)"),
                GameObject.Find("ItemLocation(1)"),
                GameObject.Find("ItemLocation(2)")
            };
            SceneManager.LoadScene(("MainHub"), LoadSceneMode.Single);

        }
    }
    private void Movement(float horizontalInput,float verticalInput)
    {
        //movement
        Vector2 movementDir = new Vector2(horizontalInput, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDir.magnitude);
        movementDir.Normalize();

        transform.Translate(movementDir * speed * inputMagnitude * Time.deltaTime, Space.World);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity;
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public Vector2 velocity;
    public bool isGrounded = false;

    public bool isHoldingJump = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                isGrounded = true;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
            }
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            isHoldingJump = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if(!isGrounded)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;

            if(!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            if(pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        transform.position = pos;
    }
}

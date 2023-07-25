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
    public float maxHoldJumpTime = .4f;
    public float holdJumpTimer = .0f;

    public float jumpGroundThreshold = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);

        if(isGrounded || groundDistance <= jumpGroundThreshold)
        {

            if(Input.GetKeyDown("space"))
            {
                isGrounded = true;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;

            }
        }

        if(Input.GetKeyUp("space"))
        {
            isHoldingJump = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if(!isGrounded)
        {
            if(isHoldingJump)
            {
                    holdJumpTimer += Time.fixedDeltaTime;

                    if(holdJumpTimer >= maxHoldJumpTime)
                    {
                        isHoldingJump = false;
                    }
            }

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

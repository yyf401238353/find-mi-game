using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    bool isJump;
    bool isFall;
    public float jumpSpeed = 8.0f;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // input controll
        Vector2 speedNow = rigidbody2d.velocity;
        if(isJump && speedNow.y == 0)
        {
            isFall = true;
            isJump = false;
        } else if(isFall && speedNow.y == 0)
        {
            isFall = false;
        }



        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
            speedNow.x = -2.0f;
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            speedNow.x = 2.0f;
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
            speedNow.x = 0.0f;
        }
        if (Input.GetKey(KeyCode.Space) && !isFall && !isJump)
        {
            speedNow.y = jumpSpeed;
            isJump = true;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space))
        {
            rigidbody2d.velocity = speedNow;
        }
        //Input.GetKeyDown(KeyCode.W);
    }
}

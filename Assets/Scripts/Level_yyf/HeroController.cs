using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    bool isJump;
    bool isFall;
    public float jumpSpeed = 8.0f;
    int currentEnergy = 0;
    public int maxEnergy = 100;


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
        if (isJump && speedNow.y < 0)
        {
            isFall = true;
            isJump = false;
        }
        else if (isFall && speedNow.y >= 0)
        {
            isFall = false;
            isJump = false;
        }
        else {
            Debug.Log("isJump");
            Debug.Log(isJump);
            Debug.Log("isFall");
            Debug.Log(isFall);
            Debug.Log(speedNow.y);
        }




        //if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
        //    speedNow.x = -3.0f;
        //}
        //if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        //{
        //    speedNow.x = 3.0f;
        //}
        //if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
        //    speedNow.x = 0.0f;
        //}

        if (Input.GetKey(KeyCode.Space) && !isFall && !isJump)
        {
            speedNow.y = jumpSpeed;
            isJump = true;
        }
        speedNow.x = 3.0f;
        rigidbody2d.velocity = speedNow;
        //Input.GetKeyDown(KeyCode.W);
    }
    //public void StopYSpeed ()
    //{
    //    Vector2 speedNow = rigidbody2d.velocity;
    //    speedNow.y = 0;
    //    rigidbody2d.velocity = speedNow;
    //}
    public void ChangeEnergy(int amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
        Debug.Log(currentEnergy + "/" + maxEnergy);
    }
}

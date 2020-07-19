using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    bool isJump;
    bool isFall;
    public float jumpSpeed = 8.0f;
    int currentEnergy = 100;
    public int maxEnergy = 100;
    Animator animator;
    EnergyTextController EnergyText;
    private float waitTime = 0.5f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsJump", false);
        animator.SetBool("IsFall", false);
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

        if (Input.GetKey(KeyCode.Space) && !isFall && !isJump)
        {
            speedNow.y = jumpSpeed;
            isJump = true;
        }
        speedNow.x = 3.0f;
        rigidbody2d.velocity = speedNow;
        animator.SetBool("IsJump", isJump);
        animator.SetBool("IsFall", isFall);

        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            // Remove the recorded 0.5 seconds.
            timer = timer - waitTime;
            ChangeEnergy(-1);
        }
        //Input.GetKeyDown(KeyCode.W);
    }
    public void StopYSpeed ()
    {
        Vector2 speedNow = rigidbody2d.velocity;
        Debug.Log("reset");
        speedNow.y = 0;
        isFall = false;
        isJump = false;
        animator.SetBool("IsJump", isJump);
        animator.SetBool("IsFall", isFall);
        rigidbody2d.velocity = speedNow;
    }
    public void ChangeEnergy(int amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
        EnergyText = GameObject.Find("/UI/Text").GetComponent<EnergyTextController>();
        EnergyText.UpdatePercentage(currentEnergy);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    private float switchTime = 1.5f;
    private float timer = 0.0f;
    public float speed = 2f;
    public int direction = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        Vector2 speedNow = rigidbody2d.velocity;
        speedNow.y = speed;
        rigidbody2d.velocity = speedNow;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 speedNow = rigidbody2d.velocity;
        timer += Time.deltaTime;
        if (timer > switchTime)
        {
            // Change direction after 1.5s
            timer = timer - switchTime;
            speedNow.y = -speedNow.y;
            direction = direction == 0 ? 1 : 0;
        }
        rigidbody2d.velocity = speedNow;
    }
}

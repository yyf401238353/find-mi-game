using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float speed;
    bool isJump;
    bool isFall;
    float horizontal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // input controll
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
            horizontal = -1.0f;
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            horizontal = 1.0f;
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
            horizontal = 0.0f;
        }

        //Input.GetKeyDown(KeyCode.W);
        Vector2 position = transform.position;
        position.x = position.x + 3.0f * horizontal * Time.deltaTime;
        transform.position = position;
    }
}

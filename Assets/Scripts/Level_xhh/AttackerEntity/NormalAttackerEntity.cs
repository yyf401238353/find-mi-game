using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalAttackerEntity : AttackerEntity
{
    [Header("追击速度")]
    public float MoveSpeed;

    private Vector2 originalPos;
    private Vector2 targetPos;
    private Rigidbody2D myRigidbody2D;
    private bool isInit = false;

    // Start is called before the first frame update
    void Start()
    {
        this.myRigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (this.isInit)
        {
            float distance = (this.targetPos - (Vector2)this.transform.position).magnitude;
            Vector2 direction = (this.targetPos - (Vector2)this.transform.position).normalized;
            Debug.Log(distance);
            this.myRigidbody2D.velocity = Vector2.Lerp(this.myRigidbody2D.velocity, direction * this.MoveSpeed * distance, this.MoveSpeed);
        }
    }

    public override void entityInit(Vector3 originalPos, Vector3 targetPos)
    {
        this.originalPos = originalPos;
        this.transform.position = originalPos;
        this.targetPos = targetPos;
        this.isInit = true;
    }

}

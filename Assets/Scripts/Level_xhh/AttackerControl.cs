using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerControl : MonoBehaviour
{
    [Header("跟随速度")]
    public float FollowSpeed;

    private Rigidbody2D myRigidbody2D;

    private GameObject targetObj;
    private GameObject heroFollowPoint;

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
        if (this.targetObj)
        {
            this.followTarget();
        }
    }

    private void followTarget()
    {
        float distance = (this.targetObj.transform.position - this.transform.position).magnitude;
        Vector2 direction = (this.targetObj.transform.position - this.transform.position).normalized;

        this.myRigidbody2D.velocity = Vector2.Lerp(this.myRigidbody2D.velocity, direction * this.FollowSpeed * distance, this.FollowSpeed);
    }

    public void initAttacker(GameObject heroFollowPoint)
    {
        this.heroFollowPoint = heroFollowPoint;
        this.targetObj = heroFollowPoint;
    }
}

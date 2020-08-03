using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjMove : TriggerObj
{
    public Vector3 TargetPos;
    public float Speed;

    private bool isActive = false;
    private AudioSource myAudioSource;
    private Vector3 startPos;
    private float startTime = -1;

    // Start is called before the first frame update
    void Start()
    {
        this.myAudioSource = this.GetComponent<AudioSource>();
        this.startPos = this.transform.position;
    }


    void FixedUpdate()
    {
        if (this.isActive)
        {
            if (this.startTime < 0)
            {
                this.startTime = Time.time;
            }

            this.transform.position = Vector3.Lerp(this.startPos, this.TargetPos, (Time.time - this.startTime) * this.Speed);

            if ((this.transform.position - this.TargetPos).magnitude < 0.0001f)
            {
                this.myAudioSource.Stop();
            }
        }
    }

    public override void OnTriggerEvent()
    {
        this.isActive = true;
        this.myAudioSource.Play();
    }
}

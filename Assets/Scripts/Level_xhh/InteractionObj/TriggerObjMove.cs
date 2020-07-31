using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjMove : TriggerObj
{
    public Vector3 TargetPos;
    public float Speed;

    private bool isActive = false;
    private AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.myAudioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isActive)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, this.TargetPos, this.Speed);

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

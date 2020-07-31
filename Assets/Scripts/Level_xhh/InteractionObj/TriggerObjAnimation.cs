using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjAnimation : TriggerObj
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnTriggerEvent()
    {
        this.GetComponent<Animation>().Play();
    }
}

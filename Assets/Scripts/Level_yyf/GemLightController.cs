using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class GemLightController : MonoBehaviour
{
    Light2D gemLight;
    float timer = 0;
    float interval = 2.0f;
    bool isBrighting = false;
    // Start is called before the first frame update
    void Start()
    {
        gemLight = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isBrighting)
        {
            gemLight.pointLightOuterRadius += 0.02f;
        } else
        {
            gemLight.pointLightOuterRadius -= 0.02f;
        }

        timer += Time.deltaTime;
        if (timer > interval)
        {
            // Remove the recorded 0.5 seconds.
            timer = timer - interval;
            isBrighting = !isBrighting;
        }
    }
}

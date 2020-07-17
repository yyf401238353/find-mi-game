using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        HeroController controller = collision.gameObject.GetComponent<HeroController>();

        controller.StopYSpeed();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

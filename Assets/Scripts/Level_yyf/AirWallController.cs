using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AirWallController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        transform.localPosition = new Vector3(-width / 2, 0, 10);
        Debug.Log(height);
        Debug.Log(width);
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        HeroController controller = other.GetComponent<HeroController>();
        if (controller != null)
        {
            controller.GameOver();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AirWallController : MonoBehaviour
{
    // Start is called before the first frame update
    CinemachineVirtualCamera camera;
    void Start()
    {
        camera = transform.parent.gameObject.GetComponent<CinemachineVirtualCamera>();
        Debug.Log(camera);
        transform.localPosition = new Vector3 (0, 0,10);
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}

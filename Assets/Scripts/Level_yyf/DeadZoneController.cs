using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    GameObject EndText;
    // Start is called before the first frame update
    void Start()
    {
        EndText = GameObject.Find("/UI/EndText");
        EndText.SetActive(false);
        Debug.Log(EndText);
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
            controller.PauseGame();
            EndText.SetActive(true);
        }
    }
}

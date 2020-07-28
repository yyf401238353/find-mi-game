﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportorController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        HeroController controller = other.GetComponent<HeroController>();
        if (controller != null)
        {
            controller.GameWin();
        }
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

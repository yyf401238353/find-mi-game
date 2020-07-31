using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGemControll : MonoBehaviour
{
    public AudioClip collectSE;
    private AudioSource source;
    public Renderer rend;
    void OnTriggerEnter2D(Collider2D other)
    {
        HeroController  controller = other.GetComponent<HeroController>();
        source = GetComponent<AudioSource>();
        if (controller != null)
        {
            source.PlayOneShot(collectSE);
            controller.ChangeEnergy(20);
            rend.enabled = false;
            Destroy(gameObject , collectSE.length);
            Destroy(transform.Find("Point Light 2D").gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

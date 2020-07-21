using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        HeroController hero = collision.gameObject.GetComponent<HeroController>();
        HeroBulletController hero_bullet = collision.gameObject.GetComponent<HeroBulletController>();
        if (hero != null)
        {
            hero.StopYSpeed();
        }
        if (hero_bullet != null)
        {
            StartCoroutine(hero_bullet.DestroyBullet());
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

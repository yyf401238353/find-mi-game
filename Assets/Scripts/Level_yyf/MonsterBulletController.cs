using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Collider2D m_Collider;
    Animator animator;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsDestroyed", false);
    }

    public void Launch()
    {
        Vector2 SpeedNow = rigidbody2d.velocity;
        SpeedNow.x = -4.5f;
        rigidbody2d.velocity = SpeedNow;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        HeroController controller = other.GetComponent<HeroController>();
        HeroBulletController hero_bullet = other.GetComponent<HeroBulletController>();
        if (controller != null)
        {
            //controller.GameOver();
            controller.ChangeEnergy(-10);
            controller.BeUntouch();
            StartCoroutine(DestroyBullet());
        }
        if (hero_bullet != null)
        {
            StartCoroutine(hero_bullet.DestroyBullet());
            StartCoroutine(DestroyBullet());
            
        }
    }
    public IEnumerator DestroyBullet()
    {
        Vector2 SpeedNow = rigidbody2d.velocity;
        SpeedNow.x = 0;
        SpeedNow.y = 0;
        rigidbody2d.velocity = SpeedNow;
        m_Collider.enabled = false;
        animator.SetBool("IsDestroyed", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

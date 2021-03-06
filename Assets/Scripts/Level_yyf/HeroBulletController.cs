using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBulletController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Animator animator;
    Collider2D m_Collider;

    float liftTime = 1.75f;
    float timer = 0;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        m_Collider = GetComponent<Collider2D>();
    }

    public void Launch()
    {
        Vector2 SpeedNow = rigidbody2d.velocity;
        SpeedNow.x = 8.0f;
        rigidbody2d.velocity = SpeedNow;
        animator.SetBool("IsDestroyed", false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > liftTime)
        {
            StartCoroutine(DestroyBullet());
        }
    }

    public IEnumerator DestroyBullet()
    {
        Vector2 SpeedNow = rigidbody2d.velocity;
        SpeedNow.x = 0;
        SpeedNow.y = 0;
        rigidbody2d.velocity = SpeedNow;
        animator.SetBool("IsDestroyed", true);
        m_Collider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

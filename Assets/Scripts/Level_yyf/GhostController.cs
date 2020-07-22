using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    private float switchTime = 1.5f;
    private float timer = 0.0f;
    public float speed = 2f;
    public int direction = 0;
    public int maxHealth = 3;
    int currentHealth;

    Animator animator;
    Collider2D m_Collider;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        Vector2 speedNow = rigidbody2d.velocity;

        currentHealth = maxHealth;
        speedNow.y = speed;
        rigidbody2d.velocity = speedNow;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 speedNow = rigidbody2d.velocity;
        timer += Time.deltaTime;
        if (timer > switchTime)
        {
            // Change direction after 1.5s
            timer = timer - switchTime;
            speedNow.y = -speedNow.y;
            direction = direction == 0 ? 1 : 0;
        }
        rigidbody2d.velocity = speedNow;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HeroController hero = collision.gameObject.GetComponent<HeroController>();
        HeroBulletController hero_bullet = collision.gameObject.GetComponent<HeroBulletController>();
        if (hero != null)
        {
            
        }
        if (hero_bullet != null)
        {
            StartCoroutine(hero_bullet.DestroyBullet());
            currentHealth = Mathf.Clamp(currentHealth - 1, 0, maxHealth);
            Debug.Log(currentHealth);
            if (currentHealth == 0)
            {
               StartCoroutine(DestroyGhost());
            }
        }
    }
    public IEnumerator DestroyGhost()
    {
        Vector2 SpeedNow = rigidbody2d.velocity;
        SpeedNow.x = 0;
        SpeedNow.y = 0;
        rigidbody2d.velocity = SpeedNow;
        animator.SetBool("IsDestroyed", true);
        m_Collider.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

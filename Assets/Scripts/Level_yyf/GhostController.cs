using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    private float switchTime = 2f;
    private float timer = 0.0f;

    private float shotTime = 0.8f;
    private float shotTimer = 0.0f;

    public float speed = 2f;
    public int direction = 0;
    public int maxHealth = 2;
    int currentHealth;
    float distanceToHero;

    public GameObject projectilePrefab;
    public GameObject gemPrefab;
    Animator animator;
    Collider2D m_Collider;
    GameObject Hero;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        Hero = GameObject.Find("/Hero");
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
            timer -= switchTime;
            speedNow.y = -speedNow.y;
            direction = direction == 0 ? 1 : 0;
        }

        // Control bullet project


        rigidbody2d.velocity = speedNow;
        distanceToHero = Vector2.Distance(transform.position, Hero.transform.position);

        if (distanceToHero < 10f)
        {
            shotTimer += Time.deltaTime;
            if (shotTimer > shotTime && transform.position.x > Hero.transform.position.x && animator.GetBool("IsDestroyed") == false)
            {
                shotTimer -= shotTime;
                Launch();
            }
        }
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.left * 0.5f, Quaternion.identity);

        MonsterBulletController projectile = projectileObject.GetComponent<MonsterBulletController>();
        projectile.Launch();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HeroController controller = other.GetComponent<HeroController>();
        HeroBulletController hero_bullet = other.GetComponent<HeroBulletController>();
        if (controller != null)
        {
            controller.ChangeEnergy(-20);
            controller.BeUntouch();
        }
        if (hero_bullet != null)
        {
            StartCoroutine(hero_bullet.DestroyBullet());
            currentHealth = Mathf.Clamp(currentHealth - 1, 0, maxHealth);
            if (currentHealth == 0)
            {
                StartCoroutine(DestroyGhost());
            }
        }
    }
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    HeroController hero = collision.gameObject.GetComponent<HeroController>();
    //    HeroBulletController hero_bullet = collision.gameObject.GetComponent<HeroBulletController>();
    //    if (hero != null)
    //    {
            
    //    }
    //    if (hero_bullet != null)
    //    {
    //        StartCoroutine(hero_bullet.DestroyBullet());
    //        currentHealth = Mathf.Clamp(currentHealth - 1, 0, maxHealth);
    //        Debug.Log(currentHealth);
    //        if (currentHealth == 0)
    //        {
    //           StartCoroutine(DestroyGhost());
    //        }
    //    }
    //}
    public IEnumerator DestroyGhost()
    {
        Vector2 SpeedNow = rigidbody2d.velocity;
        SpeedNow.x = 0;
        SpeedNow.y = 0;
        rigidbody2d.velocity = SpeedNow;
        animator.SetBool("IsDestroyed", true);
        m_Collider.enabled = false;
        yield return new WaitForSeconds(1f);
        Instantiate(gemPrefab, rigidbody2d.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

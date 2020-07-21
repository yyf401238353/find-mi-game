using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 正方向（没有旋转）的方向是朝向左边的
/// </summary>
public class EnemyLineShootAttack : EnemyAttackBase
{
    [Header("伤害")]
    public int Damage;
    [Header("飞行速度")]
    public float Speed;
    [Header("最大飞行范围")]
    public float MaxRange;


    private Vector2 originalPos;
    private bool isInited = false;
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (this.isInited)
        {
            float distance = (this.originalPos - (Vector2)this.transform.position).magnitude;

            if (distance >= this.MaxRange)
            {
                Destroy(this.transform.gameObject);
            }
        }

    }

    public override void init(Vector2 originalPos, Vector2 enemyVelocity)
    {
        bool isLeft = enemyVelocity.normalized.x < 0;
        this.myRigidbody = this.GetComponent<Rigidbody2D>();
        this.myRigidbody.velocity = new Vector2(this.Speed * (isLeft ? -1 : 1), 0);
        this.isInited = true;
        this.originalPos = originalPos;
        this.transform.rotation = Quaternion.Euler(0, isLeft ? 0 : 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Road")
        {
            Destroy(this.transform.gameObject);
        }
        else if (collision.gameObject.tag == "Hero")
        {
            Hero hero = collision.GetComponent<Hero>();
            hero.heroBeAttacked(this.transform.position);
            Destroy(this.transform.gameObject);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 注意，使用的时候需要怪物默认朝向左边，并且此时的旋转角为0
/// 线性巡逻敌人
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class LinePatrol : MoveAndLogicBase
{

    [Header("巡逻移动速度")]
    public float MoveSpeed;
    [Header("x轴移动范围,左边界")]
    public float PatorlXRangeLeft;
    [Header("x轴移动范围,右边界")]
    public float PatorlXRangeRight;
    [Header("受伤颜色")]
    public Color InjuredColor;
    [Header("受伤的力度")]
    public float InjuredStrength;
    [Header("受伤的时间")]
    public float InjuredTime;
    [Header("发起攻击的时间间隔")]
    public float AttackT;
    [Header("伤害的理由")]
    public string AttackReason;

    private bool isMoveLeft;
    private Rigidbody2D myRigibody;
    private SpriteRenderer myRender;
    private bool isGetInjured = false;
    private Vector3 injuredPoint;
    private AudioSource myAudioSource;
    private float attackClock = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.myRigibody = this.GetComponent<Rigidbody2D>();
        this.myRender = this.GetComponent<SpriteRenderer>();
        this.myAudioSource = this.GetComponent<AudioSource>();

        this.isMoveLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        // 不受伤的时候执行巡逻和攻击操作
        if (!this.isGetInjured)
        {
            this.patrol();
            this.attack();
        }

    }

    private void attack()
    {
        this.attackClock += Time.deltaTime;

        if (this.attackClock >= this.AttackT)
        {
            // 存在受伤音频，则播放
            if (this.AttackAudioInfo.clip)
            {
                this.myAudioSource.clip = this.AttackAudioInfo.clip;
                this.myAudioSource.volume = this.AttackAudioInfo.volume;
                this.myAudioSource.Play();

            }

            this.attackClock = 0;
            EnemyAttackBase attacker = Instantiate<EnemyAttackBase>(this.AttackerPart, this.transform.parent);
            attacker.gameObject.transform.position = this.AttackStartPoint.transform.position;
            attacker.init(attacker.gameObject.transform.position, this.myRigibody.velocity);
        }
    }

    private void patrol()
    {
        Vector2 nowPosition = this.transform.position;

        // 到达左边界，则朝右边
        if (nowPosition.x <= this.PatorlXRangeLeft)
        {
            this.isMoveLeft = false;
        }
        else if (nowPosition.x >= this.PatorlXRangeRight)
        {
            this.isMoveLeft = true;
        }

        // 需要朝向左边
        if (this.isMoveLeft)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.myRigibody.velocity = new Vector2(-this.MoveSpeed, 0);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
            this.myRigibody.velocity = new Vector2(this.MoveSpeed, 0);
        }


    }

    public override void attackedByHero(Vector3 attackPoint, int damage)
    {
        this.injuredPoint = attackPoint;
        // 存在受伤音频，则播放
        if (this.InjuredAudioInfo.clip)
        {
            this.myAudioSource.clip = this.InjuredAudioInfo.clip;
            this.myAudioSource.volume = this.InjuredAudioInfo.volume;
            this.myAudioSource.Play();

        }

        StopCoroutine("getInjuredEffect");
        StartCoroutine("getInjuredEffect");

        this.getInjured(damage);
    }

    private IEnumerator getInjuredEffect()
    {
        Vector2 direction = (this.transform.position - this.injuredPoint).normalized;

        this.isGetInjured = true;
        this.myRigibody.velocity = new Vector2(direction.x > 0 ? 1 : -1, 0) * this.InjuredStrength;
        this.myRender.color = this.InjuredColor;
        yield return new WaitForSeconds(this.InjuredTime);

        this.myRender.color = Color.white;
        this.isGetInjured = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            UIControl.setDeadReason(this.AttackReason);
            collision.gameObject.GetComponent<Hero>().heroBeAttacked(this.transform.position, this.CrashDamage);

        }
    }
}

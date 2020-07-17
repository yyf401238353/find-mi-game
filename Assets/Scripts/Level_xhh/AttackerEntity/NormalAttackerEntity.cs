using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalAttackerEntity : AttackerEntity
{
    [Header("追击速度")]
    public float MoveSpeed;
    [Header("攻击范围")]
    public float AttackRange;
    [Header("发射攻击音效")]
    public AudioInfo StartAttackAudioInfo;

    private GameObject followHeroPoint;
    private Vector2 targetPos;
    private Rigidbody2D myRigidbody2D;
    private AudioSource myAudioSource;
    private Vector2 moveDirection;
    private bool isInit = false;

    // Start is called before the first frame update
    void Start()
    {
        this.myRigidbody2D = this.GetComponent<Rigidbody2D>();
        this.myAudioSource = this.GetComponent<AudioSource>();

        this.myAudioSource.clip = this.StartAttackAudioInfo.clip;
        this.myAudioSource.volume = this.StartAttackAudioInfo.volume;
        this.myAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isInit)
        {
            return;
        }
        // 超出范围，则直接结束攻击
        if (this.checkIsOutofRange())
        {
            this.overAttack();
        }
    }

    private void FixedUpdate()
    {
        if (this.isInit)
        {
            this.myRigidbody2D.velocity = Vector2.Lerp(this.myRigidbody2D.velocity, this.moveDirection * this.MoveSpeed, this.MoveSpeed);
        }
    }

    private bool checkIsOutofRange()
    {
        Vector2 mePos = this.transform.position;
        Vector2 followPos = this.followHeroPoint.transform.position;
        float distance = (mePos - followPos).magnitude;

        return distance >= this.AttackRange;
    }

    public override void entityInit(GameObject followHeroPoint, Vector3 targetPos)
    {
        this.followHeroPoint = followHeroPoint;
        this.transform.position = this.followHeroPoint.transform.position;
        this.targetPos = targetPos;
        this.moveDirection = (this.targetPos - (Vector2)this.transform.position).normalized;
        this.isInit = true;
    }

    private void overAttack()
    {
        GameObject boom = Instantiate(this.AttackOver, this.transform.parent);
        boom.transform.position = this.transform.position;
        // 生成boom的效果
        Destroy(boom, 5);
        // 销毁自己
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 撞到了路上
        if (collision.gameObject.tag == "Road")
        {
            this.overAttack();
        }
        // 撞到了敌人，则对齐造成伤害
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<MoveLogicBase>().attackedByHero(this.transform.position);
            this.overAttack();
        }

    }

}

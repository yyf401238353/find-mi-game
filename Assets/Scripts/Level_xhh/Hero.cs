using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(BoxCollider2D))]
public class Hero : MonoBehaviour
{
    public enum Status
    {
        /// <summary>
        /// 未出生,此状态由 HeroBorn 来管理，出生之后转到默认状态 STATIC
        /// </summary>
        NOT_BORN = 1,
        /// <summary>
        /// 静止站立, 出生之后的默认状态
        /// </summary>
        STATIC = 2,
        /// <summary>
        /// 跑动
        /// </summary>
        RUNNING = 3,
        /// <summary>
        /// 向上跳动
        /// </summary>
        JUMP_UP = 4,
        /// <summary>
        /// 二段跳向上
        /// </summary>
        DOUBLE_JUMP_UP = 5,
        /// <summary>
        /// 降落
        /// </summary>
        JUMP_DOWN = 6,
        /// <summary>
        /// 受伤状态
        /// </summary>
        INJURED = 7
    }


    [Header("水平移动速度")]
    public float HorizontalVelocity;
    [Header("垂直移动速度")]
    public float JumpVerticalVelocity;
    [Header("受伤力度")]
    public float InjuredStrength;
    [Header("出生HP")]
    public int BornHp;
    [Header("死亡特效")]
    public GameObject DeadObjPrefab;

    private HeroAttackerControl heroAttackerControl;

    private HeroAnimationControl myAnimationControl;
    private HeroParticlesControl myParticlesControl;
    private HeroAudioControl myAudioControl;
    private Status nowStatus = Status.NOT_BORN;
    private float nowHorizontalVelocity = 0;
    private Rigidbody2D myRigidbody;

    private bool isStandInRoad = false;
    private List<KeyCode> horizontalPressKey = new List<KeyCode>();
    private int heroHp;

    // Start is called before the first frame update
    void Start()
    {
        this.heroHp = this.BornHp;
        this.myRigidbody = this.GetComponent<Rigidbody2D>();
        this.myParticlesControl = this.GetComponent<HeroParticlesControl>();
        this.heroAttackerControl = this.GetComponent<HeroAttackerControl>();
        this.myAudioControl = this.GetComponent<HeroAudioControl>();
    }

    void Update()
    {

        this.keyboardReaction();
        this.refreshStatus();
        this.updateHorizontalVelocity();
    }

    private void keyboardReaction()
    {
        this.disposeHorizontalKeys();
        this.disposeVecticalKeys();
    }

    private void updateHorizontalVelocity()
    {
        // 只有允许控制的情况下，x轴速度才会刷新
        if (this.isAlloweHorizontalControl)
        {
            Vector2 velocity = this.myRigidbody.velocity;
            velocity.x = this.nowHorizontalVelocity;
            this.myRigidbody.velocity = velocity;
        }
    }

    /// <summary>
    /// 刷新状态
    /// </summary>
    private void refreshStatus()
    {
        bool isInJumpUpStatus = this.NowStatus == Status.JUMP_UP || this.NowStatus == Status.DOUBLE_JUMP_UP;
        if (isInJumpUpStatus && this.myRigidbody.velocity.y < 0)
        {
            this.NowStatus = Status.JUMP_DOWN;
        }
        else if (this.NowStatus == Status.JUMP_DOWN && this.isStandInRoad)
        {
            this.NowStatus = Status.STATIC;
        }
        else if (this.NowStatus == Status.INJURED && this.isStandInRoad)
        {
            if (System.Math.Abs(this.myRigidbody.velocity.y) < 0.0001f)
            {
                this.NowStatus = Status.STATIC;
            }
        }

    }

    private void disposeVecticalKeys()
    {
        List<Status> allowSetYVelocityStatus = new List<Status>(new Status[] { Status.RUNNING, Status.STATIC });

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 velocity = this.myRigidbody.velocity;
            // 站在地上，并且属于允许设置Y轴的状态，则给予Y初始速度
            if (allowSetYVelocityStatus.Contains(this.NowStatus) && this.isStandInRoad)
            {
                velocity.y = this.JumpVerticalVelocity;
                this.NowStatus = Status.JUMP_UP;
            }
            else if (this.NowStatus == Status.JUMP_UP)
            {
                velocity.y = this.JumpVerticalVelocity;
                this.NowStatus = Status.DOUBLE_JUMP_UP;
                // 二段跳的时候可以强制刷新方向
                this.disposeHorizontalKeys(true);
                velocity.x = this.nowHorizontalVelocity;
                this.myRigidbody.velocity = velocity;
            }
            this.myRigidbody.velocity = velocity;
        }
    }

    /// <summary>
    /// 处理水平按键
    /// </summary>
    /// <param name="forceToChange">是否强行使左右按键生效，无需考虑是否站在地上,并且也不会改变状态</param>
    private void disposeHorizontalKeys(bool forceToChange = false)
    {
        KeyCode[] keycodes = { KeyCode.A, KeyCode.D };

        foreach (KeyCode code in keycodes)
        {
            if (Input.GetKeyDown(code))
            {
                this.horizontalPressKey.Add(code);
            }
            if (Input.GetKeyUp(code))
            {
                this.horizontalPressKey.Remove(code);
            }

        }

        // 如果没有任何水平按键按下时，是正在地面上奔跑，则会将水平速度放置为0
        if (this.horizontalPressKey.Count == 0 && this.isAlloweHorizontalControl)
        {
            this.NowStatus = Status.STATIC;
            this.nowHorizontalVelocity = 0;
        }
        // 当有按键按下并且正站在地面上，则，可左右移动
        else if (this.horizontalPressKey.Count > 0 && this.isStandInRoad)
        {
            bool isMoveRight = this.horizontalPressKey[this.horizontalPressKey.Count - 1] == KeyCode.D;

            if (this.isAlloweHorizontalControl)
            {
                float rotateY = isMoveRight ? 0 : 180;
                this.NowStatus = Status.RUNNING;
                this.nowHorizontalVelocity = isMoveRight ? this.HorizontalVelocity : -this.HorizontalVelocity;
                this.transform.rotation = Quaternion.Euler(new Vector3(0, rotateY, 0));
            }
        }


        if (this.horizontalPressKey.Count > 0 && forceToChange)
        {
            bool isMoveRight = this.horizontalPressKey[this.horizontalPressKey.Count - 1] == KeyCode.D;
            float rotateY = isMoveRight ? 0 : 180;
            this.nowHorizontalVelocity = isMoveRight ? this.HorizontalVelocity : -this.HorizontalVelocity;
            this.transform.rotation = Quaternion.Euler(new Vector3(0, rotateY, 0));
        }
    }


    /// <summary>
    /// 英雄出生事件
    /// </summary>
    public void heroBorn()
    {
        this.myAnimationControl = this.GetComponentInChildren<HeroAnimationControl>();
        // 此处不可使用 NowStatus，因为此时 myAnimationControl 没有加载完成
        // 考虑到默认动画就是STATIC，故而，直接使用
        this.nowStatus = Status.STATIC;
        // 开启攻击反应
        this.heroAttackerControl.AttackActive = true;
    }

    public void heroBeAttacked(Vector3 attackPoint, int damage)
    {
        this.heroHp -= damage;

        if (this.heroHp < 0)
        {
            this.heroHp = 0;
        }

        if (this.heroHp > 0)
        {
            bool isToLeft = (this.transform.position - attackPoint).x < 0;
            Vector2 direction = new Vector2(this.InjuredStrength * (isToLeft ? -1 : 1), this.InjuredStrength * 0.9f);
            this.myRigidbody.velocity = direction;
            this.NowStatus = Status.INJURED;
        }
        else
        {
            GameObject dead = Instantiate(this.DeadObjPrefab);
            dead.transform.position = this.transform.position;
            // 通知UI，英雄已经死亡
            UIControl.UnityIns.HeroDead();
            Destroy(this.gameObject);
        }

    }

    public int getHeroHp() { return this.heroHp; }

    public void addHpToHero(int hp)
    {
        this.heroHp += hp;
    }

    /// <summary>
    /// 是否允许水平轴控制
    /// </summary>
    private bool isAlloweHorizontalControl
    {
        get
        {
            return this.NowStatus == Status.STATIC || this.NowStatus == Status.RUNNING;
        }
    }

    private Status NowStatus
    {
        set
        {
            if (this.nowStatus != value)
            {
                this.nowStatus = value;
                this.myAnimationControl.setHeroStatusParam(value);
                this.myParticlesControl.setHeroStatus(value);
                this.myAudioControl.setStatus(value);
            }

        }

        get
        {
            return this.nowStatus;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 站在了地面
        if (collision.gameObject.tag == "Road")
        {
            this.isStandInRoad = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 离开了地面
        if (collision.gameObject.tag == "Road")
        {
            this.isStandInRoad = false;
        }
    }
}

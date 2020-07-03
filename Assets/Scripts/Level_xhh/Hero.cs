using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private HeroAnimationControl myAnimationControl;
    private Status nowStatus = Status.NOT_BORN;
    private float nowHorizontalVelocity = 0;
    private Rigidbody2D myRigidbody;

    private List<KeyCode> horizontalPressKey = new List<KeyCode>();

    // Start is called before the first frame update
    void Start()
    {
        this.myRigidbody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (this.NowStatus != Status.NOT_BORN)
        {
            this.keyboardReaction();
            // 更新速度
            this.myRigidbody.velocity = new Vector2(this.nowHorizontalVelocity, this.myRigidbody.velocity.y);


            this.preventSlopeSlide();
        }
    }

    private void keyboardReaction()
    {
        this.disposeHorizontalKeys();
    }

    private void disposeHorizontalKeys()
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
        if (this.horizontalPressKey.Count == 0 && this.NowStatus == Status.RUNNING)
        {
            this.NowStatus = Status.STATIC;
            this.nowHorizontalVelocity = 0;
        }
        else if (this.horizontalPressKey.Count > 0)
        {
            bool isMoveRight = this.horizontalPressKey[this.horizontalPressKey.Count - 1] == KeyCode.D;

            if (this.NowStatus == Status.RUNNING || this.NowStatus == Status.STATIC)
            {
                float rotateY = isMoveRight ? 0 : 180;
                this.NowStatus = Status.RUNNING;
                this.nowHorizontalVelocity = isMoveRight ? this.HorizontalVelocity : -this.HorizontalVelocity;
                this.transform.rotation = Quaternion.Euler(new Vector3(0, rotateY, 0));
            }
        }
    }

    private void preventSlopeSlide()
    {
        // 在静止状态下，Y轴速度为0，防止在斜坡上下滑
        if (this.NowStatus == Status.STATIC)
        {
            this.myRigidbody.velocity = new Vector2(this.myRigidbody.velocity.x, 0);
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
    }


    private Status NowStatus
    {
        set
        {
            if (this.nowStatus != value)
            {
                this.nowStatus = value;
                this.myAnimationControl.setHeroStatusParam((int)value);
            }

        }

        get
        {
            return this.nowStatus;
        }
    }
}

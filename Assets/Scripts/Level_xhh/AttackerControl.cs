using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackerInfo
{
    public enum AttacekrType
    {
        NORMAL
    }
    public AttacekrType type;
    public AttackerBase attacker;
}

public class AttackerControl : MonoBehaviour
{
    [Header("跟随速度")]
    public float FollowSpeed;
    [Header("攻击者信息数组")]
    public AttackerInfo[] AttackerInfos;

    private Rigidbody2D myRigidbody2D;

    private GameObject targetObj;
    /// <summary>
    /// 跟随英雄的定位点
    /// </summary>
    private GameObject heroFollowPoint;
    /// <summary>
    /// 攻击者类型
    /// </summary>
    private AttackerInfo.AttacekrType nowAttackerType;

    private AttackerInfo[] nowAttackerInfos;

    // Start is called before the first frame update
    void Start()
    {
        this.myRigidbody2D = this.GetComponent<Rigidbody2D>();
        this.initAttackers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void initAttackers()
    {
        this.nowAttackerInfos = new AttackerInfo[this.AttackerInfos.Length];

        for (int index = 0; index < this.AttackerInfos.Length; index++)
        {
            AttackerInfo realInfo = new AttackerInfo();
            realInfo.type = this.AttackerInfos[index].type;
            realInfo.attacker = Instantiate<AttackerBase>(this.AttackerInfos[index].attacker, this.transform);
            this.nowAttackerInfos[index] = realInfo;
        }
        // 初始化成普通攻击
        this.setNowAttackerType(AttackerInfo.AttacekrType.NORMAL);
    }

    private void FixedUpdate()
    {
        if (this.targetObj)
        {
            this.followTarget();
        }
    }

    private void followTarget()
    {
        float distance = (this.targetObj.transform.position - this.transform.position).magnitude;
        Vector2 direction = (this.targetObj.transform.position - this.transform.position).normalized;

        this.myRigidbody2D.velocity = Vector2.Lerp(this.myRigidbody2D.velocity, direction * this.FollowSpeed * distance, this.FollowSpeed);
    }


    private void setNowAttackerType(AttackerInfo.AttacekrType type)
    {
        foreach (AttackerInfo item in this.AttackerInfos)
        {

            if (item.type == type)
            {
                // TODO: 在类型变换之前禁用上一个攻击者
                this.nowAttackerType = type;
                this.getNowAttacker(this.nowAttackerType).Active = true;
            }
        }
    }

    private AttackerBase getNowAttacker(AttackerInfo.AttacekrType type)
    {
        AttackerBase attacker = null;
        foreach (AttackerInfo info in this.nowAttackerInfos)
        {
            if (info.type == type)
            {
                attacker = info.attacker;
                break;
            }

        }
        return attacker;
    }

    public void initAttacker(GameObject heroFollowPoint)
    {
        this.heroFollowPoint = heroFollowPoint;
        this.targetObj = heroFollowPoint;
    }
}

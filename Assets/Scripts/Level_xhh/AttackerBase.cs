using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerBase : MonoBehaviour
{
    [Header("跟随英雄的小光点特效预设")]
    public GameObject FollowerPrefab;
    [Header("真正攻击的部分")]
    public AttackerEntity EntityPrefab;
    [Header("攻击周期，ms为单位")]
    public float AttackT;

    /// <summary>
    /// 跟随英雄的小光点
    /// </summary>
    private GameObject myHeroAttackerFollower;
    /// <summary>
    /// 上次攻击经过的时间
    /// <0 代表，此时可以进行攻击操作
    /// </summary>
    private float afterAttackPassTime = -1;

    private GameObject attackerEntityControl;

    // Start is called before the first frame update
    void Start()
    {
        this.attackerEntityControl = new GameObject("AttackerEntityControl");
    }

    // Update is called once per frame
    void Update()
    {
        this.updateAttackT();
    }

    public bool Active
    {
        set
        {
            if (value)
            {
                if (!this.myHeroAttackerFollower)
                {
                    this.attackerActive();
                }
            }
        }
    }

    private void attackerActive()
    {
        this.myHeroAttackerFollower = Instantiate(this.FollowerPrefab, this.gameObject.transform.parent);
        this.myHeroAttackerFollower.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void updateAttackT()
    {
        if (this.afterAttackPassTime >= 0)
        {
            this.afterAttackPassTime += Time.deltaTime;
        }

        if (this.afterAttackPassTime >= this.AttackT)
        {
            this.afterAttackPassTime = -1;
        }
    }

    public void attack(Vector2 targetPosition)
    {
        if (this.afterAttackPassTime < 0)
        {
            this.afterAttackPassTime = 0;
            AttackerEntity entity = Instantiate<AttackerEntity>(this.EntityPrefab, this.attackerEntityControl.transform);
            entity.entityInit(this.myHeroAttackerFollower, targetPosition);
        }
    }
}

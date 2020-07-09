using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerBase : MonoBehaviour
{
    [Header("跟随英雄的小光点特效预设")]
    public GameObject HerosAttackerFollowerPrefab;
    [Header("真正攻击的部分")]
    public GameObject RealAttackerPart;

    /// <summary>
    /// 跟随英雄的小光点
    /// </summary>
    private GameObject myHeroAttackerFollower;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

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
        this.myHeroAttackerFollower = Instantiate(this.HerosAttackerFollowerPrefab, this.gameObject.transform.parent);
    }

}

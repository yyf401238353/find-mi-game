using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class MoveAndLogicBase : MonoBehaviour
{
    [System.Serializable]
    public struct AudioInfo
    {
        public AudioClip clip;
        public float volume;
    }

    [Header("碰撞伤害")]
    public int CrashDamage;
    [Header("受伤音频")]
    public AudioInfo InjuredAudioInfo;
    [Header("攻击音频")]
    public AudioInfo AttackAudioInfo;
    [Header("日常音频播放周期")]
    public float NormalAudioChangeT;
    [Header("攻击部分")]
    public EnemyAttackBase AttackerPart;
    [Header("攻击开始的点")]
    public GameObject AttackStartPoint;
    [Header("死亡特效对象")]
    public GameObject DeadObj;
    [Header("最大血量")]
    public int MaxHp;


    private int nowHp = -1;

    private void enemyDead()
    {
        GameObject dead = Instantiate(this.DeadObj, this.transform.parent);
        dead.transform.position = this.transform.position;
        dead.transform.rotation = this.transform.rotation;
        Destroy(this.gameObject);
        Destroy(dead, 10);
    }

    protected void getInjured(int damage)
    {
        if (nowHp == -1)
        {
            this.nowHp = this.MaxHp;
        }

        this.nowHp -= damage;
        if (this.nowHp < 0)
        {
            this.nowHp = 0;
        }

        if (this.nowHp == 0)
        {
            this.enemyDead();
        }
    }


    public abstract void attackedByHero(Vector3 attackPoint, int damage);
}

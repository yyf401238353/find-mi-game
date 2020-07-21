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
    public abstract void attackedByHero(Vector3 attackPoint);
    [Header("攻击部分")]
    public EnemyAttackBase AttackerPart;
    [Header("攻击开始的点")]
    public GameObject AttackStartPoint;
}

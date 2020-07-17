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
    [Header("日常音频播放周期")]
    public float NormalAudioChangeT;
    public abstract void attackedByHero(Vector3 attackPoint);


}

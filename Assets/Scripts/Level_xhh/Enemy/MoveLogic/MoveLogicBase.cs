using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveLogicBase : MonoBehaviour
{
    [Header("碰撞伤害")]
    public int CrashDamage;
    public abstract void attackedByHero(Vector3 attackPoint);

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class AttackerEntity : MonoBehaviour
{
    [Header("攻击完成特效")]
    public GameObject AttackOver;
    public abstract void entityInit(GameObject followHeroPoint, Vector3 targetPos);
}

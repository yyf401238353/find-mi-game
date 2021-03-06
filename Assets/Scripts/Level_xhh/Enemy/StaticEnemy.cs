using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 静态，且不会死亡的对象
/// </summary>
public class StaticEnemy : MonoBehaviour
{
    [Header("伤害")]
    public int Damage;
    [Header("伤害的理由")]
    public string AttackReason;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            UIControl.UnityIns.SetDeadReason(this.AttackReason);
            collision.GetComponent<Hero>().heroBeAttacked(this.transform.position, this.Damage);
        }
    }
}

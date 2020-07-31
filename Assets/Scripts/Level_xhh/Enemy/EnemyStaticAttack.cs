using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStaticAttack : MonoBehaviour
{
    [Header("伤害")]
    public int Damage;
    [Header("伤害的理由")]
    public string AttackReason;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Road")
        {
            Destroy(this.transform.gameObject);
        }
        else if (collision.gameObject.tag == "Hero")
        {
            UIControl.UnityIns.SetDeadReason(this.AttackReason);
            Hero hero = collision.GetComponent<Hero>();
            hero.heroBeAttacked(this.transform.position, this.Damage);
            Destroy(this.transform.gameObject);
        }
    }
}

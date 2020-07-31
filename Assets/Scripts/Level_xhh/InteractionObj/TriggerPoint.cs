using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class TriggerObj : MonoBehaviour
{
    public abstract void OnTriggerEvent();
}

public class TriggerPoint : MonoBehaviour
{
    [Header("触发对象")]
    public TriggerObj Del;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HeroAttack")
        {
            Del.OnTriggerEvent();
            Destroy(this.gameObject);
        }
    }
}

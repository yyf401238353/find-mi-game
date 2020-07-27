using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHpObj : MonoBehaviour
{
    [Header("添加hp的数量")]
    public int AddHpNum;
    [Header("真实的用来展示的加血对象")]
    public GameObject RealAddHpObj;

    private AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.myAudioSource = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            this.myAudioSource.Play();
            collision.gameObject.GetComponent<Hero>().addHpToHero(this.AddHpNum);
            Destroy(this.RealAddHpObj);
        }
    }
}

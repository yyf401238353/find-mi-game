using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hero))]
public class HeroBorn : MonoBehaviour
{
    public GameObject BornParticle;
    public GameObject BoomActiveParticle;
    public GameObject AnimationControl;

    private bool isBorn = false;

    private HeroAnimationControl nowAniamtionControl;
    private GameObject nowBornParticle;
    private Hero mainController;

    // Start is called before the first frame update
    void Start()
    {
        this.nowBornParticle = Instantiate(this.BornParticle, this.transform);
        this.mainController = this.GetComponent<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 在没有出生的时候，碰到障碍物，则视为出生
        if (!this.isBorn)
        {
            this.isBorn = true;
            this.nowAniamtionControl = Instantiate(this.AnimationControl, this.transform).GetComponent<HeroAnimationControl>();
            this.nowAniamtionControl.name = "AnimationControl";
            GameObject boomObject = Instantiate(this.BoomActiveParticle, this.transform);
            Destroy(boomObject, 3);
            Destroy(this.nowBornParticle);

            // 通知主控制脚本，已经成功出生
            this.mainController.heroBorn();
        }

    }
}

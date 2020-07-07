using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HeroAnimationControl : MonoBehaviour
{
    private Animator myAnimator;


    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setHeroStatusParam(Hero.Status value)
    {
        this.myAnimator.SetInteger("HeroStatus", (int)value);
    }
}

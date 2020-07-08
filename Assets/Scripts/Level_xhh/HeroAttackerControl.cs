using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackerControl : MonoBehaviour
{
    public AttackerControl Attacker;

    public GameObject HeroAttackerFollowPart;


    private AttackerControl nowAttacker;

    // Start is called before the first frame update
    void Start()
    {
        this.nowAttacker = Instantiate<AttackerControl>(this.Attacker, this.HeroAttackerFollowPart.transform.position, this.Attacker.transform.rotation);
        this.nowAttacker.initAttacker(this.HeroAttackerFollowPart);
    }

    // Update is called once per frame
    void Update()
    {

    }

}

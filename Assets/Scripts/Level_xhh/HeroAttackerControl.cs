using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackerControl : MonoBehaviour
{
    public AttackerControl AttackerControl;

    public GameObject HeroAttackerFollowPart;


    private AttackerControl nowAttacker;

    private bool isAttackActive = false;

    // Start is called before the first frame update
    void Start()
    {
        this.nowAttacker = Instantiate<AttackerControl>(this.AttackerControl, this.HeroAttackerFollowPart.transform.position, this.AttackerControl.transform.rotation);
        this.nowAttacker.initAttacker(this.HeroAttackerFollowPart);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isAttackActive)
        {
            this.disposeLeftMouseClick();
        }
    }

    private void disposeLeftMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 camera = Camera.main.WorldToScreenPoint(transform.position);// 相机是世界的，世界到屏幕
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.z);
            Vector3 realWorldPos = Camera.main.ScreenToWorldPoint(screenPos);
            this.nowAttacker.attack(realWorldPos);
        }
    }

    public bool AttackActive
    {
        set
        {
            this.isAttackActive = value;
        }
    }

}

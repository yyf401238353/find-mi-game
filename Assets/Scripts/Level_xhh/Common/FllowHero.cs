using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FllowHero : MonoBehaviour
{

    [Header("跟随对象名")]
    public string TargetObjectName;
    [Header("最小跟随速度")]
    public float MinFollowSpeed = 0.1f;
    [Header("跟随系数")]
    public float FollowCoefficient = 0.1f;

    private float DefaultZ;
    private Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        this.Target = GameObject.Find(this.TargetObjectName).transform;
        this.DefaultZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        Vector3 myPosition = this.transform.position;
        Vector3 targetPosisiton = this.Target.position;

        float diff = Vector3.Distance(myPosition, targetPosisiton);
        float speed = diff * this.FollowCoefficient + this.MinFollowSpeed;

        this.transform.position = Vector3.Lerp(myPosition, targetPosisiton, speed) + new Vector3(0, 0, this.DefaultZ);
    }
}

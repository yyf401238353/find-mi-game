using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class AttackerEntity : MonoBehaviour
{
    public abstract void entityInit(Vector3 originalPos, Vector3 targetPos);
}

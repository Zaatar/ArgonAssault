using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] float hitDamage = 25.0f;

    public float GetHitDamage() { return hitDamage; }
}

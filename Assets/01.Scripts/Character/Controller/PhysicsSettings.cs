using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Movement")]
public class PhysicsSettings : ScriptableObject
{
    [Header(" ------Movement ------")]
    [Min(0)] public float Friction;


    [Header(" ------Acceleration and Deceleration ------")]
    [Min(0)] public float Accelleration;
    [Min(0)] public float Decelleration;
}

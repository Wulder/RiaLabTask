using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/MovementConfig")]
public class MovementConfig : ScriptableObject
{
    [field: SerializeField] public float Speed;
    [field: SerializeField] public float JumpForce;
    [field: SerializeField] public float RotationSensitivity;
    [field: SerializeField] public float Gravity;
}

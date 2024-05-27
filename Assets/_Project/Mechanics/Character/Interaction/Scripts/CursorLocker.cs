using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLocker : MonoBehaviour
{
    [SerializeField] private PlayerMovement _pMovement;

    private void Awake()
    {
        SwitchState(true);
    }
    private void OnEnable()
    {
        _pMovement.OnCanMoveChange += SwitchState;
    }

    private void OnDisable()
    {
        _pMovement.OnCanMoveChange -= SwitchState;
    }
    
    private void SwitchState(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.None;

        Cursor.visible = !state;
    }
}

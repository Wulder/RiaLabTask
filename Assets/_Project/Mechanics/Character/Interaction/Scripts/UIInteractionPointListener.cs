using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractionPointListener : MonoBehaviour
{
    [SerializeField] private Interactor _interactor;
    [SerializeField] private PlayerMovement _pMovement;
    [SerializeField] private Animator _animator;
    [SerializeField] private CanvasGroup _pointGroup;
    
    private void OnEnable()
    {
        _interactor.OnEnter += Show;
        _interactor.OnExit += Hide;

        _pMovement.OnCanMoveChange += SwitchState;
    }

    private void OnDisable()
    {
        _interactor.OnEnter -= Show;
        _interactor.OnExit -= Hide;

        _pMovement.OnCanMoveChange -= SwitchState;
    }
    void Show()
    {
        _animator.SetBool("Active", true);
    }

    void Hide()
    {
        _animator.SetBool("Active", false);
    }

    void SwitchState(bool b)
    {
        _pointGroup.alpha = b ? 1 : 0;
    }


  
}

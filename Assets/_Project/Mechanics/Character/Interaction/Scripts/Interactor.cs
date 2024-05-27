using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Action OnEnter, OnExit;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxIteractDistance = 1;
    [SerializeField] private LayerMask _mask;
    

    private bool _onSeeInteractable = false;
    private bool _canInteract = true;
    private RaycastHit _cast;

    //тут опасные подписки на события, player инициализируется у нас первым в ExecutionOrder, в рабочем проекте исопльзуем DI
    private void OnEnable()
    {
        Player.Instance.OnStateChange += SwitchState;
    }

    private void OnDisable()
    {
        Player.Instance.OnStateChange -= SwitchState;
    }

    void SwitchState(Player.PlayerState state)
    {
        if(state == Player.PlayerState.Walk)
        {
            _canInteract = true;
            return;
        }
        if (state == Player.PlayerState.Puzzle)
        {
            _canInteract = false;
            return;
        }
    }
    void Update()
    {
        if(!_canInteract)
        {
            _onSeeInteractable = false;
            OnInteractableExit();
            return;
        }

        bool prevState = _onSeeInteractable;
        _onSeeInteractable =  Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _cast,_maxIteractDistance,_mask);

        if (!prevState && _onSeeInteractable)
        {
            OnInteractableEnter();
        }

        if (prevState && !_onSeeInteractable)
        {
            OnInteractableExit();
        }

        if(_onSeeInteractable && Input.GetMouseButtonDown(0))
        {
            
            var interactor = _cast.collider.gameObject.GetComponent<IInteractableObject>();
            
            if(interactor != null)
            {
                interactor.Interact();
            }
        }

    }

    void OnInteractableEnter()
    {
        OnEnter?.Invoke();
    }

    void OnInteractableExit()
    {
        OnExit?.Invoke();
    }
}

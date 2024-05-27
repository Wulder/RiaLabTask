using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismPart : MonoBehaviour
{
    [SerializeField] private Puzzle _puzzle;
    [SerializeField] private Animator _animator;


    private void OnEnable()
    {
        if (_puzzle == null) return;
        _puzzle.OnFinish += SetActive;
        _puzzle.OnReset += Deactivate;
    }

    private void OnDisable()
    {
        if (_puzzle == null) return;
        _puzzle.OnFinish -= SetActive;
        _puzzle.OnReset -= Deactivate;
    }

    private void SetActive()
    {
        _animator.SetBool("Activated", true);
    }

    private void Deactivate()
    {
        _animator.SetBool("Activated", false);
    }
}

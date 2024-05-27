using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour, IInteractableObject
{

    public Action OnFinish, OnReset;
    public bool IsComple => _isComplete;

    protected bool _isComplete;

    [SerializeField] protected PuzzleCamera _puzzleCamera;
    public void Interact()
    {
        
        Player.Instance.SetState(Player.PlayerState.Puzzle);
        Player.Instance.CameraController.SetNewCamera(_puzzleCamera);
        Player.Instance.PlayerMovement.SetMoveable(false);
    }

  
    protected abstract void ResetPuzzle();

    protected virtual void OnFinishCallback()
    {

    }

    [Button]
    protected void Finish()
    {
       _isComplete = true;
        OnFinishCallback();
        OnFinish?.Invoke();
        ExitPuzzle();
    }

    [Button]
    protected void ExitPuzzle()
    {
        Player.Instance.SetState(Player.PlayerState.Walk);
        Player.Instance.CameraController.ResetToMainCamera();
        Player.Instance.PlayerMovement.SetMoveable(true);
    }
    
}

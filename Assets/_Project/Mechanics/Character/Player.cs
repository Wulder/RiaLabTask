using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action<PlayerState> OnStateChange;

    //простейший синглтон, в рабочем проекте используем DI
    private static Player instance;
    public static Player Instance => instance;

    public PlayerMovement PlayerMovement => _playerMovement;
    public CameraController CameraController => _cameraController;
    public Interactor Interactor => _interactor;
    public PlayerState State => _state;

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Interactor _interactor;

    private PlayerState _state;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void SetState(PlayerState newState)
    {
        _state = newState;
        OnStateChange?.Invoke(newState);
    }

    public enum PlayerState
    {
        Walk,
        Puzzle
    }
}

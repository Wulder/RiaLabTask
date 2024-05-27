using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Action<bool> OnCanMoveChange;

    [SerializeField] private float speed;
    [SerializeField] private SphereCastGroundChecker _groundChecker;
    [SerializeField] private MovementConfig _config;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _body;

    private MovementInput _movementInput = new MovementInput();
    private float _gravityAcceleration;

    private bool _canMove = true;

    private float _xRotation = 0;

    void Update()
    {
        if(_canMove)
        {
            MovementInput();
            HandleGroundState();
            Move();
            Rotate();
            Jump();
        }
       
        ApplyAcceleration();
    }
    private void MovementInput()
    {
        _movementInput.Direction.x = Input.GetAxisRaw("Horizontal");
        _movementInput.Direction.y = Input.GetAxisRaw("Vertical");
        _movementInput.Jump = Input.GetKey(KeyCode.Space);

        _movementInput.Rotation.x = Input.GetAxis("Mouse X");
        _movementInput.Rotation.y = Input.GetAxis("Mouse Y");
    }

    private void Move()
    {
        Vector3 moveDirection = transform.TransformDirection(_movementInput.NormalizedDirection);

        _characterController.Move(moveDirection * Time.deltaTime * _config.Speed);
    }

    private void ApplyAcceleration()
    {
        //применение ускорения свободного паденния
        _characterController.Move(new Vector3(0, -_gravityAcceleration, 0) * Time.deltaTime);
    }

    private void HandleGroundState()
    {
        switch (_groundChecker.IsGround())
        {
            case SphereCastGroundChecker.OnGroundState.Stay:
                _gravityAcceleration = 0;
                break;
            case SphereCastGroundChecker.OnGroundState.Sliding:
                _gravityAcceleration += _config.Gravity * Time.deltaTime;
                _characterController.Move(_groundChecker.Surface * Time.deltaTime * _gravityAcceleration);
                break;
            case SphereCastGroundChecker.OnGroundState.NonGround:
                _gravityAcceleration += _config.Gravity * Time.deltaTime;
                break;
        }
    }

    private void Rotate()
    {
        transform.rotation *= Quaternion.Euler(0, _movementInput.Rotation.x * _config.RotationSensitivity, 0);

        _xRotation += -_movementInput.Rotation.y * _config.RotationSensitivity;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);
        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
    }

    private void Jump()
    {
        if(_movementInput.Jump && _groundChecker.IsGround() == SphereCastGroundChecker.OnGroundState.Stay)
        {   
            _gravityAcceleration += -_config.JumpForce;
        }
    }

    public void SetMoveable(bool canMove)
    {
        if (canMove == _canMove) return;
        _canMove = canMove;

        if(canMove)
        {
            GetComponent<Collider>().enabled = true;
            _body.gameObject.SetActive(true);
            
        }
        else
        {
            GetComponent<Collider>().enabled = false;
            _body.gameObject.SetActive(false);
           
        }

        OnCanMoveChange?.Invoke(canMove);
    }

   
}

public class MovementInput
{
    public Vector2 Direction;
    public bool Jump;
    public Vector2 Rotation;

    public Vector3 NormalizedDirection => new Vector3(Direction.normalized.x, 0, Direction.normalized.y);
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class SphereCastGroundChecker : MonoBehaviour
{

    public Vector3 Surface => _surface;
    public float NormalAngle => _normalAngle;

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _height, _radius, _slidingThreshold = 45;

    private float _normalAngle = 0;
    private RaycastHit _castHit;
    private bool _isGround = false;
    private Vector3 _surface;

    public OnGroundState IsGround()
    {
        OnGroundState state = OnGroundState.Stay;

        if (_isGround)
        {
            if (_normalAngle <= _slidingThreshold)
                state = OnGroundState.Stay;
            else
                state = OnGroundState.Sliding;
        }
        else
        {
            state = OnGroundState.NonGround;
        }

        return state;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_playerMovement.transform.position, Vector3.down * _height);
        Gizmos.DrawWireSphere(_playerMovement.transform.position + Vector3.down * _height, _radius);

        if (_normalAngle < _slidingThreshold)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawSphere(_castHit.point, .1f);

        Gizmos.DrawRay(_castHit.point, _castHit.normal);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_castHit.point, _surface);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(_castHit.point, -_surface);
    }

    public void OnGUI()
    {
       //   GUI.Label(new Rect(50, 50, 50, 50), $"angle: {_normalAngle}");
    }
    public void Update()
    {
        _isGround = Physics.SphereCast(_playerMovement.transform.position, _radius, Vector3.down, out _castHit, _height);
        _normalAngle = Vector3.Angle(_castHit.normal, Vector3.up);
        _surface = Quaternion.AngleAxis(-90, _castHit.normal) * Vector3.Cross(_playerMovement.transform.up, _castHit.normal);
    }

    public enum OnGroundState
    {
        Stay,
        Sliding,
        NonGround
    }
}

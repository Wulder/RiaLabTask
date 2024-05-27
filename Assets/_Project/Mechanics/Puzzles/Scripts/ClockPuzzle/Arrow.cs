using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Action OnUpdate;
    public int CompletedSteps => _completedSteps;
    public int StepsCount => _stepsCount;
    [SerializeField] private int _stepsCount;
    
    private float _angle = 0;
    private int _completedSteps = 0;

    private void Awake()
    {
        _angle = transform.localRotation.x * 360f;
        _completedSteps = Mathf.RoundToInt(_angle / (360f / _stepsCount));
    }

    private void Update()
    {
        if (_angle < 0) _angle += 360f;
        _completedSteps = Mathf.RoundToInt(_angle / (360f / _stepsCount));
        _angle = _completedSteps * (360f / _stepsCount);



        transform.localRotation = Quaternion.Euler(_angle, 0, 0);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("start drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //rotate by x axis

        Vector2 onScreen = (Vector2)(Camera.main.WorldToScreenPoint(transform.position));
        var _localMousePosition = eventData.position - onScreen;
        _angle = -Vector2.SignedAngle(Vector2.up, _localMousePosition);

        OnUpdate?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("end drag");
    }

   public void SetAngle(int steps)
    {
        _angle = steps * (360f / _stepsCount);
        transform.localRotation = Quaternion.Euler(_angle, 0, 0);
    }




}

using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ClockPuzzle : Puzzle
{
    [SerializeField] private TextMeshProUGUI _digitalClock;
    [SerializeField] private Arrow _hourArrow, _minutesArrow;

    private int _targetHours, _targetMinutes;

    private void Awake()
    {
        ResetPuzzle();

    }

    private void OnEnable()
    {
        _hourArrow.OnUpdate += Check;
        _minutesArrow.OnUpdate += Check;
    }

    private void OnDisable()
    {
        _hourArrow.OnUpdate -= Check;
        _minutesArrow.OnUpdate -= Check;
    }

    [Button]
    protected override void ResetPuzzle()
    {
        _isComplete = false;
        _targetHours = UnityEngine.Random.RandomRange(1, 12);
        _targetMinutes = UnityEngine.Random.Range(1, 60);

        _hourArrow.SetAngle(UnityEngine.Random.RandomRange(1, 12));
        _minutesArrow.SetAngle(UnityEngine.Random.Range(1, 60));

        _hourArrow.enabled = true;
        _minutesArrow.enabled = true;


        
        
        _digitalClock.text = $"{_targetHours}:{_targetMinutes.ToString("D2")}";

        OnReset?.Invoke();
    }

    protected override void OnFinishCallback()
    {
        print("Finish clock Puzzle!");

        _hourArrow.SetAngle(_targetHours);
        _minutesArrow.SetAngle(_targetMinutes + (60*_targetHours));

        _hourArrow.enabled = false;
        _minutesArrow.enabled = false;
    }

    void Check()
    {
        if (_isComplete) return;

        int currentHours = _hourArrow.CompletedSteps;
        if (currentHours == 0) currentHours = 12;

        int currentMinutes = _minutesArrow.CompletedSteps;

        print($"{currentHours} | {currentMinutes}");

        if(currentHours == _targetHours && currentMinutes == (_targetMinutes +(60* currentHours)))
        {
            
            Finish();
        }
    }
    
}

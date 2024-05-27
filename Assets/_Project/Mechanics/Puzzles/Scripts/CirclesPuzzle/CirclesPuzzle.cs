using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclesPuzzle : Puzzle
{

    [SerializeField] private List<Arrow> _circles = new List<Arrow>();

    private void Awake()
    {
        ResetPuzzle();
    }

    private void OnEnable()
    {
        foreach(Arrow circle in _circles)
        {
            circle.OnUpdate += Check;
        }
    }

    private void OnDisable()
    {
        foreach (Arrow circle in _circles)
        {
            circle.OnUpdate -= Check;
        }
    }

    [Button]
    protected override void ResetPuzzle()
    {

        foreach (Arrow circle in _circles)
        {
            circle.SetAngle(UnityEngine.Random.RandomRange(0, 12));
        }
        _isComplete = false;
        OnReset?.Invoke();
    }

    protected override void OnFinishCallback()
    {
        Debug.Log("finish cicrcle puzzle!");
        foreach (Arrow circle in _circles)
        {
            circle.SetAngle(0);
        }
    }
    
    private void Check()
    {
        if (_isComplete) return;

        bool result = true;

        foreach (Arrow circle in _circles)
        {
            int val = circle.CompletedSteps == 12 ? 0 : circle.CompletedSteps;
            if (val != 0)
            {
                result = false;
            }

        }

        if (result)
        {
            Finish();
        }
    }
}

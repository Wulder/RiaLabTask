using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PasswordPuzzle : Puzzle
{

    public int PasswrodLength => _passwordLength;

    [SerializeField] private int _passwordLength = 4;
    [SerializeField] private TextMeshProUGUI _passwordText;
    [SerializeField] private PasswordPanel _passwordPanel;

    private string _targetPassword = string.Empty;

    private void Awake()
    {
        ResetPuzzle();
    }
    [Button]
    protected override void ResetPuzzle()
    {
        _isComplete = false;
        _targetPassword = string.Empty;
        for (int i = 0; i < _passwordLength; i++)
            _targetPassword += UnityEngine.Random.RandomRange(0, 9).ToString();

        _passwordText.text = _targetPassword;

        OnReset?.Invoke();
    }

    public void Check(string password)
    {
        if (password == _targetPassword)
            Finish();
    }

    protected override void OnFinishCallback()
    {
        Debug.Log("Finish password puzzle!");
        _passwordPanel.SetInput(_targetPassword);
    }
}

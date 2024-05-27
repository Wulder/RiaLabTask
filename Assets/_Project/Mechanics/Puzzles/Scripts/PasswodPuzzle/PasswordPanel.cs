using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PasswordPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _display;
    [SerializeField] private PasswordPuzzle _puzzle;

    public string CurrentInput => _currentInput;

    private string _currentInput = "";

    private void OnEnable()
    {
        _puzzle.OnReset += ResetInput;
    }
    private void OnDisable()
    {
        _puzzle.OnReset -= ResetInput;
    }

    public void AddNumber(int number)
    {
        if(_currentInput.Length >= _puzzle.PasswrodLength) return;
        _currentInput += number.ToString();
        UpdateDisplay();
    }
    public void ResetInput()
    {
        _currentInput = "";
        UpdateDisplay();
    }

    public void SetInput(string input)
    {
        _currentInput = input;
        UpdateDisplay();
    }

    public void Ok()
    {
        _puzzle.Check(_currentInput);
    }

    private void UpdateDisplay()
    {
        _display.text = _currentInput.ToString();
        
    }

}

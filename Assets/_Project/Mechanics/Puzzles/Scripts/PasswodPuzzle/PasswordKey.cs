using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PasswordKey : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private KeyType _keyType;
    [SerializeField] private int _value;
    [SerializeField] private PasswordPanel _passPanel;
    public void OnPointerClick(PointerEventData eventData)
    {
       if(_keyType == KeyType.Number)
        {
            _passPanel.AddNumber(_value);
        }

       if(_keyType == KeyType.Ok)
        {
            _passPanel.Ok();
        }

       if(_keyType == KeyType.Reset)
        {
            _passPanel.ResetInput();
        }
    }



    [Serializable]
    enum KeyType
    {
        Number,
        Ok,
        Reset
    }
}

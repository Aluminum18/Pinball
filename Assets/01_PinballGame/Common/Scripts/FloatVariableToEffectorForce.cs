using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatVariableToEffectorForce : MonoBehaviour
{
    [SerializeField]
    private FloatVariable _refFloat;
    [SerializeField]
    private AreaEffector2D _targetEffector;
    [SerializeField]
    private float _exchangeFactor;
    [SerializeField][Tooltip("Only exchange when Exchange is called")]
    private bool _activeUpdate = false;

    public void Exchange()
    {
        UpdateChange(_refFloat.Value);
    }

    private void OnEnable()
    {
        if (_activeUpdate)
        {
            return;
        }

        _refFloat.OnValueChange += UpdateChange;
        UpdateChange(_refFloat.Value);
    }

    private void OnDisable()
    {
        if (_activeUpdate)
        {
            return;
        }

        _refFloat.OnValueChange -= UpdateChange;
    }

    private void UpdateChange(float newValue)
    {
        _targetEffector.forceMagnitude = _refFloat.Value * _exchangeFactor;
    }
}

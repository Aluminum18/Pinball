using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TweenFloatVariable : MonoBehaviour
{
    [SerializeField]
    private bool _tweenOnEnable = false;
    [SerializeField]
    private FloatVariable _targetFloat;
    [SerializeField]
    private float _from;
    [SerializeField]
    private float _to;
    [SerializeField]
    private LeanTweenType _tweenType;
    [SerializeField]
    private float _duration;

    [SerializeField]
    private UnityEvent _onStartTween;
    [SerializeField]
    private UnityEvent _onFinishTween;

    private LTDescr _ltd;

    public void DoTween()
    {
        _onStartTween.Invoke();

        _ltd = LeanTween.value(_from, _to, _duration).setEase(_tweenType).setOnUpdate(value =>
        {
            _targetFloat.Value = value;
        }).setOnComplete(() =>
        {
            _onFinishTween.Invoke();
            _ltd = null;
        });
    }

    public void StopTween()
    {
        if (_ltd == null)
        {
            return;
        }

        LeanTween.cancel(_ltd.id);
    }

    private void OnEnable()
    {
        if (_tweenOnEnable)
        {
            DoTween();
        }
    }
}

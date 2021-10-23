using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceAdjust : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite;
    [SerializeField]
    private LeanTweenType _tweenType;
    [SerializeField]
    private float _appearDuration = 0.5f;
    [SerializeField]
    private float _disappearDuration = 0.5f;

    public void Appear()
    {
        float current = _sprite.material.GetFloat("_AppearPercent");
        LeanTween.value(current, 1f, _appearDuration).setEase(_tweenType).setOnUpdate(value =>
        {
            _sprite.material.SetFloat("_AppearPercent", value);
        });
    }

    public void Disappear()
    {
        float current = _sprite.material.GetFloat("_AppearPercent");
        LeanTween.value(current, 0f, _appearDuration).setEase(_tweenType).setOnUpdate(value =>
        {
            _sprite.material.SetFloat("_AppearPercent", value);
        });
    }


}

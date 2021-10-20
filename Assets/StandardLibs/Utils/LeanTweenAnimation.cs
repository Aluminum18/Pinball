using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LeanTweenAnimation : MonoBehaviour
{
    [SerializeField]
    private bool _doTweenOnEnable = false;
    [SerializeField]
    private GameObject _go;
    [SerializeField]
    private AnimType _animType;
    [SerializeField]
    private LeanTweenType _tweenType;
    [SerializeField]
    private float _duration;
    public float Duration
    {
        set
        {
            _duration = value;
        }
    }
    [SerializeField]
    private bool _useCurrentAsFrom = false;
    [SerializeField]
    private Vector3 _from;
    [SerializeField]
    private Vector3 _to;

    [SerializeField]
    private UnityEvent _onStartTween;
    [SerializeField]
    private UnityEvent _onFinishTween;

    private LTDescr _ltDes;

    public enum AnimType
    {
        None = 0,
        Move,
        Zoom,
        FadeSprite,
        FadeUI,
        MoveLocal,
        RotateLocal,
        MoveByLocal
    }

    public void StopTween()
    {
        if (_ltDes == null)
        {
            return;
        }

        LeanTween.cancel(_ltDes.id);
    }

    public void DoTweenWithDelay(float delay)
    {
        Observable.Timer(System.TimeSpan.FromSeconds(delay)).Subscribe(_ =>
        {
            DoTween();
        });
    }

    public void DoTween()
    {
        if (_go == null)
        {
            _go = gameObject;
        }

        _onStartTween.Invoke();

        Observable.Timer(System.TimeSpan.FromSeconds(_duration)).Subscribe(_ =>
        {
            _onFinishTween.Invoke();
        });

        switch (_animType)
        {
            case AnimType.Move:
                {
                    if (!_useCurrentAsFrom)
                    {
                        _go.transform.position = _from;
                    }

                    _ltDes = LeanTween.move(_go, _to, _duration).setEase(_tweenType);
                    break;
                }
            case AnimType.MoveLocal:
                {
                    if (!_useCurrentAsFrom)
                    {
                        _go.transform.localPosition = _from;
                    }
                    _ltDes = LeanTween.moveLocal(_go, _to, _duration).setEase(_tweenType);
                    break;
                }
            case AnimType.Zoom:
                {
                    if (!_useCurrentAsFrom)
                    {
                        _go.transform.localScale = _from;
                    }
                    _ltDes = LeanTween.scale(_go, _to, _duration).setEase(_tweenType);
                    break;
                }
            case AnimType.FadeSprite:
                {
                    var spriteRenderer = _go.GetComponent<SpriteRenderer>();

                    if (spriteRenderer == null)
                    {
                        return;
                    }
                    if (!_useCurrentAsFrom)
                    {
                        Color bufferColor = spriteRenderer.color;
                        bufferColor.a = _from.x;
                        spriteRenderer.color = bufferColor;
                    }

                    _ltDes = LeanTween.alpha(_go, _to.x, _duration).setEase(_tweenType);
                    break;
                }
            case AnimType.FadeUI:
                {
                    var canvasGroup = _go.GetComponent<CanvasGroup>();

                    if (canvasGroup == null)
                    {
                        Debug.LogWarning("To use FadeUI, add CanvasGroup component to the target");
                        return;
                    }

                    if (!_useCurrentAsFrom)
                    {
                        canvasGroup.alpha = _from.x;
                    }

                    _ltDes = LeanTween.alphaCanvas(canvasGroup, _to.x, _duration).setEase(_tweenType);
                    break;
                }
            case AnimType.RotateLocal:
                {
                    if (!_useCurrentAsFrom)
                    {
                        _go.transform.localRotation = Quaternion.Euler(_from);
                    }

                    _ltDes = LeanTween.rotateLocal(_go, _to, _duration).setEase(_tweenType);
                    break;
                }
            case AnimType.MoveByLocal:
                {
                    if (!_useCurrentAsFrom)
                    {
                        _go.transform.localPosition = _from;
                    }

                    _ltDes = LeanTween.moveLocal(_go, _go.transform.localPosition + _to, _duration).setEase(_tweenType);
                    break;
                }
            default:
                break;              
        }

        _ltDes.setOnComplete(() =>
        {
            _ltDes = null;
        });
    }

    private void OnEnable()
    {
        if (_doTweenOnEnable)
        {
            DoTween();
        }
    }
}

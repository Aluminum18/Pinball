using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AngularMomentumBoost : MonoBehaviour
{
    [SerializeField]
    private Transform _root;
    [SerializeField]
    private Transform _tip;
    [SerializeField]
    private Transform _boostDirectionAdjustor;
    [SerializeField]
    private float _rootSpeedBoost;
    [SerializeField]
    private float _tipSpeedBoost;
    [SerializeField]
    private LayerMask _boostLayer;

    [Header("Inspec")]
    [SerializeField]
    private float _tipRootDistance;
    [SerializeField]
    private Vector2 _boostDirection;

    private int _maxRetry = 3;
    [SerializeField]
    private int _retry = 0;

    [SerializeField]
    private Rigidbody2D _recentlyContact;

    private void OnEnable()
    {
        _tipRootDistance = Vector2.Distance(_root.position, _tip.position);
        _boostDirection = _boostDirectionAdjustor.position - (_tip.position + _root.position) / 2f;
        _boostDirection = _boostDirection.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((1 << other.gameObject.layer & _boostLayer) == 0)
        {
            return;
        }

        _recentlyContact = other.attachedRigidbody;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _recentlyContact = null;
    }

    public void Boost()
    {
        if (_recentlyContact == null)
        {
            if (_retry > _maxRetry)
            {
                _retry = 0;
                return;
            }

            RetryBoost();
            return;
        }

        float distanceToRoot = Vector2.Distance(_recentlyContact.position, _root.position);
        float boostSpeed = Mathf.Lerp(_rootSpeedBoost, _tipSpeedBoost, distanceToRoot / _tipRootDistance);

        if (_recentlyContact.velocity.magnitude > boostSpeed)
        {
            return;
        }

        _recentlyContact.velocity += _boostDirection * boostSpeed;

        _retry = 0;
    }

    private void RetryBoost()
    {
        _retry++;
        Observable.TimerFrame(1).Subscribe(_ =>
        {
            Boost();
        });
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_tip.position, _root.position);
        _tipRootDistance = Vector2.Distance(_root.position, _tip.position);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(_boostDirectionAdjustor.position, (_tip.position + _root.position) / 2f);
    }

}

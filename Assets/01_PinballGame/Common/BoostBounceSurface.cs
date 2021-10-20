using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBounceSurface : MonoBehaviour
{
    [SerializeField]
    private float _speedBoostFactor;
    [SerializeField]
    private bool _constantSpeedBoost = false;
    [SerializeField][Tooltip("m/s")]
    private float _boostToSpeed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var incomingRb = collision.collider.attachedRigidbody;
        if (incomingRb == null)
        {
            return;
        }

        if (_constantSpeedBoost)
        {
            ConstantBoost(incomingRb);
            return;
        }

        FactorBoost(incomingRb);
    }

    private void FactorBoost(Rigidbody2D target)
    {
        target.velocity *= _speedBoostFactor;
    }

    private void ConstantBoost(Rigidbody2D target)
    {
        if (target.velocity.magnitude >= _boostToSpeed)
        {
            return;
        }

        Vector2 boostSpeedDir = target.velocity.normalized;
        target.velocity = boostSpeedDir * _boostToSpeed;
    }
}

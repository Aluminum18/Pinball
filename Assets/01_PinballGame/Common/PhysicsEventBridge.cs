using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsEventBridge : MonoBehaviour
{
    [SerializeField]
    private LayerMask _contactMask;
    [SerializeField]
    private UnityEvent _onTriggerEnter2D;
    [SerializeField]
    private UnityEvent _onTriggerExit2D;
    [SerializeField]
    private UnityEvent _onCollisionEnter2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _contactMask) == 0)
        {
            return;
        }

        _onTriggerEnter2D.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _contactMask) == 0)
        {
            return;
        }

        _onTriggerExit2D.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((1 << collision.gameObject.layer & _contactMask) == 0)
        {
            return;
        }

        _onCollisionEnter2D.Invoke();
    }
}

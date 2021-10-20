using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInSinglePool : MonoBehaviour
{
    private SingleObjectPool _parrentPool;

    public void SetParrent(SingleObjectPool parrent)
    {
        _parrentPool = parrent;
    }

    private void OnDisable()
    {
        _parrentPool.ReturnToPool(gameObject);
    }
}

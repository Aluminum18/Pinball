using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SingleObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectInPool;
    [SerializeField]
    private Transform _parrentTransform;
    [SerializeField]
    private Transform _spawnPos;
    [SerializeField]
    private Vector3 _randomRangePos;
    [SerializeField]
    private int _poolSize;

    private Stack<GameObject> _available = new Stack<GameObject>();

    public void Spawn()
    {
        var go = SpawnAndReturnObject();
        if (_parrentTransform != null)
        {
            go.transform.parent = _parrentTransform;
        }
        
        if (_spawnPos == null)
        {
            go.transform.position = _objectInPool.transform.position;
            return;
        }

        go.transform.position = _spawnPos.position;
    }

    public GameObject SpawnAndReturnObject()
    {
        GameObject go;

        if (_available.Count == 0)
        {
            go = Instantiate(_objectInPool);
            go.AddComponent<ObjectInSinglePool>().SetParrent(this);
            return go;
        }

        go = _available.Pop();
        if (go.activeSelf)
        {
            go = SpawnAndReturnObject();
        }

        go.SetActive(true);
        return go;
    }

    public void ReturnToPool(GameObject go)
    {
        if (_available.Count >= _poolSize)
        {
            Destroy(go);
            return;
        }

        go.SetActive(false);
        _available.Push(go);
    }

    Vector3 _bufferVector = Vector3.zero;
    public void SpawnWithRandomRange()
    {
        _bufferVector.x = Random.Range(-_randomRangePos.x, _randomRangePos.x);
        _bufferVector.y = Random.Range(-_randomRangePos.y, _randomRangePos.y);
        _bufferVector.z = Random.Range(-_randomRangePos.z, _randomRangePos.z);

        var go = SpawnAndReturnObject();
        if (_parrentTransform != null)
        {
            go.transform.parent = _parrentTransform;
        }

        if (_spawnPos == null)
        {
            go.transform.position += _bufferVector;
            return;
        }

        _bufferVector += _spawnPos.position;

        go.transform.position = _bufferVector;

    }

    public void FrameDelaySpawnWithRandonRange(int delayFrame)
    {
        Observable.TimerFrame(1).Subscribe(_ =>
        {
            SpawnWithRandomRange();
        });
    }

    private void OnDrawGizmosSelected()
    {
        if (_spawnPos == null)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_spawnPos.position, _randomRangePos * 2f);
    }

}

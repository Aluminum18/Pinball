using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsSettings : MonoBehaviour
{
    [SerializeField]
    private int _targetFrameRate = 60;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = _targetFrameRate;
    }
}

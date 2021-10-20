using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCodeToEvent : MonoBehaviour
{
    [SerializeField]
    private List<KeyEventsPair> _registerKeys;

    private void Update()
    {
        for (int i = 0; i < _registerKeys.Count; i++)
        {
            if (Input.GetKeyDown(_registerKeys[i].keyCode))
            {
                _registerKeys[i].RaiseKeyDownEvents();
            }

            if (Input.GetKeyUp(_registerKeys[i].keyCode))
            {
                _registerKeys[i].RaiseKeyUpEvents();
            }
        }
    }
}

[System.Serializable]
public class KeyEventsPair
{
    public KeyCode keyCode;
    public List<GameEvent> keydownEvents;
    public List<GameEvent> keyupEvents;

    public void RaiseKeyDownEvents()
    {
        for (int i = 0; i < keydownEvents.Count; i++)
        {
            keydownEvents[i].Raise();
        }
    }

    public void RaiseKeyUpEvents()
    {
        for (int i = 0; i < keyupEvents.Count; i++)
        {
            keyupEvents[i].Raise();
        }
    }
}

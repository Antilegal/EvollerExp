using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    protected float _value = 0f;

    public float Value
    {
        get
        {
            return _value;
        }

        set
        {
            _value = value;
            OnSetValue();
        }
    }

    protected virtual void OnSetValue()
    {

    }
}

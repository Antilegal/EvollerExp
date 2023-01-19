using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIValue : MonoBehaviour
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
            SetValue();
        }
    }

    protected virtual void SetValue()
    {

    }
}

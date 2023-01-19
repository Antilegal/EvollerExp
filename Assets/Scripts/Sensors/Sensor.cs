using UnityEngine;

public class Sensor : MonoBehaviour
{
    protected float _value = 0f;

    public float Value
    {
        get
        {
            OnGetValue();
            return _value;
        }

        set
        {
            _value = value;
        }
    }

    protected virtual void OnGetValue()
    {

    }
}

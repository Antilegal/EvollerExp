using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSensor : Sensor
{
    private HingeJoint attachedHinje;
    private Rigidbody attachedRigidbody;

    protected override void OnGetValue()
    {
        base.OnGetValue();

        if (!attachedHinje) attachedHinje = GetComponent<HingeJoint>();
        if (!attachedRigidbody) attachedRigidbody = GetComponent<Rigidbody>();

        if(attachedHinje)
        {
            _value = attachedHinje.angle;
            if (Mathf.Abs(_value) > 360f) _value = _value % 360f;
            
            _value = _value / 360f;
        }
    }
}

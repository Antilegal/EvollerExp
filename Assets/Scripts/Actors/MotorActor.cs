using UnityEngine;

public class MotorActor : Actor
{
    private float motorMaxSpeed = 200f;

    private HingeJoint attachedHinje;

    protected override void OnSetValue()
    {
        base.OnSetValue();

        if (!attachedHinje) attachedHinje = GetComponent<HingeJoint>();

        if (attachedHinje)
        {
            attachedHinje.useMotor = true;
            
            JointMotor motor = attachedHinje.motor;

            motor.targetVelocity = _value * motorMaxSpeed;

            attachedHinje.motor = motor;
        }

    }
}

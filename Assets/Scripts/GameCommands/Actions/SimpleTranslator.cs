using System;
using UnityEngine;


public class SimpleTranslator : SimpleTransformer
{
    public new Rigidbody rigidbody;
    public Vector3 start;
    public Vector3 end;

    public override void PerformTransform(GameCommandType type, float position)
    {

        var curvePosition = accelCurve.Evaluate(position);
        var pos = transform.TransformPoint(Vector3.Lerp(start, end, curvePosition));
        Vector3 deltaPosition = pos - rigidbody.position;
        rigidbody.MovePosition(pos);

        if (m_Platform != null)
            m_Platform.MoveCharacterController(deltaPosition);
    }
}


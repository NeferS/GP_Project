using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Applies a transform to a target object, changing its start rotation to a target rotation*/
public class SimpleRotator : SimpleTransformer
{
    public enum Axis { X, Y, Z}
    /*The axis on with the object will be rotated*/
    public Axis axis;
    public float angle = 90f;
    /*If 'true' the initial rotation of the object will be randomized*/
    public bool random = false;

    private Vector3 rotationAxis, start, end;

    void Start()
    {
        switch(axis)
        {
            case Axis.X:
                rotationAxis = Vector3.right;
                break;
            case Axis.Y:
                rotationAxis = Vector3.up;
                break;
            case Axis.Z:
                rotationAxis = Vector3.forward;
                break;
        }

        if(random)
        {
            StartCoroutine(Randomize());
        }
        else
        {
            start = transform.localEulerAngles;
            end = start + angle * rotationAxis;
        }
    }

    public override void PerformInteraction(GameCommandType type)
    {
        if (type == GameCommandType.Reset)
        {
            activate = false;
            start = transform.localEulerAngles;
            end = start + angle * rotationAxis;
            return;
        }
        activate = true;
        lastReceived = type;
        if (OnStartCommand != null) OnStartCommand.Send();
        if (onStartAudio != null) onStartAudio.Play();
    }

    public override void PerformTransform(GameCommandType type, float position)
    {
        var curve = accelCurve.Evaluate(position);
        transform.localEulerAngles = Vector3.Lerp(start, end, curve);
    }

    /*Randomizes the initial rotation of the object.*/
    private IEnumerator Randomize()
    {
        int r = Random.Range(1, (int)(360/angle));
        transform.localEulerAngles = transform.localEulerAngles + (angle * r )* rotationAxis;
        start = transform.localEulerAngles;
        end = start + rotationAxis * angle;
        yield return null;
    }
}

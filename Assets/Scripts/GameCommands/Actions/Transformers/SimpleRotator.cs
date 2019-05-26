using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Applies a transform to a target object, changing its start rotation to a target rotation*/
public class SimpleRotator : SimpleTransformer
{
    public bool random = false;
    private Vector3 start;
    private Vector3 end;
    private bool reset;

    void Start()
    {
        if(random)
        {
            StartCoroutine(Randomize());
        }
        else
        {
            start = transform.localEulerAngles;
            end = start + new Vector3(0, 90f, 0);
        }
    }

    public override void PerformInteraction(GameCommandType type)
    {
        activate = true;
        lastReceived = type;
        if (OnStartCommand != null) OnStartCommand.Send();
        if (onStartAudio != null && type == GameCommandType.Activate) onStartAudio.Play();
    }

    public override void PerformTransform(GameCommandType type, float position)
    {
        if(type == GameCommandType.Activate)
        {
            reset = false;
            var curve = accelCurve.Evaluate(position);
            transform.localEulerAngles = Vector3.Lerp(start, end, curve);
        }
        else if(type == GameCommandType.Reset && !reset)
        {
            start = end;
            end = start + new Vector3(0, 90f, 0);
            reset = true;
        }
    }

    private IEnumerator Randomize()
    {
        int r = Random.Range(0, 5);
        transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, 90 * r, 0);
        start = transform.localEulerAngles;
        end = start + new Vector3(0, 90f, 0);
        yield return null;
    }
}

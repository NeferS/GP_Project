using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UNUSED
public class MultipleTranslator : GameCommandHandler
{ 
    public bool activate = false;

    public BodiesStartEndPositions[] bodies;

    public float duration = 1;
    public AnimationCurve accelCurve;

    public SendGameCommand OnStartCommand, OnStopCommand;
    public AudioSource onStartAudio, onEndAudio;

    float time = 0f;
    public float position;
    float direction = 1f;

    private void Reset()
    {
        int n = transform.childCount;
        bodies = new BodiesStartEndPositions[n];
        for (int i=0;i<n; i++)
        {
            bodies[i] = transform.GetChild(i).GetComponent<BodiesStartEndPositions>();
        }
    }

    public void FixedUpdate()
    {
        if (activate)
        {
            time = time + (direction * Time.deltaTime / duration);
            LoopOnce();
            PerformTransform(position);
        }
    }

    void LoopOnce()
    {
        position = Mathf.Clamp01(time);
        if (position >= 1)
        {
            enabled = false;
            if (OnStopCommand != null) OnStopCommand.Send();
            direction *= -1;
        }
    }

    public override void PerformInteraction(GameCommandType type)
    {
        activate = true;
        if (OnStartCommand != null) OnStartCommand.Send();
        if (onStartAudio != null) onStartAudio.Play();
    }

    public void PerformTransform(float position)
    {
        var curvePosition = accelCurve.Evaluate(position);
        foreach (BodiesStartEndPositions body in bodies)
        {
            var pos = transform.TransformPoint(Vector3.Lerp(body.start, body.end, curvePosition));
            body.GetComponent<Rigidbody>().MovePosition(pos);
        }
    }
}

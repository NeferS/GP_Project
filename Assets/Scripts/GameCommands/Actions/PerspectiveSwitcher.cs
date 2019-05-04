using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveSwitcher : SimpleTransformer
{
    private ProjectionMatrixes matrixes;
    public Matrix4x4 start, end;

    public bool toOrtho = true;
    public new Camera camera;

    private void Start()
    {
        matrixes = GetComponent<ProjectionMatrixes>();
        if (!toOrtho)
        {
            start = matrixes.ortho;
            end = matrixes.perspective;
        }
        else
        {
            start = matrixes.perspective;
            end = matrixes.ortho;
        }
    }

    public override void PerformInteraction(GameCommandType type)
    {
        activate = true;
        lastReceived = type;
        if (OnStartCommand != null) OnStartCommand.Send();
        if (onStartAudio != null && (type == GameCommandType.Activate || type == GameCommandType.Deactivate))
            onStartAudio.Play();
    }

    public override void PerformTransform(GameCommandType type, float position)
    {
        if(type == GameCommandType.Activate || type == GameCommandType.Deactivate)
        {
            matrixes.orthoOn = !matrixes.orthoOn;
            var curvePosition = accelCurve.Evaluate(position);
            Matrix4x4 matrixPos = new Matrix4x4();
            for (int i = 0; i < 16; i++)
                matrixPos[i] = Mathf.Lerp(start[i], end[i], curvePosition);
            camera.projectionMatrix = matrixPos;
        }
        else if(type == GameCommandType.Reset)
        {
            InvertMatrixes();
        }
    }

    private void InvertMatrixes()
    {
        var tmp = start;
        start = end;
        end = tmp;
        toOrtho = !toOrtho;
    }
}




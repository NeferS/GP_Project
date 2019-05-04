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

    public override void PerformTransform(float position)
    {
        matrixes.orthoOn = !matrixes.orthoOn;
        var curvePosition = accelCurve.Evaluate(position);
        Matrix4x4 matrixPos = new Matrix4x4();
        for (int i = 0; i < 16; i++)
            matrixPos[i] = Mathf.Lerp(start[i], end[i], curvePosition);
        camera.projectionMatrix = matrixPos;
    }
}




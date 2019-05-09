using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of 'GameCommandHandler'; it performs a projection switch to the camera in both directions (from perspective
 *to orthographic and vice-versa).*/
public class PerspectiveSwitcher : GameCommandHandler
{
    private ProjectionMatrixes matrixes;
    public Matrix4x4 start, end;
    public float duration = 1;
    public AnimationCurve accelCurve;
    public AudioSource onStartAudio;
    public bool toOrtho = true;
    public new Camera camera;
    public bool activate = false;

    float time = 0f;
    float position = 0f;
    float direction = 1f;

    /*Initializes the ProjectionMatrixes that will be used as starting matrix and ending matrix, with respect to the
     *value of the variable 'toOrtho' (if 'true' the starting matrix will be set perspective and the ending matrix will be
     *set orthographic)*/
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

    /*Performs the interaction (the switch) with respect to the received GameCommandType*/
    public override void PerformInteraction(GameCommandType type)
    {
        if (type == GameCommandType.Activate || type == GameCommandType.Deactivate)
        {
            activate = true;
            if (onStartAudio)
                onStartAudio.Play();
            matrixes.orthoOn = !matrixes.orthoOn;
        }
        else if (type == GameCommandType.Reset)
        {
            InvertMatrixes();
        }
    }
    
    /*Gradually interpolates the start and the end matrices to switch the view mode*/
    private void PerformSwitch(float position)
    {
        var curvePosition = accelCurve.Evaluate(position);
        Matrix4x4 matrixPos = new Matrix4x4();
        for (int i = 0; i < 16; i++)
            matrixPos[i] = Mathf.Lerp(start[i], end[i], curvePosition);
        camera.projectionMatrix = matrixPos;
    }

    /*Simply exchanges the pointers of the start and end matrices*/
    private void InvertMatrixes()
    {
        var tmp = start;
        start = end;
        end = tmp;
        toOrtho = !toOrtho;
    }

    void FixedUpdate()
    {
        if (activate)
        {
            time = time + (direction * Time.deltaTime / duration);
            position = Mathf.Clamp01(time);
            if (position >= 1)
            {
                activate = false;
                time = 0f;
            }
            PerformSwitch(position);
        }
    }
}




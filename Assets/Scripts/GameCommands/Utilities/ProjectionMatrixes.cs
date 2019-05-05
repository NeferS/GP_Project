using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: comments
public class ProjectionMatrixes : MonoBehaviour

{
    public Matrix4x4 ortho,
                     perspective;
    public float near = .3f,
                 fov = 50f,
                 far = 1000f,
                 orthographicSize = 10f;

    private float aspect;
    private float previusAspect;

    public bool orthoOn;

    public new Camera camera;

    void Start()
    {
        aspect = (float)Screen.width / (float)Screen.height;
        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
        perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        camera.projectionMatrix = perspective;
    }

    private void FixedUpdate()
    {
        aspect = (float)Screen.width / (float)Screen.height;
        if (aspect != previusAspect)
        {
            ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
            perspective = Matrix4x4.Perspective(fov, aspect, near, far);
            if (orthoOn)
            {
                camera.projectionMatrix = ortho;
            }
            else
            {
                camera.projectionMatrix = perspective;
            }
        }
        previusAspect = aspect;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script pre-calculates the two matrices (perspective and orthographic) that has to be applied to the camera when a
 *camera interaction occurs.*/
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

    /*If the screen dimension (aspect) changes, new matrices are calculated with respect to the new aspect.*/
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

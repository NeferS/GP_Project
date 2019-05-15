using UnityEngine;

/*This script lerps the camera position and the camera rotation to the target position and rotation for a camera interaction*/
public class PositionAdjust : SimpleTransformer
{
    public Transform cameraPivot;
    public Transform cameraTarget;
    
    private Vector3 cameraDefaultPosition;
    private Vector3 cameraDefaultRotation;

    public bool isCameraLocked = false;

    public override void PerformInteraction(GameCommandType type)
    {
        base.PerformInteraction(type);
        if (!isCameraLocked)
        {
            if (cameraPivot.localEulerAngles.y > 180)
                cameraTarget.localEulerAngles = new Vector3(cameraTarget.localEulerAngles.x, 359.99f, cameraTarget.localEulerAngles.z);
            cameraDefaultPosition = cameraPivot.localPosition;
            cameraDefaultRotation = cameraPivot.localEulerAngles;
        }
        isCameraLocked = !isCameraLocked;
    }

    public override void PerformTransform(GameCommandType type, float position)
    {
        var curvePosition = accelCurve.Evaluate(position);
        if (isCameraLocked)
        {
            cameraPivot.localPosition = Vector3.Lerp(cameraDefaultPosition, cameraTarget.localPosition, curvePosition);
            cameraPivot.localEulerAngles = Vector3.Lerp(cameraDefaultRotation, cameraTarget.localEulerAngles, curvePosition);
        }
        else
        {
            cameraPivot.localPosition = Vector3.Lerp(cameraTarget.localPosition, cameraDefaultPosition, curvePosition);
            cameraPivot.localEulerAngles = Vector3.Lerp(cameraTarget.localEulerAngles, cameraDefaultRotation, curvePosition);
        }
    }
}

using UnityEngine;

//TODO: comments
public class PositionAdjust : SimpleTransformer
{
    public Transform player;
    public Transform cameraPivot;
    public Transform cameraTarget;
    
    private Quaternion playerStartRotation;
    private Vector3 cameraDefaultPosition;
    private Vector3 cameraDefaultRotation;
    private Vector3 cameraRigStartRotation;

    public bool isCameraLocked = false;

    public override void PerformInteraction(GameCommandType type)
    {
        base.PerformInteraction(type);
        if (!isCameraLocked)
        {
            cameraDefaultPosition = cameraPivot.localPosition;
            cameraDefaultRotation = cameraPivot.localEulerAngles;
            playerStartRotation = player.rotation;
            cameraRigStartRotation = cameraPivot.parent.localEulerAngles;
        }
        isCameraLocked = !isCameraLocked;
    }

    public override void PerformTransform(GameCommandType type, float position)
    {
        var curvePosition = accelCurve.Evaluate(position);
        if (isCameraLocked)
        {

            //rotazione player
            player.rotation = Quaternion.Lerp(playerStartRotation, transform.rotation, curvePosition);

            //posizione camera
            cameraPivot.localPosition = Vector3.Lerp(cameraDefaultPosition, cameraTarget.localPosition, curvePosition);
            //cameraPivot.position = transform.TransformPoint(Vector3.Lerp(transform.TansformPoint(cameraDefoultPosition),cameraTarget.position,curvePosition);

            //rotazione camera
            cameraPivot.parent.localEulerAngles = Vector3.Lerp(cameraRigStartRotation, Vector3.zero, curvePosition);
            cameraPivot.localEulerAngles = Vector3.Lerp(cameraDefaultRotation, cameraTarget.localEulerAngles, curvePosition);
        }
        else
        {
            //posizione camera
            cameraPivot.localPosition = Vector3.Lerp(cameraTarget.localPosition, cameraDefaultPosition, curvePosition);

            //rotazione camera
            cameraPivot.localEulerAngles = Vector3.Lerp(cameraTarget.localEulerAngles, cameraDefaultRotation, curvePosition);
        }
    }
}

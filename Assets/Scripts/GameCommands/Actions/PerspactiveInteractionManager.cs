using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspactiveInteractionManager : GameCommandHandler
{
    public CharacterInput characterInput;
    public GameCommandReceiver manager;
    public new GameCommandReceiver camera;
    public GameCommandReceiver[] interactables;

    public float moveCharacterTime = 3f,
                 perspectiveSwitchTime = 1f,
                 interactionTime = 5f;

    public override void PerformInteraction()
    {
        StartCoroutine(Interaction());
    }

    private IEnumerator Interaction()
    {

        // disattiva il CharacterController
        characterInput.EnableMovement(false);

        // disattiva gli script della camera che interferirebbero con PositionAdjust
        CameraLook mouseLook = characterInput.GetComponentInChildren<CameraLook>();
        ProtectCameraFromWallClip protectCameraFromWallClip = characterInput.GetComponentInChildren<ProtectCameraFromWallClip>();
        if (mouseLook)
        {
            mouseLook.enabled = false;
        }
        if (protectCameraFromWallClip)
        {
            protectCameraFromWallClip.enabled = false;
        }

        //posiziona player e camera al giusto posto
        manager.Receive(GameCommandType.Start);
        yield return new WaitForSeconds(moveCharacterTime);

        //cambia la proiezione della camera
        camera.Receive(GameCommandType.Activate);
        yield return new WaitForSeconds(perspectiveSwitchTime);

        //attiva i vari interactables
        foreach (GameCommandReceiver receiver in interactables)
            receiver.Receive(GameCommandType.Activate);
        yield return new WaitForSeconds(interactionTime);
        
        //inverte le matrici della camera
        camera.Receive(GameCommandType.Start);
        yield return new WaitForSeconds(perspectiveSwitchTime);

        //resetta la proiezione della camera
        camera.Receive(GameCommandType.Deactivate);
        yield return new WaitForSeconds(perspectiveSwitchTime);

        //riposiziona la camera
        manager.Receive(GameCommandType.Start);
        yield return new WaitForSeconds(moveCharacterTime);

        //riattiva il CharacterController e gli script della camera
        characterInput.EnableMovement(true);
        if (mouseLook)
        {
            mouseLook.enabled = true;
        }
        if (protectCameraFromWallClip)
        {
            protectCameraFromWallClip.enabled = true;
        }
    }
}

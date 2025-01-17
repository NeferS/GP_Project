﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of 'GameCommandHandler'. It performs all the operations in order to switch the camera view, move each interactable
 *object and then switch back the camera view.*/
public class PerspectiveSwitchManager : GameCommandHandler
{
    public CharacterInput characterInput;
    public GameCommandReceiver manager;
    public new GameCommandReceiver camera;
    public GameCommandReceiver[] interactables;

    public float moveCameraTime = 5f,
                 perspectiveSwitchTime = 2f,
                 cameraResetTime = 1.5f,
                 interactionTime = 5f;

    public override void PerformInteraction(GameCommandType type)
    {
        StartCoroutine(Interaction(type));
    }

    private IEnumerator Interaction(GameCommandType type)
    {
        /*disables the character input script*/
        characterInput.enabled = false;

        /*disables the script in the camera that interferes with the 'PositionAdjust' script*/
        camera.GetComponentInParent<CameraController>().enabled = false;

        /*moves the character and the camera to the right places*/
        manager.Receive(GameCommandType.Start);
        yield return new WaitForSeconds(moveCameraTime);

        /*changes the camera projection*/
        camera.Receive(GameCommandType.Activate);
        yield return new WaitForSeconds(perspectiveSwitchTime);

        /*enables all the interactables objects in 'interactables'*/
        foreach (GameCommandReceiver receiver in interactables)
            receiver.Receive(GameCommandType.Activate);
        yield return new WaitForSeconds(interactionTime);

        /*switches the matrices of the camera*/
        camera.Receive(GameCommandType.Reset);
        yield return new WaitForSeconds(cameraResetTime);

        /*resets the camera projection*/
        camera.Receive(GameCommandType.Deactivate);
        yield return new WaitForSeconds(perspectiveSwitchTime);

        /*places the camera to its original position*/
        manager.Receive(GameCommandType.Start);
        yield return new WaitForSeconds(moveCameraTime);

        /*enables the character input script and the script in the camera*/
        characterInput.enabled = true;
        camera.GetComponentInParent<CameraController>().enabled = true;
    }
}

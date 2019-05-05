using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of Controller; it's a simple controller for the main scene.*/
public class MainMenuController : Controller
{

    [SerializeField] private Texture2D _cursorTexture;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Cursor.SetCursor(_cursorTexture, new Vector2(15.0f, 15.0f), CursorMode.Auto);
    }

    public override void ExitTriggered() { Application.Quit(); }
}

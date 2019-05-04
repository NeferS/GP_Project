using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : Controller
{

    [SerializeField] private Texture2D _cursorTexture;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Cursor.SetCursor(_cursorTexture, new Vector2(15.0f, 15.0f), CursorMode.Auto);
    }


    void Update()
    {
        
    }

    public override void ExitTriggered() { }

    public void Quit() { Application.Quit(); }
}

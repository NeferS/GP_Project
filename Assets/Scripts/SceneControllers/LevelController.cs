using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is a subclass of Controller; it's a simple controller for the level scene.*/
public class LevelController : Controller
{
    [SerializeField] private Texture2D _cursorTexture;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Cursor.SetCursor(_cursorTexture, new Vector2(15.0f, 15.0f), CursorMode.Auto);
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public override void ExitTriggered()
    {
        SceneLoader sl = GetComponent<SceneLoader>();
        sl.enabled = true;
        sl.Reset();
        sl.Load(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private Texture2D _cursorTexture;

    void Start()
    {
        Cursor.SetCursor(_cursorTexture, new Vector2(15.0f, 15.0f), CursorMode.Auto);
    }


    void Update()
    {
        
    }

    public void Quit() { Application.Quit(); }
}

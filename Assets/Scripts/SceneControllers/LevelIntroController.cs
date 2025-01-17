﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This class is a subclass of Controller; it's a simple controller for the level intro scene.*/
[RequireComponent(typeof(SceneLoader))]
public class LevelIntroController : Controller
{
    [SerializeField] private Text textComponent;
    /*Value used to gradually fade in or out the text inside the canvas*/
    private const float alphaVariationPerStep = 0.007f;
    /*Value used to determine how much time a message has to be displayed on the scene*/
    public float timeBeforeFade = 3.0f;
    public string[] messages;
    private int lastIndex = 0;
    private bool canContinue = true, fadingOut = false;
    private int q = 0;

    void Start()
    {
        GetComponent<SceneLoader>().enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        textComponent.color = new Color(255, 255, 255, 0);
    }


    void Update()
    {
        if(canContinue)
        {
            textComponent.text = messages[lastIndex];
            switch(q)
            {
                case 0: /*Fades in the current message*/
                    {
                        if (textComponent.color.a < 1)
                            textComponent.color = new Color(255, 255, 255, textComponent.color.a + alphaVariationPerStep);
                        else
                        {
                            q = 1;
                            canContinue = false;
                            StartCoroutine(TimeUntilNext());
                        }
                        break;
                    }
                case 1: /*Fades out the current message*/
                    {
                        if (textComponent.color.a > 0)
                            textComponent.color = new Color(255, 255, 255, textComponent.color.a - alphaVariationPerStep);
                        else
                        {
                            q = 0;
                            lastIndex++;
                            /*The last message has been shown, so the Level Scene is loaded*/
                            if(lastIndex == messages.Length)
                            {
                                canContinue = false;
                                SceneLoader loader = GetComponent<SceneLoader>();
                                loader.enabled = true;
                                loader.Load(0);
                            }
                        }
                    }
                    break;
            }
        }
    }

    private IEnumerator TimeUntilNext()
    {
        yield return new WaitForSeconds(timeBeforeFade);
        canContinue = true;
    }

    public override void ExitTriggered()
    {
        throw new System.NotImplementedException();
    }
}

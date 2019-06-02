using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This class is a subclass of Controller; it's a simple controller for the level intro scene.*/
[RequireComponent(typeof(SceneLoader))]
public class EndLevelController : Controller
{
    [SerializeField] private Text textComponent;
    [SerializeField] private Image logo;
    private AudioSource _audioSource;
    /*Value used to gradually fade in or out the text inside the canvas*/
    private const float variationPerStep = 0.007f;
    /*Value used to determine how much time a message has to be displayed on the scene*/
    public float timeBeforeFade = 3.0f;
    public string[] messages;
    private int lastIndex = 0;
    private bool canContinue = true, fadingOut = false;
    private int q = 0;

    void Start()
    {
        GetComponent<SceneLoader>().enabled = false;
        _audioSource = GetComponentInChildren<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        textComponent.color = new Color(255, 255, 255, 0);
    }


    void Update()
    {
        if (canContinue)
        {
            if(lastIndex < messages.Length && lastIndex != messages.Length-1)
                textComponent.text = messages[lastIndex];
            switch (q)
            {
                case 0: /*Fades in the current message*/
                    {
                        if (textComponent.color.a < 1)
                            textComponent.color = new Color(255, 255, 255, textComponent.color.a + variationPerStep);
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
                        {
                            textComponent.color = new Color(255, 255, 255, textComponent.color.a - variationPerStep);
                            if(lastIndex == messages.Length-1) 
                            {
                                /*Fades out the music*/
                                float newVolume = _audioSource.volume - variationPerStep;
                                if (newVolume <= 0.0f)
                                {
                                    newVolume = 0.0f;
                                    _audioSource.Stop();
                                }
                                _audioSource.volume = newVolume;
                            }
                        }
                        else
                        {
                            q = 0;
                            lastIndex++;
                            /*Shows the logo of the game*/
                            if (lastIndex == messages.Length-1)
                            {
                                q = 3;
                                timeBeforeFade = 5.0f;
                                logo.enabled = true;
                            }
                            /*The last message has been shown, so the main menu scene can be loaded*/
                            if (lastIndex == messages.Length)
                            {
                                canContinue = false;
                                SceneLoader loader = GetComponent<SceneLoader>();
                                loader.enabled = true;
                                loader.Load(0);
                            }
                        }
                    }
                    break;
                case 3: /*Fades in the logo*/
                    {
                        if (logo.color.a < 1)
                            logo.color = new Color(255, 255, 255, logo.color.a + variationPerStep);
                        else
                        {
                            q = 4;
                            canContinue = false;
                            StartCoroutine(TimeUntilNext());
                        }
                        break;
                    }
                case 4: /*Fades out the logo*/
                    {
                        if(logo.color.a > 0)
                            logo.color = new Color(255, 255, 255, logo.color.a - variationPerStep);
                        else
                        {
                            textComponent.text = "Developed by \n\nAlessio Iocca\nMarco Scarfone\nVincenzo Parrilla";
                            q = 0;
                        }
                        break;
                    }
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

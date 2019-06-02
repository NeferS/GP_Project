using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/*This class performs a fade to black from a scene and loads a previously specified scene.*/
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject _gui;
    /*More than one scene can be loaded by this script just selecting an index on this array, which
     *contains the names of the desired scenes*/
    [SerializeField] private string[] scenesNames;
    public float fadingStep = 0.007f;

    private Image blackPanel;
    private const string scenesPath = "Scenes/";
    /*if 'true', the script performs the fade to black and loads the new scene*/
    private bool load = false;
    private int _index;
    public bool initialize = true;

    void Start()
    {
        if(initialize)
            Reset();
    }


    void Update()
    {
        if(load)
        {
            if(blackPanel && blackPanel.color.a < 1)
            {
                blackPanel.color = new Color(0f, 0f, 0f, blackPanel.color.a + fadingStep);
            }
            else
            {
                SceneManager.LoadScene(scenesPath + scenesNames[_index]);
            }
        }
    }
    /*First of all, sets some important values for a correct execution of the 'LoadScene' method. Moreover, it disables
     *any button from the gui (if it exists) because any of them could interfere with this*/
    public void Load(int index)
    {
        load = true;
        _index = index;
        if(blackPanel)
            blackPanel.enabled = true;
        foreach (Button btn in _gui.GetComponentsInChildren<Button>())
        {
            btn.enabled = false;
            btn.GetComponent<EventTrigger>().enabled = false;
            btn.GetComponentInChildren<Text>().enabled = false;
        }
    }

    /*Gets an Image component from the canvas or creates one if it does not exist.*/
    public void Reset()
    {
        Image img = _gui.GetComponent<Image>();
        if (img)
            blackPanel = img;
        else
            blackPanel = _gui.AddComponent<Image>();

        blackPanel.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        blackPanel.enabled = false;
    }
}

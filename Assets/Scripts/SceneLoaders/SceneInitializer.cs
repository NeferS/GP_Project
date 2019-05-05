using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This abstract class performs a simple fade from black to the scene. When it ends, the 'DoAction' method is invoked
 *on the subclasses; this method defines a specific action that has to be done after the fade completation.*/
public abstract class SceneInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _gui;

    private Image blackPanel;
    private float fadingStep = 0.007f;

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if (blackPanel != null && blackPanel.color.a > 0)
        {
            blackPanel.color = new Color(0f, 0f, 0f, blackPanel.color.a - fadingStep * 2);
        }
        else
        {
            DoAction();
        }
    }

    /*Gets from the '_gui' the image that will be used to perform the fade or creates one if it doesn't exist*/
    public void Reset()
    {
        Image img = _gui.GetComponent<Image>();
        if (img != null)
            blackPanel = img;
        else
            blackPanel = _gui.AddComponent<Image>();
        blackPanel.color = Color.black;
    }

    public abstract void DoAction();
}

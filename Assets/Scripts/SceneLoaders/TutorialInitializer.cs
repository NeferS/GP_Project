using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _gui;

    private Image blackPanel;
    private float fadingStep = 0.007f;

    void Start()
    {
        blackPanel = _gui.AddComponent<Image>();
        blackPanel.color = Color.black;
    }

    void Update()
    {
        if (blackPanel.color.a > 0)
        {
            blackPanel.color = new Color(0f, 0f, 0f, blackPanel.color.a - fadingStep * 2);
        }
        else
        {
            GetComponent<TutorialController>().enabled = true;
            blackPanel.enabled = false;
            blackPanel.color = new Color(0f, 0f, 0f, 1f);
            enabled = false;
        }
    }

    public void Reset()
    {
        blackPanel.enabled = true;
        enabled = true;
    }
}

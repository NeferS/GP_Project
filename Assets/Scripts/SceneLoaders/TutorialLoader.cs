using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialLoader : MonoBehaviour
{
    [SerializeField] private GameObject _gui;

    private Image blackPanel;
    private float fadingStep = 0.007f;

    private bool load = false;

    void Start()
    {
        blackPanel = _gui.AddComponent<Image>();
        blackPanel.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        blackPanel.enabled = false;
    }


    void Update()
    {
        if(load)
        {
            if(blackPanel.color.a < 1)
            {
                blackPanel.color = new Color(0f, 0f, 0f, blackPanel.color.a + fadingStep);
            }
            else
            {
                SceneManager.LoadScene("Scenes/Tutorial");
            }
        }
    }

    public void LoadTutorial()
    {
        load = true;
        blackPanel.enabled = true;
        foreach (Text txt in _gui.GetComponentsInChildren<Text>())
        {
            txt.enabled = false;
        }
        
    }
}

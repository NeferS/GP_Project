using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject _gui;
    [SerializeField] private string[] scenesNames;
    public float fadingStep = 0.007f;

    private Image blackPanel;
    private const string scenesPath = "Scenes/";
    private bool load = false;
    private int _index;

    void Start()
    {
        Reset();
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
                SceneManager.LoadScene(scenesPath + scenesNames[_index]);
            }
        }
    }

    public void Load(int index)
    {
        load = true;
        _index = index;
        blackPanel.enabled = true;
        foreach (Button btn in _gui.GetComponentsInChildren<Button>())
        {
            btn.enabled = false;
            btn.GetComponent<EventTrigger>().enabled = false;
            btn.GetComponentInChildren<Text>().enabled = false;
        }
    }

    public void Reset()
    {
        Image img = _gui.GetComponent<Image>();
        if (img != null)
            blackPanel = img;
        else
            blackPanel = _gui.AddComponent<Image>();

        blackPanel.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        blackPanel.enabled = false;
    }
}

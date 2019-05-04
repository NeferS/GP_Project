using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(SceneLoader))]
public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _gui;
    public enum ActionOnCompletation
    {
        Nothing = 0,
        ControllerAndLoader = 1
    };
    public ActionOnCompletation onCompletationAction = ActionOnCompletation.Nothing;

    private Image blackPanel;
    private float fadingStep = 0.007f;

    void Start()
    {
        GetComponent<SceneLoader>().enabled = false;
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

    public void Reset()
    {
        Image img = _gui.GetComponent<Image>();
        if (img != null)
            blackPanel = img;
        else
            blackPanel = _gui.AddComponent<Image>();
        blackPanel.color = Color.black;
    }

    protected void DoAction()
    {
        switch (onCompletationAction)
        {
            case ActionOnCompletation.Nothing:
                break;
            case ActionOnCompletation.ControllerAndLoader:
                GetComponent<Controller>().enabled = true;
                SceneLoader sl = GetComponent<SceneLoader>();
                sl.Reset();
                sl.enabled = true;
                enabled = false;
                break;
        }
    }
}

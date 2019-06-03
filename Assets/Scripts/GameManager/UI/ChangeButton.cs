using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeButton : MonoBehaviour
{

    [SerializeField] private Sprite onYesImage;
    [SerializeField] private Sprite offYesImage;
    [SerializeField] private Sprite onNoImage;
    [SerializeField] private Sprite offNoImage;
    [SerializeField] private Button yes;
    [SerializeField] private Button no;
    [SerializeField] private bool YesEnable;
    [SerializeField] private bool NoEnable;

    void Start()
    {
        yes.interactable = YesEnable;
        no.interactable = NoEnable;
    }


    void Update()
    {

    }


    public void OnButtonChange()
    {
        if (no.IsInteractable())
        {
            yes.GetComponent<Image>().sprite = offYesImage;
            no.GetComponent<Image>().sprite = onNoImage;
            no.interactable = false;
            yes.interactable = true;
        }else if (yes.IsInteractable())
        {
            yes.GetComponent<Image>().sprite = onYesImage;
            no.GetComponent<Image>().sprite = offNoImage;

            no.interactable = true;
            yes.interactable = false;
        }

    }
}

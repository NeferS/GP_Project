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

    // Start is called before the first frame update
    void Start()
    {
       //Color color = yes.colors;
        //color.a = transparency;
        //ButtonBackground.color = color;
        //yes.colors = 
        yes.interactable = YesEnable;
        no.interactable = NoEnable;
    }

    // Update is called once per frame
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : Controller
{
    /*Intro Section*/
    //intro objects
    [SerializeField] private Text _t0, _t1; 

    /*Movement Section*/
    //wasd objects
    [SerializeField] private Text _wasdTxt;
    [SerializeField] private Image _wasdPanelImg, _wasdImg;
    [SerializeField] private GameObject _wasdCheckBox;

    //jump and crouch objects
    [SerializeField] private Text _jcTxt;
    [SerializeField] private Image _jcPanelImg, _jcImg;
    [SerializeField] private GameObject _jcCheckBox;

    /*Interactable Section*/
    //push and pull objects
    [SerializeField] private GameObject boxPrefab;
    private GameObject box;
    [SerializeField] private Text _interactTxt;
    [SerializeField] private Image _interactPanelImg, _interactImg;
    [SerializeField] private GameObject _interactCheckBox;

    /*FieldCamera Section*/
    [SerializeField] private GameObject camTutorialPrefab;
    private GameObject camTutorial;
    [SerializeField] private Text _cameraTxt;
    [SerializeField] private Image _cameraPanelImg;
    [SerializeField] private GameObject _cameraCheckBox;
    private PerspectiveSwitcher[] _switchers;

    [SerializeField] private GameObject _player;
    [SerializeField] private Sprite _emptyCheckBox;
    [SerializeField] private Sprite _fullCheckBox;

    private int q;
    private const float alphaVariationPerStep = 0.007f;
    private const int Q0 = 0, Q1 = 1, Q2 = 2, Q3 = 3, Q4 = 4, Q5 = 5, Q6 = 6, Q7 = 7, Q8 = 8,
                      Q9 = 9, Q10 = 10, Q11 = 11, Q12 = 12, Q13 = 13, Q14 = 14;
    private const int W = 0, A = 1, S = 2, D = 3, SPACE = 4, C = 5, E = 6, V = 7;

    private bool[] keysPressed = { false, false, false, false, false, false, false, false };
    private bool exitTriggered = false;

    void Start()
    {
        q = Q0;
        _t0.enabled = true;
        _t0.color = new Color(_t0.color.r, _t0.color.g, _t0.color.b, 0);
        _t1.color = new Color(_t1.color.r, _t1.color.g, _t1.color.b, 0);
        _player.GetComponent<CharacterInput>().EnableMovement(false);
        _player.GetComponent<CharacterInput>().EnableJump(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        enabled = false;
    }


    void Update()
    {
        switch(q)
        {
            case Q0:
                if (_t0.color.a <= 1)
                {
                    _t0.color = new Color(_t0.color.r, _t0.color.g, _t0.color.b, _t0.color.a + alphaVariationPerStep);
                }
                else
                {
                    _t1.enabled = true;
                    q = Q1;
                }
                break;
            case Q1:
                if (_t1.color.a <= 1)
                {
                    _t0.color = new Color(_t0.color.r, _t0.color.g, _t0.color.b, _t0.color.a - alphaVariationPerStep * 2);
                    _t1.color = new Color(_t1.color.r, _t1.color.g, _t1.color.b, _t1.color.a + alphaVariationPerStep);
                }
                else
                {
                    _t0.enabled = false;
                    q = Q2;
                }
                break;
            case Q2:
                if (_t1.color.a > 0)
                {
                    _t1.color = new Color(_t0.color.r, _t0.color.g, _t0.color.b, _t0.color.a - alphaVariationPerStep * 2);
                }
                else
                {
                    _t1.enabled = false;

                    _wasdPanelImg.enabled = true;
                    _wasdCheckBox.GetComponent<Image>().enabled = true;
                    _wasdTxt.enabled = true;
                    _wasdImg.enabled = true;
                    q = Q3;
                    _player.GetComponent<CharacterInput>().EnableMovement(true);
                }
                break;
            case Q3:
                if(Input.GetKeyDown(KeyCode.W)) { keysPressed[W] = true; }
                if(Input.GetKeyDown(KeyCode.A)) { keysPressed[A] = true; }
                if(Input.GetKeyDown(KeyCode.S)) { keysPressed[S] = true; }
                if(Input.GetKeyDown(KeyCode.D)) { keysPressed[D] = true; }
                if(keysPressed[W] && keysPressed[A] && keysPressed[S] && keysPressed[D])
                {
                    SetFull(_wasdCheckBox);
                    q = Q4;
                }
                break;
            case Q4:
                if(_wasdTxt.color.a > 0)
                {
                    if (_wasdTxt.color.a <= 100)
                    {
                        _wasdPanelImg.color = new Color(_wasdPanelImg.color.r, _wasdPanelImg.color.g, _wasdPanelImg.color.b,
                                                        _wasdPanelImg.color.a - alphaVariationPerStep * 2);
                    }
                    Color cbColor = _wasdCheckBox.GetComponent<Image>().color;
                    _wasdCheckBox.GetComponent<Image>().color = new Color(cbColor.r, cbColor.g, cbColor.b,
                                                                cbColor.a - alphaVariationPerStep * 2);
                    _wasdTxt.color = new Color(_wasdTxt.color.r, _wasdTxt.color.g, _wasdTxt.color.b,
                                               _wasdTxt.color.a - alphaVariationPerStep * 2);
                    _wasdImg.color = new Color(_wasdImg.color.r, _wasdImg.color.g, _wasdImg.color.b,
                                               _wasdImg.color.a - alphaVariationPerStep * 2);
                }
                else
                {
                    _wasdPanelImg.enabled = false;
                    _wasdCheckBox.GetComponent<Image>().enabled = false;
                    _wasdTxt.enabled = false;
                    _wasdImg.enabled = false;

                    _jcPanelImg.enabled = true;
                    _jcCheckBox.GetComponent<Image>().enabled = true;
                    _jcTxt.enabled = true;
                    _jcImg.enabled = true;
                    q = Q5;
                    _player.GetComponent<CharacterInput>().EnableJump(true);
                }
                break;
            case Q5:
                if (Input.GetKeyDown(KeyCode.Space)) { keysPressed[SPACE] = true; }
                if (Input.GetKeyDown(KeyCode.C) && _player.GetComponent<CharacterController>().isGrounded) { keysPressed[C] = true; }
                if(keysPressed[SPACE] && keysPressed[C])
                {
                    SetFull(_jcCheckBox);
                    q = Q6;
                }
                break;
            case Q6:
                if (_jcTxt.color.a > 0)
                {
                    if (_jcTxt.color.a <= 100)
                    {
                        _jcPanelImg.color = new Color(_jcPanelImg.color.r, _jcPanelImg.color.g, _jcPanelImg.color.b,
                                                      _jcPanelImg.color.a - alphaVariationPerStep * 2);
                    }
                    Color cbColor = _jcCheckBox.GetComponent<Image>().color;
                    _jcCheckBox.GetComponent<Image>().color = new Color(cbColor.r, cbColor.g, cbColor.b,
                                                                cbColor.a - alphaVariationPerStep * 2);
                    _jcTxt.color = new Color(_jcTxt.color.r, _jcTxt.color.g, _jcTxt.color.b,
                                             _jcTxt.color.a - alphaVariationPerStep * 2);
                    _jcImg.color = new Color(_jcImg.color.r, _jcImg.color.g, _jcImg.color.b,
                                             _jcImg.color.a - alphaVariationPerStep * 2);
                }
                else
                {
                    _jcPanelImg.enabled = false;
                    _jcCheckBox.GetComponent<Image>().enabled = false;
                    _jcTxt.enabled = false;
                    _jcImg.enabled = false;

                    _interactPanelImg.enabled = true;
                    _interactCheckBox.GetComponent<Image>().enabled = true;
                    _interactTxt.enabled = true;
                    _interactImg.enabled = true;

                    _player.transform.position = new Vector3(0.0f, 1.5f, 0.0f);
                    _player.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                    box = Instantiate(boxPrefab) as GameObject;
                    box.transform.position = _player.transform.position + new Vector3(0.0f, -1.0f, 5.0f);
                    q = Q7;
                }
                break;
            case Q7:
                if(Input.GetKeyDown(KeyCode.E) && box.GetComponent<FixedJoint>() != null) { keysPressed[E] = true; }
                if(keysPressed[E])
                {
                    SetFull(_interactCheckBox);
                    q = Q8;
                }
                break;
            case Q8:
                if (_interactTxt.color.a > 0)
                {
                    if (_interactTxt.color.a <= 100)
                    {
                        _interactPanelImg.color = new Color(_interactPanelImg.color.r, _interactPanelImg.color.g, 
                                                            _interactPanelImg.color.b, _interactPanelImg.color.a - alphaVariationPerStep * 2);
                    }
                    Color cbColor = _interactCheckBox.GetComponent<Image>().color;
                    _interactCheckBox.GetComponent<Image>().color = new Color(cbColor.r, cbColor.g, cbColor.b,
                                                                cbColor.a - alphaVariationPerStep * 2);
                    _interactTxt.color = new Color(_interactTxt.color.r, _interactTxt.color.g, _interactTxt.color.b,
                                                   _interactTxt.color.a - alphaVariationPerStep * 2);
                    _interactImg.color = new Color(_interactImg.color.r, _interactImg.color.g, _interactImg.color.b,
                                                   _interactImg.color.a - alphaVariationPerStep * 2);
                }
                else
                {
                    Color cbColor = _interactCheckBox.GetComponent<Image>().color;
                    _interactCheckBox.GetComponent<Image>().color = new Color(cbColor.r, cbColor.g, cbColor.b, 1);
                    _interactCheckBox.GetComponent<Image>().sprite = _emptyCheckBox;
                    _interactTxt.color = new Color(_interactTxt.color.r, _interactTxt.color.g, _interactTxt.color.b, 1);
                    _interactTxt.text = "Some interactions last as long as you don't press 'E' again. Press it to release the box.";
                    _interactImg.color = new Color(_interactImg.color.r, _interactImg.color.g, _interactImg.color.b, 1);
                    _interactPanelImg.color = new Color(_interactPanelImg.color.r, _interactPanelImg.color.g, 
                                                        _interactPanelImg.color.b, 0.5f);
                    keysPressed[E] = false;
                    q = Q9;
                }
                break;
            case Q9:
                if (Input.GetKeyUp(KeyCode.E) && box.GetComponent<FixedJoint>() == null) { keysPressed[E] = true; }
                if (keysPressed[E])
                {
                    SetFull(_interactCheckBox);
                    q = Q10;
                }
                break;
            case Q10:
                if (_interactTxt.color.a > 0)
                {
                    if (_interactTxt.color.a <= 100)
                    {
                        _interactPanelImg.color = new Color(_interactPanelImg.color.r, _interactPanelImg.color.g,
                                                            _interactPanelImg.color.b, _interactPanelImg.color.a - alphaVariationPerStep * 2);
                    }
                    Color cbColor = _interactCheckBox.GetComponent<Image>().color;
                    _interactCheckBox.GetComponent<Image>().color = new Color(cbColor.r, cbColor.g, cbColor.b,
                                                                cbColor.a - alphaVariationPerStep * 2);
                    _interactTxt.color = new Color(_interactTxt.color.r, _interactTxt.color.g, _interactTxt.color.b,
                                                   _interactTxt.color.a - alphaVariationPerStep * 2);
                    _interactImg.color = new Color(_interactImg.color.r, _interactImg.color.g, _interactImg.color.b,
                                                   _interactImg.color.a - alphaVariationPerStep * 2);
                }
                else
                {
                    _interactPanelImg.enabled = false;
                    _interactCheckBox.GetComponent<Image>().enabled = false;
                    _interactTxt.enabled = false;
                    _interactImg.enabled = false;

                    _cameraPanelImg.enabled = true;
                    _cameraCheckBox.GetComponent<Image>().enabled = true;
                    _cameraTxt.enabled = true;

                    _player.transform.position = new Vector3(-10.5f, 1.5f, 19.0f);
                    _player.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                    Vector3 angles = _player.transform.localEulerAngles;
                    _player.transform.localEulerAngles = new Vector3(angles.x, 60.0f, angles.z);

                    Destroy(box);
                    box = null;
                    camTutorial = Instantiate(camTutorialPrefab) as GameObject;
                    camTutorial.transform.position = new Vector3(0, 0, 12f);

                    PerspactiveInteractionManager pim = camTutorial.GetComponentInChildren<PerspactiveInteractionManager>();
                    pim.characterInput = _player.GetComponent<CharacterInput>();
                    pim.camera = _player.GetComponentInChildren<GameCommandReceiver>();

                    PositionAdjust pa = camTutorial.GetComponentInChildren<PositionAdjust>();
                    pa.player = _player.transform;
                    pa.cameraPivot = _player.GetComponentInChildren<CameraLook>().transform;

                    camTutorial.GetComponentInChildren<OnTriggerWarning>().controller = this;

                    _switchers = _player.GetComponentsInChildren<PerspectiveSwitcher>();

                    q = Q11;
                }
                break;
            case Q11:
                foreach(PerspectiveSwitcher ps in _switchers)
                {
                    if(ps.activate && ps.interactionType == GameCommandType.Activate)
                    {
                        SetFull(_cameraCheckBox);
                        q = Q12;
                    }
                }
                break;
            case Q12:
                if (_cameraTxt.color.a > 0)
                {
                    if (_cameraTxt.color.a <= 100)
                    {
                        _cameraPanelImg.color = new Color(_cameraPanelImg.color.r, _cameraPanelImg.color.g,
                                                          _cameraPanelImg.color.b, _cameraPanelImg.color.a - alphaVariationPerStep * 2);
                    }
                    Color cbColor = _cameraCheckBox.GetComponent<Image>().color;
                    _cameraCheckBox.GetComponent<Image>().color = new Color(cbColor.r, cbColor.g, cbColor.b,
                                                                            cbColor.a - alphaVariationPerStep * 2);
                    _cameraTxt.color = new Color(_cameraTxt.color.r, _cameraTxt.color.g, _cameraTxt.color.b,
                                                 _cameraTxt.color.a - alphaVariationPerStep * 2);
                }
                else
                {
                    q = Q13;
                }
                break;
            case Q13:
                if (exitTriggered)
                {
                    _cameraCheckBox.SetActive(false);
                    _cameraPanelImg.color = new Color(_cameraPanelImg.color.r, _cameraPanelImg.color.g,
                                                      _cameraPanelImg.color.b, 0.5f);
                    Vector2 size = _cameraPanelImg.rectTransform.rect.size;
                    Vector3 position = _cameraPanelImg.rectTransform.anchoredPosition;
                    _cameraTxt.rectTransform.sizeDelta = size;
                    _cameraTxt.rectTransform.localPosition = Vector3.zero;
                    _cameraTxt.color = new Color(_cameraTxt.color.r, _cameraTxt.color.g, _cameraTxt.color.b, 1);
                    _cameraTxt.text = "Congratulations! You have completed the tutorial. Press 'X' to return to the main menu.";
                    _cameraTxt.alignment = TextAnchor.MiddleCenter;
                    q = Q14;
                }
                break;
            case Q14:
                if (Input.GetKeyDown(KeyCode.X))
                {
                    _cameraTxt.enabled = false;
                    _cameraPanelImg.enabled = false;
                    GetComponent<SceneLoader>().Load(0);
                }
                break;
        }
    }

    private void SetFull(GameObject obj)
    {
        RectTransform cbRect = obj.GetComponent<RectTransform>();
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(90, cbRect.sizeDelta.y);
        obj.GetComponent<RectTransform>().position = new Vector3(cbRect.position.x + 5,
                                                                 cbRect.position.y, cbRect.position.z);
        obj.GetComponent<Image>().sprite = _fullCheckBox;
    }

    public override void ExitTriggered() { exitTriggered = true; }
}

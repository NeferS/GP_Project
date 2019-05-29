﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private SettingPopup settingPopup;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        settingPopup.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && count == 0)
        {
            settingPopup.Open();
            count++;
        }else if (Input.GetKeyDown(KeyCode.Escape) && count == 1)
        {
            settingPopup.Close();
            count--;
        } else
        {
            count = 0;
        }
    }


    public void OnOpenSetting()
    {
        settingPopup.Open();
    }

    public void OnCloseSetting()
    {
        settingPopup.Close();
    }

    public void SwitchMainScene()
    {
        settingPopup.Close();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }
}

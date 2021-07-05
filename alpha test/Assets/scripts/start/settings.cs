using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    public Toggle fsToggle;
    public Slider baMusic;
    public Slider efMusic;
    public float v;
    public GameObject btnQuit;
    // Start is called before the first frame update
    void Start()
    {
        if (Screen.fullScreen)
        {
            fsToggle.isOn = true;
        }
        else
        {
            fsToggle.isOn = false;
        }
        baMusic.value = index.bgMusicVolume;
        efMusic.value = index.efMusicVolume;
        fsToggle.onValueChanged.AddListener((bool value) => OnToggleClick(fsToggle, value));
        baMusic.onValueChanged.AddListener((float value) => OnbaMusicChanged(baMusic));
        efMusic.onValueChanged.AddListener((float value) => OnefMusicChanged(efMusic));
        btnQuit.GetComponent<Button>().onClick.AddListener(onQuitClick);
    }
    private void OnToggleClick(Toggle toggle, bool value)
    {
        //Debug.Log("toggle change " + (value ? "On" : "Off"));
        if (Screen.fullScreen)
        {
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
        }
    }
    private void OnbaMusicChanged(Slider slider)
    {
        index.bgMusicVolume = slider.value;
        //index.bgMusic.volume = value;
        v = slider.value;
    }
    private void OnefMusicChanged(Slider slider)
    {
        index.efMusicVolume = slider.value;
    }

    private void onQuitClick()
    {
        SceneManager.LoadScene("index");
    }
}

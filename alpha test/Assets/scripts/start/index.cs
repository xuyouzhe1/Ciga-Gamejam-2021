using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class index : MonoBehaviour
{
    private Scene scene;
    public AudioSource bgMusic;
    
    public GameObject btnStart;
    public GameObject btnSettings;
    public GameObject btnQuit;

    public static float bgMusicVolume = 1;
    public static float efMusicVolume = 1;
    
    void Start()
    {
        btnStart.GetComponent<Button>().onClick.AddListener(_Play);
        btnSettings.GetComponent<Button>().onClick.AddListener(_Setting);
        btnQuit.GetComponent<Button>().onClick.AddListener(_Quit);
        bgMusic.volume = bgMusicVolume;
    }

    void _Play()
    {
        SceneManager.LoadScene("playerSelect");
    }
    void _Setting()
    {
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        scene = SceneManager.LoadScene("playerSetting", parameters);

    }
    void _Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    private void Update()
    {
        bgMusic.volume = bgMusicVolume;
    }
}
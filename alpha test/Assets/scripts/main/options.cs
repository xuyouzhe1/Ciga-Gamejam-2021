using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class options : MonoBehaviour
{
    public GameObject btnOption;
    public GameObject btnOptionContinue;
    public GameObject btnOptionSetting;
    public GameObject btnOptionQuit;

    private bool isActiveOptions; //是否处于options界面
    void Start()
    {
        btnOption.GetComponent<Button>().onClick.AddListener(_option);

        btnOptionContinue.GetComponent<Button>().onClick.AddListener(_continue);
        btnOptionSetting.GetComponent<Button>().onClick.AddListener(_setting);
        btnOptionQuit.GetComponent<Button>().onClick.AddListener(_quit);

        isActiveOptions = false;
    }


    void _option()
    {
        GameObject parentObj = GameObject.Find("UIRoot");
        GameObject child = parentObj.transform.Find("Options").gameObject;
        child.SetActive(true);
        isActiveOptions = true;
    }

    void _continue()
    {
        GameObject.Find("Options").SetActive(false);
    }
    void _setting()
    {
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        SceneManager.LoadScene("playerSetting", parameters);
    }
    void _quit()
    {
        SceneManager.LoadScene("index");
    }
}

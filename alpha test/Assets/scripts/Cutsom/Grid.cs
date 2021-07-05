using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject dice;
    //骰子的数字以及行动代表色
    public int num;
    public string actionColour;


    //随机生成骰面数字
    public int randomDiceFace()
    {
        num = Random.Range(1,7);
        return num;
    }
    public void setMaterial(Material material)
    {

        //更改材质球
        dice.gameObject.GetComponentInChildren<MeshRenderer>().materials[0].CopyPropertiesFromMaterial(material);
    }

    public void setActionColour(string colour)
    {
        this.actionColour = colour;
    }
    private void Start()
    {
        if (transform.childCount > 0)
        {
            dice = transform.GetChild(0).gameObject;
        }
    }
}

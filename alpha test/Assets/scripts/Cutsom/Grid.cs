using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject dice;
    //���ӵ������Լ��ж�����ɫ
    public int num;
    public string actionColour;


    //���������������
    public int randomDiceFace()
    {
        num = Random.Range(1,7);
        return num;
    }
    public void setMaterial(Material material)
    {

        //���Ĳ�����
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

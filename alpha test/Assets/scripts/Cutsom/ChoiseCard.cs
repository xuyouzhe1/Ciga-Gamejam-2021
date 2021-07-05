using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiseCard : MonoBehaviour
{
    //��ǰ��ѡ���ڵķ���
    public Grid[] grids;
    //��ǰ��ѡ���ڵ�����
    public GameObject[] dices;
    private void Awake()
    {
        grids = new Grid[3];
        dices = new GameObject[3];
        grids = GetComponentsInChildren<Grid>();


    }

    public void commit()
    {
        grids = GetComponentsInChildren<Grid>();
        Debug.Log("grids1:" + grids.Length);
        for (int i = 0; i < grids.Length; ++i)
        {
            if (grids[i] != null)
            {
                if (grids[i].transform.childCount > 0)
                {
                    grids[i].dice = grids[i].transform.GetChild(0).gameObject;
                    
                }
                dices[i] = grids[i].dice;
                //print(grids[i].dice.num);
            }
        }
        
    }

    //ɾ����ǰ������ѡ�����ڵ�����
    public void clear()
    {
        for (int i = 0; i < dices.Length; ++i)
        {
            Destroy(dices[i].gameObject);
        }
    }

}

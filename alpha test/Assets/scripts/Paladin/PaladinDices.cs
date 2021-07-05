using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//圣骑士骰子盘，负责调用圣骑士攻击方法
public class PaladinDices : MonoBehaviour
{
    public GameObject dice;

    public Grid[] grids = new Grid[5];
    public int rollTimes;
    //骰子的材质
    public Material[] materials = new Material[6];
    private void Start()
    {
        rollTimes = 2;
        grids = GetComponentsInChildren<Grid>();
        for (int i = 0; i < grids.Length; ++i)
        {
            grids[i].dice = grids[i].transform.GetChild(0).gameObject;
        }
        roll();
    }
    
  

    public void roll()
    {
        if (rollTimes == 0)
        {
            //提示重骰次数已经用完
            Debug.Log("重骰次数已经用完");
            return;
        }
        for (int i = 0; i < grids.Length; ++i)
        {

            if (grids[i].dice != null)
            {
                grids[i].randomDiceFace();
                switch (grids[i].num)
                {
                    case 1:
                    case 2:
                    case 3:
                        grids[i].setActionColour(ActionColour.Red);
                        break;
                    case 4:
                    case 5:
                        grids[i].setActionColour(ActionColour.Bule);
                        break;
                    case 6:
                        grids[i].setActionColour(ActionColour.Green);
                        break;
                
                }
                //设置材质
                print(i);
                grids[i].setMaterial(materials[grids[i].num - 1]);

            }

        }
        --rollTimes;
    }

    //棋盘初始化
    public void reset()
    {
        rollTimes = 2;
        for (int i = 0; i < grids.Length; ++i)
        {
            if (grids[i].dice == null)
            {
                GameObject go = GameObject.Instantiate(dice,transform.position,Quaternion.identity);
                go.transform.SetParent(grids[i].transform);
                grids[i].dice = go;
                go.transform.localPosition = Vector3.zero;
                //grids[i].transform.localPosition = Vector2.zero;
            }
        }
        roll();
    }
    public void clear()
    {
        for (int i = 0; i < grids.Length; ++i)
        {
            Destroy(grids[i].dice.gameObject);
        }
    }

}

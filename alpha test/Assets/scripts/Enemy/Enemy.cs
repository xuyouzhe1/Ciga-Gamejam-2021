using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人基类
public abstract class Enemy : MonoBehaviour
{
    public int hp = 0;
    public int amror = 0;
    //下一次行动颜色
    public string actionColour;


    //受到攻击
    public void receiveDamage(int damage)
    {
        if (amror > 0)
        {
            this.amror -= damage;
            if (amror < 0)
            {
                this.hp += amror;
            }
        }
        else
        {
            hp -= damage;
        }
    }

    //获得护甲
    public void getAmror(int amror)
    {
        this.amror += amror;
    }

    //清空护甲
    public void clearAmoror()
    {
        amror = 0;
    }

    //当玩家回合开始时，设置该回合攻击方式
    public abstract void setNextAction();

    //显示下次行动颜色
    public abstract void getActionColour();

    //怪物行动
    public abstract int action(out bool isPosion);


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl _PlayerControl;
    //主角属性
    public int hp = 50;
    public int amror = 0;
    public GameObject btnAttack;
    //动画组件  
    private Animator playerAnimator;


    private void Start()
    {
    }

    

    //承受伤害  
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

    //圣骑士技能:基础
    //斩击
    public int chop()
    {
        //播放动画
        return Random.Range(3, 6);
    }
    //圣光
    public int holyLight()
    {
        return 3;
    }
    //治愈
    public int heal()
    {
        hp += Random.Range(1,4);
        return 0;
    }

    //圣骑士技能:强化
    //纵斩
    public int powfulChop()
    {
        return Random.Range(8, 11);
    }
    //辟邪斩
    public int holyChop()
    {
        return Random.Range(6, 9);
    }
    //血战到底
    public int healChop()
    {
        int damage = Random.Range(6, 9);
        this.hp += damage;
        return damage;
    }

    //圣光爆发 aoe
    public int holyBurst()
    {
        return 12;
    }
    //神圣治愈
    public int holyHeal()
    {
        hp += Random.Range(3, 7);
        return 0;
    }

    //祝福术
    public int blessing()
    {
        getAmror(10);
        return 0;
    }
}
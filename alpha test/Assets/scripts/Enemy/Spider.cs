using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    //动画组件  
    private Animator enemyAnimator;

    void Start()
    {
        hp = 27;
    }


    //玩家鼠标停留时显示下回合状态
    public override void getActionColour()
    {

    }

    //玩家回合开始时初始化调用
    public override void setNextAction()
    {
        print("enemyAction");
        int probability = Random.Range(1, 101);
        if (probability >= 1 && probability <= 50)
        {
            actionColour = ActionColour.Red;
        }
        else if (probability > 50 && probability <= 51)
        {
            actionColour = ActionColour.Bule;
        }
        else
        {
            actionColour = ActionColour.Green;
        }
    }

    //敌人行动
    public override int action(out bool isPoison)
    {
        isPoison = false;
        switch (actionColour)
        {
            case ActionColour.Red:
                return bravery();
            case ActionColour.Bule:
                return wisdom();
            case ActionColour.Green:
                isPoison = true;
                return trick();
        }
        return -1;
    }


    //勇壮
    public int bravery()
    {
        //调用攻击动画
        return Random.Range(6, 13);
    }

    //智谋
    public int wisdom()
    {
        //动画
        this.amror += 8;
        return 0;
    }

    //诡计
    public int trick()
    {
        //anim
        return Random.Range(1, 4);
    }

}
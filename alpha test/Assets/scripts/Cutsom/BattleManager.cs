using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //回合控制脚本
    private TurnControl turnScript;

    //获取脚本的引用 
    private Spider enemyScript;
    private PlayerControl playerScrpit;
    private ChoiseCard choiseCard;
    private PaladinDices dicesScrpit;


    private bool isSetEnemyAction = false;

    //玩家是否中毒
    private bool isPosion = false;
    //中毒剩余回合数
    private int posionCount = 0;
    //玩家是否处于祝福状态
    private bool isBless = false;
    //祝福剩余回合数
    private int blessCount = 0;

    void Start()
    {
        turnScript = GameObject.Find("TurnSystem").GetComponent<TurnControl>();
        //playerAnimator = GetComponent<Animator>();
        enemyScript = GameObject.FindGameObjectWithTag("EnemyUnit").GetComponent<Spider>();
        playerScrpit = GameObject.FindGameObjectWithTag("PlayerUnit").GetComponent<PlayerControl>();
        choiseCard = GameObject.FindGameObjectWithTag("ChoicesCard").GetComponent<ChoiseCard>();
        dicesScrpit = GameObject.FindGameObjectWithTag("DicesUnit").GetComponent<PaladinDices>();
    }

    void Update()
    {

        if (turnScript.currentState == TurnControl.GameState.PlayerTurn)
        {
            //Debug.Log("玩家回合");
            if (!isSetEnemyAction)
            {
                enemyScript.setNextAction();

                //清空玩家护甲
                playerScrpit.clearAmoror();
                isSetEnemyAction = true;
            }
            
            //GUI.Window(1, new Rect(Screen.width / 2 + 200, Screen.height / 2, 100, 100), PlayerUnitAction, "选择技能");
            //显示出牌界面
            GameObject parentObj = GameObject.Find("UIRoot");
            GameObject child = parentObj.transform.Find("choices").gameObject;
            child.SetActive(true);

           
        }
        if (turnScript.currentState == TurnControl.GameState.EnemyTurn)
        {
            //清空敌人护甲
            enemyScript.clearAmoror();
            int damage = enemyScript.action(out isPosion);
            if (isPosion)
            {
                posionCount = 3;
                isPosion = false;
            }
            //给与玩家伤害
            playerScrpit.receiveDamage(damage);
            //延迟1s出现玩家操作UI
            isSetEnemyAction = false;
            StartCoroutine("PlayerUIWait");
            turnScript.currentState = TurnControl.GameState.PlayerTurn;
            //判断双方hp游戏是否结束
            hp();
            //初始化骰子盘
            dicesScrpit.reset();
        }

        if (turnScript.currentState == TurnControl.GameState.Win)
        { 
            
        }

        if (turnScript.currentState == TurnControl.GameState.Lose)
        {

        }


  
        
    }

    public void playerTurnEnd()
    {
        if (turnScript.currentState == TurnControl.GameState.PlayerTurn)
        {
            turnScript.currentState = TurnControl.GameState.EnemyTurn;
        }
        //判断玩家buff
        if (posionCount > 0)
        {
            posion();
        }
        if (blessCount > 0)
        {
            bless();
        }
    }

    public void playerCommit()
    {
        int damage  = playerAction(out isBless);
        if (isBless)
        {
            blessCount = 3;
            isBless = false;
        }
        enemyScript.receiveDamage(damage);

        //判断双方hp游戏是否结束
        hp();
        //删除选择盘内的骰子
        choiseCard.clear();
    }

    private void hp()
    {
        if (playerScrpit.hp <= 0)
        {
            turnScript.currentState = TurnControl.GameState.Lose;
        }
        else if (enemyScript.hp <= 0)
        {
            turnScript.currentState = TurnControl.GameState.Win;

        }
    }

    //玩家提交数据，玩家行动执行
    public int playerAction(out bool isBless)
    {
        isBless = false;
        string[] colours = new string[3];
        int damage = 0;
        for (int i = 0; i < choiseCard.dices.Length; ++i)
        {
            if (choiseCard.dices[i] != null)
            {
                colours[i] = choiseCard.grids[i].actionColour;
            }
        }
        //判断序列顺序决定玩家选择的技能
        switch (colours[0])
        {
            case ActionColour.Red:
                switch (colours[1])
                {
                    case ActionColour.Red:
                        //纵斩
                        damage = playerScrpit.powfulChop();
                        break;
                    case ActionColour.Bule:
                        //辟邪斩
                        damage = playerScrpit.holyChop();
                        if (enemyScript.actionColour == ActionColour.Green)
                        {
                            damage *= 2;
                        }
                        break;
                    case ActionColour.Green:
                        //血战到底
                        damage = playerScrpit.healChop();
                        break;
                    default :
                        //斩击
                        damage = playerScrpit.chop();
                        break;
                }
                break;
            case ActionColour.Bule:
                if (colours[1] == ActionColour.Bule && colours[2] == ActionColour.Bule)
                {
                    //神圣爆发 aoe需要做敌人列表遍历造成伤害，目前就一个所以摸了
                    damage = playerScrpit.holyBurst();
                }
                else if (colours[1] == ActionColour.Green)
                {
                    //神圣治愈
                    damage = playerScrpit.holyHeal();
                    //移除中毒状态
                    if (posionCount > 0)
                    {
                        posionCount = 0;
                    }
                }
                else
                {
                    //圣光
                    damage = playerScrpit.holyLight();
                }
                break;
            case ActionColour.Green:
                if (colours[1] == ActionColour.Bule && colours[2] == ActionColour.Red)
                {
                    //祝福术
                    isBless = true;
                    damage = playerScrpit.blessing();
                }
                else
                {
                    //治愈
                    damage = playerScrpit.heal();
                }
                break;
        }
       
        return damage;
    }

    //游戏结束


    //祝福状态
    private void bless()
    {
        if (blessCount <= 0) return;
        //要播放动画就在这里嗷
        playerScrpit.hp += 3;
        blessCount--;
    }

    private void posion()
    {
        if (posionCount <= 0) return;
        playerScrpit.hp -= 3;
        posionCount--;
    }

    //在切换到敌人操作前添加延迟
    IEnumerator EnemyWait()
    {
        yield return new WaitForSeconds(1);
        turnScript.isEnemyAction = true;
    }

    IEnumerator PlayerUIWait()
    {
        yield return new WaitForSeconds(1);
        turnScript.isWaitForPlayer = true;
    }

    
    
}

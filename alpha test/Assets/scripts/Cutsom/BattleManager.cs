using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //�غϿ��ƽű�
    private TurnControl turnScript;

    //��ȡ�ű������� 
    private Spider enemyScript;
    private PlayerControl playerScrpit;
    private ChoiseCard choiseCard;
    private PaladinDices dicesScrpit;


    private bool isSetEnemyAction = false;

    //����Ƿ��ж�
    private bool isPosion = false;
    //�ж�ʣ��غ���
    private int posionCount = 0;
    //����Ƿ���ף��״̬
    private bool isBless = false;
    //ף��ʣ��غ���
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
            //Debug.Log("��һغ�");
            if (!isSetEnemyAction)
            {
                enemyScript.setNextAction();

                //�����һ���
                playerScrpit.clearAmoror();
                isSetEnemyAction = true;
            }
            
            //GUI.Window(1, new Rect(Screen.width / 2 + 200, Screen.height / 2, 100, 100), PlayerUnitAction, "ѡ����");
            //��ʾ���ƽ���
            GameObject parentObj = GameObject.Find("UIRoot");
            GameObject child = parentObj.transform.Find("choices").gameObject;
            child.SetActive(true);

           
        }
        if (turnScript.currentState == TurnControl.GameState.EnemyTurn)
        {
            //��յ��˻���
            enemyScript.clearAmoror();
            int damage = enemyScript.action(out isPosion);
            if (isPosion)
            {
                posionCount = 3;
                isPosion = false;
            }
            //��������˺�
            playerScrpit.receiveDamage(damage);
            //�ӳ�1s������Ҳ���UI
            isSetEnemyAction = false;
            StartCoroutine("PlayerUIWait");
            turnScript.currentState = TurnControl.GameState.PlayerTurn;
            //�ж�˫��hp��Ϸ�Ƿ����
            hp();
            //��ʼ��������
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
        //�ж����buff
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

        //�ж�˫��hp��Ϸ�Ƿ����
        hp();
        //ɾ��ѡ�����ڵ�����
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

    //����ύ���ݣ�����ж�ִ��
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
        //�ж�����˳��������ѡ��ļ���
        switch (colours[0])
        {
            case ActionColour.Red:
                switch (colours[1])
                {
                    case ActionColour.Red:
                        //��ն
                        damage = playerScrpit.powfulChop();
                        break;
                    case ActionColour.Bule:
                        //��аն
                        damage = playerScrpit.holyChop();
                        if (enemyScript.actionColour == ActionColour.Green)
                        {
                            damage *= 2;
                        }
                        break;
                    case ActionColour.Green:
                        //Ѫս����
                        damage = playerScrpit.healChop();
                        break;
                    default :
                        //ն��
                        damage = playerScrpit.chop();
                        break;
                }
                break;
            case ActionColour.Bule:
                if (colours[1] == ActionColour.Bule && colours[2] == ActionColour.Bule)
                {
                    //��ʥ���� aoe��Ҫ�������б��������˺���Ŀǰ��һ����������
                    damage = playerScrpit.holyBurst();
                }
                else if (colours[1] == ActionColour.Green)
                {
                    //��ʥ����
                    damage = playerScrpit.holyHeal();
                    //�Ƴ��ж�״̬
                    if (posionCount > 0)
                    {
                        posionCount = 0;
                    }
                }
                else
                {
                    //ʥ��
                    damage = playerScrpit.holyLight();
                }
                break;
            case ActionColour.Green:
                if (colours[1] == ActionColour.Bule && colours[2] == ActionColour.Red)
                {
                    //ף����
                    isBless = true;
                    damage = playerScrpit.blessing();
                }
                else
                {
                    //����
                    damage = playerScrpit.heal();
                }
                break;
        }
       
        return damage;
    }

    //��Ϸ����


    //ף��״̬
    private void bless()
    {
        if (blessCount <= 0) return;
        //Ҫ���Ŷ������������
        playerScrpit.hp += 3;
        blessCount--;
    }

    private void posion()
    {
        if (posionCount <= 0) return;
        playerScrpit.hp -= 3;
        posionCount--;
    }

    //���л������˲���ǰ����ӳ�
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

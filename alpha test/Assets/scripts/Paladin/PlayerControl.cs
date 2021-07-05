using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl _PlayerControl;
    //��������
    public int hp = 50;
    public int amror = 0;
    public GameObject btnAttack;
    //�������  
    private Animator playerAnimator;


    private void Start()
    {
    }

    

    //�����˺�  
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
    //��û���
    public void getAmror(int amror)
    {
        this.amror += amror;
    }

    //��ջ���
    public void clearAmoror()
    {
        amror = 0;
    }

    //ʥ��ʿ����:����
    //ն��
    public int chop()
    {
        //���Ŷ���
        return Random.Range(3, 6);
    }
    //ʥ��
    public int holyLight()
    {
        return 3;
    }
    //����
    public int heal()
    {
        hp += Random.Range(1,4);
        return 0;
    }

    //ʥ��ʿ����:ǿ��
    //��ն
    public int powfulChop()
    {
        return Random.Range(8, 11);
    }
    //��аն
    public int holyChop()
    {
        return Random.Range(6, 9);
    }
    //Ѫս����
    public int healChop()
    {
        int damage = Random.Range(6, 9);
        this.hp += damage;
        return damage;
    }

    //ʥ�ⱬ�� aoe
    public int holyBurst()
    {
        return 12;
    }
    //��ʥ����
    public int holyHeal()
    {
        hp += Random.Range(3, 7);
        return 0;
    }

    //ף����
    public int blessing()
    {
        getAmror(10);
        return 0;
    }
}
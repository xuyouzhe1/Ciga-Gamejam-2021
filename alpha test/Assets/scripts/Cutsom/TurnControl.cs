using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnControl : MonoBehaviour
{
    public static TurnControl _TurnControl;
    //������Ҳ��������������Ƿ����
    public bool isWaitForPlayer = true;
    //���ƹ������
    public bool isEnemyAction = false;


    //������Ϸ״̬ö��  
    public enum GameState
    {
        Menu,//��Ϸ��ʼ�˵�  
        PlayerTurn,//��Ϸ��
        EnemyTurn,
        Win,//���ʤ��
        Lose,//���ʧ��
    }
    
    //��Ϸ��ʼ״̬
    public GameState currentState = GameState.Menu;
    public GameState GetGameState()
    {
        return currentState;
    }
    public void SetGameState(GameState gameState)
    {
        currentState = gameState;
    }
  

    void OnGUI()
    {
        if (currentState == GameState.Menu)
        {
            GUI.Window(0, new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 60), GameStartConfirm, "ս����ʼȷ��");
        }
        else if (currentState == GameState.Win)
        {
            GUI.Window(2, new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 60), GameRestartConfirm, "��Ϸ����");
        }
    }

    void GameStartConfirm(int ID)
    {
        if (GUI.Button(new Rect(50, 30, 100, 20), "��ʼս��"))
        {
            currentState = GameState.PlayerTurn;
        }   
    }
    //���ս��ʧ�ܾͷ������˵�
    void GameRestartConfirm(int ID)
    {
        if (GUI.Button(new Rect(50, 30, 100, 20), "ȷ��"))
        {
            SceneManager.LoadScene("index");
        }
    }

    void Update()
    {
       

    }


}
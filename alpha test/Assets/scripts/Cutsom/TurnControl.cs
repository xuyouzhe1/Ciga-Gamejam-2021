using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnControl : MonoBehaviour
{
    public static TurnControl _TurnControl;
    //控制玩家操作及操作窗口是否出现
    public bool isWaitForPlayer = true;
    //控制怪物操作
    public bool isEnemyAction = false;


    //定义游戏状态枚举  
    public enum GameState
    {
        Menu,//游戏开始菜单  
        PlayerTurn,//游戏中
        EnemyTurn,
        Win,//玩家胜利
        Lose,//玩家失败
    }
    
    //游戏初始状态
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
            GUI.Window(0, new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 60), GameStartConfirm, "战斗开始确认");
        }
        else if (currentState == GameState.Win)
        {
            GUI.Window(2, new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 60), GameRestartConfirm, "游戏结束");
        }
    }

    void GameStartConfirm(int ID)
    {
        if (GUI.Button(new Rect(50, 30, 100, 20), "开始战斗"))
        {
            currentState = GameState.PlayerTurn;
        }   
    }
    //如果战斗失败就返回主菜单
    void GameRestartConfirm(int ID)
    {
        if (GUI.Button(new Rect(50, 30, 100, 20), "确定"))
        {
            SceneManager.LoadScene("index");
        }
    }

    void Update()
    {
       

    }


}
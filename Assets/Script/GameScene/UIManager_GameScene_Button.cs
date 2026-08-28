using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 本类是对GameScene内Button相关逻辑进行设计
/// </summary>
public class UIManager_GameScene_Button : MonoBehaviour
{
    [Header("菜单页面物体")]
    public GameObject menu_Panel;
    [Header("帮助文档物体")]
    public GameObject help_Panel;
    [Header("UIScene的场景名称")]
    public string UISceneName;
    /// <summary>
    /// 关闭/打开 菜单页面
    /// </summary>
    public void Button_OE_MenuPage()
    {
        menu_Panel.SetActive(!menu_Panel.activeSelf);
        //设置游戏状态
        if (menu_Panel.activeSelf)
        {
            //暂停游戏
            TimeManager.Instance.TimeScale = 0;
        }
        else
        {
            //恢复游戏
            TimeManager.Instance.TimeScale = 1;
        }
    }
    /// <summary>
    /// 关闭/打开 帮助文档
    /// </summary>
    public void Button_OE_HelpPage()
    {
        help_Panel.SetActive(!help_Panel.activeSelf);
    }
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void Button_ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    /// <summary>
    /// 返回到第一个场景
    /// </summary>
    public void Button_BackHomePage()
    {
        if (UISceneName == "")
        {
            Funcs.ErrorLog<UIManager_GameScene_Button>("场景名为空!参数名:(UISceneName)");
        }
        else
        {
            try
            {
                //回到主页面，并设置游戏状态为主页面
                AudioManager.Instance.GameStatu = GameStatus.mainPage;
                TimeManager.Instance.TimeScale = 1;//恢复时间伸缩
                SceneManager.LoadScene(UISceneName);
                Funcs.NewLog<UIManager_GameScene_Button>("玩家成功回到主页");
            }
            catch (Exception e)
            {
                Funcs.ErrorLog<UIManager_GameScene_Button>("场景切换错误!\n" + e);
            }
        }
    }
}

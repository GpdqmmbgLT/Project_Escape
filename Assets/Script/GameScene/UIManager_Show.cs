using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 本类是对GameScene场景中玩家可视UI效果进行设计（血条，屏幕光效等）
/// </summary>
public class UIManager_Show : MonoBehaviour
{
    public static Action<GameStatus> Action_ChangeScreenShow;//静态委托，传递屏幕光效事件
    public static Action<float> Action_ChangeBloodSlider;//静态委托，传递玩家血条与显示事件
    [Header("游戏主面板的Image组件")]
    public Image mianPage_Img;
    [Header("游戏主面板背景光效的精灵图片,顺序<白天,黑夜>")]
    public Sprite[] screenBackgroundImgs;
    [Header("游戏主面板背景光效颜色,顺序<白天,黑夜>")]
    public Color[] screenBackgroundsColors;
    [Header("玩家血条的Slider组件")]
    public Slider playerBlood_Slider;
    [Header("玩家的血条显示文本组件")]
    public TextMeshProUGUI playerBlood_Text;
    void Awake()
    {
        //把外观显示的功能注册进静态委托中
        Action_ChangeScreenShow -= ChangeScreenShow;
        Action_ChangeScreenShow += ChangeScreenShow;
        //把血条显示与文本改变的事件注册进静态委托中
        Action_ChangeBloodSlider -= ChangeBloodSlider;
        Action_ChangeBloodSlider += ChangeBloodSlider;
    }
    // Start is called before the first frame update
    void Start()
    {
        //初始化血条与文本显示
        ChangeBloodSlider(DataManager_Player.Instance.PlayerBlood);
    }
    void OnDisable()
    {
        Action_ChangeScreenShow -= ChangeScreenShow;
        Action_ChangeBloodSlider -= ChangeBloodSlider;
    }

    public void ChangeBloodSlider(float playerBlood)
    {
        playerBlood_Slider.value = playerBlood / 100f;
        playerBlood_Text.text = $"{(int)playerBlood}/100";
    }
    /// <summary>
    /// 根据当前游戏状态改编主页面版的外观显示
    /// </summary>
    /// <param name="status"></param>
    public void ChangeScreenShow(GameStatus status)
    {
        if (status == GameStatus.game_DayTime)
        {
            mianPage_Img.sprite = screenBackgroundImgs[0];
            mianPage_Img.color = screenBackgroundsColors[0];
        }
        else if (status == GameStatus.game_Night)
        {
            mianPage_Img.sprite = screenBackgroundImgs[1];
            mianPage_Img.color = screenBackgroundsColors[1];
        }
    }
}

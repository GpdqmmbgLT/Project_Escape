using System;
using TMPro;
using UnityEngine;
/// <summary>
/// 本类是对GameScene场景的Text文本显示进行设计
/// </summary>
public class UIManager_GameScene_Text : MonoBehaviour
{
  public static Action<GameStatus> StatusText_Ac;//静态委托
  [Header("展示时间的文本组件")]
  public TextMeshProUGUI timeShow_Text;
  [Header("展示游戏状态的文本组件")]
  public TextMeshProUGUI gameStatus_Text;
  private float timer;//计时器
  void Awake()
  {
    //将游戏状态改变的文本传递进委托事件
    StatusText_Ac -= ChangeGameStatus_Text;
    StatusText_Ac += ChangeGameStatus_Text;
  }
  void Update()
  {
    timer += Time.deltaTime;
    //每0.5秒更新一次时间文本的显示
    if (timer >= 0.5f)
    {
      timeShow_Text.text = TimeManager.Instance.ReturnTime();
    }
  }
  void OnDisable()
  {
    StatusText_Ac -= ChangeGameStatus_Text;
  }
  /// <summary>
  /// 改变游戏状态的文本
  /// </summary>
  /// <param name="status"></param>
  public void ChangeGameStatus_Text(GameStatus status)
  {
    if (status == GameStatus.game_DayTime)
    {
      gameStatus_Text.text = "DayTime";
    }
    else if (status == GameStatus.game_Night)
    {
      gameStatus_Text.text = "Night";
    }
  }
}
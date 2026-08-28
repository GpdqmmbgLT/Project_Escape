using System;
using UnityEngine;
/// <summary>
/// 本单例类管理游戏进程的时间
/// </summary>
public class TimeManager : MonoBehaviour
{
  private static TimeManager instance;//唯一实例
  public static TimeManager Instance
  {
    get
    {
      if (instance != null)
      {
        return instance;
      }
      Funcs.ErrorLog<TimeManager>("实例:instance为空!");
      return null;
    }
  }
  private float gameTimer;//游戏计时器
  public float GameTimer
  {
    get
    {
      return gameTimer;
    }
    set
    {
      gameTimer = value;
      //分别对分,秒赋值(时就不用了,游戏到不了这么久)
      second = gameTimer % 60;
      minute = gameTimer / 60;
    }
  }
  private float timeScale;//时间伸缩值
  public float TimeScale
  {
    get
    {
      return timeScale;
    }
    set
    {
      timeScale = Mathf.Clamp01(value);
      if (timeScale == 1)
      {
        Funcs.NewLog<TimeManager>($"时间恢复  伸缩值:{timeScale}");
      }
      else if (timeScale == 0)
      {
        Funcs.NewLog<TimeManager>($"时间暂停  伸缩值:{timeScale}");
      }
      else
      {
        Funcs.NewLog<TimeManager>($"时间减速  伸缩值:{timeScale}");
      }
      Time.timeScale = timeScale;
    }
  }
  private float second, minute, hour = 0;//秒,分，时

  void Awake()
  {
    instance = this;
  }
  void Start()
  {
    //由于场景切换与生命周期函数执行顺序可能导致还没来得及订阅事件就触发，在此处再次进行游戏状态的改变
    if (AudioManager.Instance != null)
    {
      AudioManager.Instance.GameStatu = GameStatus.game_DayTime;
    }
  }
  void Update()
  {
    //累加计时器
    GameTimer += Time.deltaTime;
  }
  /// <summary>
  /// 返回由时分秒构成的字符串
  /// </summary>
  /// <returns></returns>
  public string ReturnTime()
  {
    return $"{(int)hour}:{(int)minute}:{(int)second}";
  }
}
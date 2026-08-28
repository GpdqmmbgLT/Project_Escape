using System;
using UnityEngine;
/// <summary>
/// 本类是对GameScene场景中环境物体的相关逻辑进行设计
/// </summary>
public class LogicManager_Environment : MonoBehaviour
{
  public static Action Action_Victory;//传递胜利后触发的事件
  public static Action Action_Defeat;//传递失败后触发的事件
  [Header("箱子物体的盖子")]
  public GameObject box_Lid;
  [Header("主光源物体")]
  public GameObject mainLight;
  [Header("主光源Light组件")]
  public Light main_Light;
  [Header("玩家身上的光源组件")]
  public Light player_Light;
  [Header("胜利页面物体")]
  public GameObject victoryPage;
  [Header("失败页面物体")]
  public GameObject defeatPage;
  float _lidRotateAllAngle, _lightRotateAllAngle;//盖子和阳光各自需要旋转的总角度
  Vector3 _rotateDirection;//旋转的方向
  float _lidRotateSum, _ligthRotateSum;//盖子和太阳的累积旋转角度
  ParticleSystem[] particles;//场景中的所有粒子系统
  Light[] lights;//场景中所有的光源
  void Awake()
  {
    //分别注册成功和失败事件
    Action_Victory -= OpenVictoryPage;
    Action_Victory += OpenVictoryPage;
    Action_Defeat -= OpenDefeatPage;
    Action_Defeat += OpenDefeatPage;
  }
  void Start()
  {
    _rotateDirection = new Vector3(1, 0, 0);
    //初始化盖子和光源的初始旋转角度
    box_Lid.transform.localRotation = Quaternion.Euler(new Vector3(DataManager_Environment.lidStartRotateRangel, 0, 0));
    mainLight.transform.localRotation = Quaternion.Euler(new Vector3(DataManager_Environment.lightStartRotateRangel, 0, 0));
    //分别计算盖子和阳光需要转动多少角度
    _lidRotateAllAngle = DataManager_Environment.lidEndRotateRangel - DataManager_Environment.lidStartRotateRangel;
    _lightRotateAllAngle = DataManager_Environment.lightEndRotateRangel - DataManager_Environment.lightStartRotateRangel;
    particles = FindObjectsOfType<ParticleSystem>();//寻找所有的粒子系统组件
    lights = FindObjectsOfType<Light>();//寻找所有的光源组件
    //遍历所有粒子系统，清除已经播放的粒子并暂停播放
    foreach (var item in particles)
    {
      item.Clear();
      item.Stop();
    }
    //遍历所有灯光组件并禁用
    foreach (var item in lights)
    {
      item.enabled = false;
    }
    //恢复主光源和玩家光源
    main_Light.enabled = true;
    player_Light.enabled = true;
  }
  void Update()
  {
    //记录两者本次旋转值
    float lidRotateValue = (_lidRotateAllAngle / DataManager_Environment.lightDayTime) * Time.deltaTime;
    float lightRotateValue = (_lightRotateAllAngle / DataManager_Environment.lightDayTime) * Time.deltaTime;
    //盖子和光源开始旋转,直到到达对应的最大角度
    box_Lid.transform.Rotate(lidRotateValue * _rotateDirection);
    mainLight.transform.Rotate(lightRotateValue * _rotateDirection);
    //累计旋转值
    _lidRotateSum += lidRotateValue;
    _ligthRotateSum += lightRotateValue;
    //如果两个物体的累积旋转角度都达到了最大角度就结束，并改变当时的天空状态为黑夜，并开启所有粒子系统与灯光
    if (_lidRotateSum >= _lidRotateAllAngle && _ligthRotateSum >= _lightRotateAllAngle)
    {
      //遍历所有粒子系统并播放
      foreach (var item in particles)
      {
        item.Play();
      }
      //遍历所有灯光组件并恢复
      foreach (var item in lights)
      {
        item.enabled = true;
      }
      AudioManager.Instance.GameStatu = GameStatus.game_Night;
      gameObject.SetActive(false);
    }
  }
  void OnDisable()
  {
    Funcs.NewLog<LogicManager_Environment>("盖子/太阳旋转完毕,脚本关闭");
  }
  void OnDestroy()
  {
    Action_Victory -= OpenVictoryPage;
    Action_Defeat -= OpenDefeatPage;
  }
  /// <summary>
  /// 打开成功页面并暂停时间
  /// </summary>
  public void OpenVictoryPage()
  {
    TimeManager.Instance.TimeScale = 0;
    victoryPage.SetActive(true);
    Funcs.NewLog<LogicManager_Environment>("玩家成功通关");
  }
  /// <summary>
  /// 打开失败页面并暂停时间
  /// </summary>
  public void OpenDefeatPage()
  {
    TimeManager.Instance.TimeScale = 0;
    defeatPage.SetActive(true);
    Funcs.NewLog<LogicManager_Environment>("玩家游戏失败");

  }
}
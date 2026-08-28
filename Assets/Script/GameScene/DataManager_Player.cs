using System;
using UnityEngine;
/// <summary>
/// 本类存储玩家相关信息
/// </summary>
public class DataManager_Player : MonoBehaviour
{
  private static DataManager_Player instance;
  public static DataManager_Player Instance
  {
    get
    {
      if (instance != null)
      {
        return instance;
      }
      Funcs.ErrorLog<DataManager_Player>("实例instance为空!");
      return null;
    }
  }
  private const float maxPlayerBlood = 100;//玩家最大血量常量
  private const float minPlayerBlood = 0;//玩家最小血量常量
  private const float playerDamage = 0.65f;//玩家每秒扣血速度
  public const float maxLookingUp = 70;//玩家最大仰视角度
  public const float maxLookingDown = -60;//玩家最大俯视角度
  public const float moveSpeed = 6.9f;//玩家的移动速度
  public const float rotateSpeed_Horizontal = 240f;//水平旋转速度
  public const float rotateSpeed_Vertical = 180f;//竖直旋转速度
  private Vector3 playerBeginPo = new Vector3(487, 2, 337);//玩家的初始诞生位置
  public Vector3 PlayerBeginPo
  {
    get
    {
      return playerBeginPo;
    }
  }
  private bool playerLock;//标记玩家是否可操作
  public bool PlayerLock
  {
    get
    {
      return playerLock;
    }
    set
    {
      playerLock = value;
    }
  }
  private float playerBlood = 100;//玩家的血量与基础值
  public float PlayerBlood
  {
    get
    {
      return playerBlood;
    }
    set
    {
      //将血量控制在范围内
      playerBlood = Mathf.Clamp(value, minPlayerBlood, maxPlayerBlood);
      //触发血条与文本更新事件
      UIManager_Show.Action_ChangeBloodSlider?.Invoke(playerBlood);
      //如果玩家血量到0则触发失败事件
      if (playerBlood == 0)
      {
        LogicManager_Environment.Action_Defeat?.Invoke();
      }
    }
  }
  void Awake()
  {
    instance = this;
  }
  /// <summary>
  /// 玩家受伤功能
  /// </summary>
  public void Damage()
  {
    //每秒流逝playerDamage点伤害
    PlayerBlood -= playerDamage * Time.deltaTime;
  }
}
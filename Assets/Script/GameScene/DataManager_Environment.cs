using UnityEngine;
/// <summary>
/// 本单例类是对GameScene场景sence游戏中环境物体(宝箱本体，灯光）的数据进行存储设计
/// </summary>
public class DataManager_Environment : MonoBehaviour
{
  private static DataManager_Environment instance;
  public static DataManager_Environment Instance
  {
    get
    {
      if (instance != null)
      {
        return instance;
      }
      Funcs.ErrorLog<DataManager_Environment>("实例instance为空!");
      return null;
    }
  }
  public const float lidStartRotateRangel = -180f;//宝箱盖子初始旋转角度（关闭的状态）
  public const float lidEndRotateRangel = -90f;//宝箱盖子最终旋转角度（打开的状态）
  public const float lightEndRotateRangel = 230f;//太阳光最终旋转角度（黑夜）
  public const float lightStartRotateRangel = 90f;//太阳光初始旋转角度（白天）
  public const float lightDayTime = 60f;//白天时长
  void Awake()
  {
    instance = this;
  }
}
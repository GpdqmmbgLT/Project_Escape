using System;
using UnityEngine;
/// <summary>
/// 本类是对玩家的相关逻辑进行设计（移动，旋转，伤害）
/// </summary>
public class LogicManager_Player : MonoBehaviour
{
  [Header("挂在在玩家身上的相机物体")]
  public GameObject playerCamera;
  float vertiAngle;//摄像机的水平旋转角度值
  void Start()
  {
    //初始化玩家位置
    gameObject.transform.position = DataManager_Player.Instance.PlayerBeginPo;
  }
  void Update()
  {
    //如果玩家被锁住则禁用所有操作
    if (DataManager_Player.Instance.PlayerLock)
    {
      return;
    }
    //获取键盘水平/竖直偏移，进行移动操作
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    PlayerMove(new Vector3(horizontal, 0, vertical));
    //获取鼠标偏移，进行旋转操作
    float horiMouse = Input.GetAxis("Mouse X");
    float vertiMouse = Input.GetAxis("Mouse Y");
    PlayerRotate(horiMouse, vertiMouse);
    //如果目前游戏状态是黑夜并且玩家血量不为0则开始造成伤害
    if (AudioManager.Instance != null && AudioManager.Instance.GameStatu == GameStatus.game_Night && DataManager_Player.Instance.PlayerBlood != 0)
    {
      DataManager_Player.Instance.Damage();
    }
  }
  /// <summary>
  /// 玩家的移动逻辑
  /// </summary>
  /// <param name="direction">移动方向</param>
  void PlayerMove(Vector3 direction)
  {
    transform.Translate(DataManager_Player.moveSpeed * Time.deltaTime * direction);
  }
  void PlayerRotate(float horiMouse, float vertiMouse)
  {
    //玩家本体饶Y旋转，模拟转身（累加增量）
    transform.Rotate(new Vector3(0, horiMouse, 0) * Time.deltaTime * DataManager_Player.rotateSpeed_Horizontal);
    //摄像机饶X旋转，模拟抬头低头（直接修改旋转）
    vertiAngle -= DataManager_Player.rotateSpeed_Vertical * Time.deltaTime * vertiMouse;//累加旋转增量
    vertiAngle = Mathf.Clamp(vertiAngle, DataManager_Player.maxLookingDown, DataManager_Player.maxLookingUp);//将旋转值控制在范围内
    Vector3 cameraRotation = new Vector3(vertiAngle, 0, 0);
    playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation);
  }
}
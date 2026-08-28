using UnityEngine;
/// <summary>
/// 本类是GameScene场景对最终胜利条件-木门的逻辑设计
/// </summary>
public class LogicManager_Door : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    //如果玩家进入触发器范围则触发成功事件
    if (other.gameObject.tag == "Player")
    {
      LogicManager_Environment.Action_Victory?.Invoke();
    }
  }
}
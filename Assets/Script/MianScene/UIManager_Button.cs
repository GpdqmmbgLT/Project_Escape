using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 本类统一管理MianScene的按钮绑定事件
/// </summary>
public class UIManager_Button : MonoBehaviour
{
    [Header("主界面物体")]
    public GameObject main_Panel;
    [Header("设置界面物体")]
    public GameObject setting_Panel;
    [Header("加载界面物体")]
    public GameObject loading_Panel;
    [Header("加载滑动条组件")]
    public Slider loading_Slider;
    [Header("加载时候的text组件")]
    public TextMeshProUGUI loading_Text;
    [Header("进入游戏按钮物体")]
    public GameObject enterGameButton;
    [Header("进入游戏按钮")]
    public Button enterGame_Button;
    [Header("帮助文档物体")]
    public GameObject help_Panel;
    [Header("游戏场景的场景名称")]
    public string game_Scene;
    [Header("主页面面板的粒子系统组件")]
    public ParticleSystem particle;
    /// <summary>
    /// 打开/关闭 帮助文档页面,并在打开/关闭文档的时候 暂停/播放粒子系统
    /// </summary>
    public void Button_OE_HelpPage()
    {
        help_Panel.SetActive(!help_Panel.activeSelf);
        if (particle.isPlaying)
        {
            particle.Stop();
            particle.Clear();
        }
        else
        {
            particle.Play();
        }
    }
    /// <summary>
    /// 打开/关闭 主页面
    /// </summary>
    public void Button_OE_MianPage()
    {
        main_Panel.SetActive(!main_Panel.activeSelf);
    }
    /// <summary>
    /// 打开/关闭 设置界面
    /// </summary>
    public void Button_OE_SettingPage()
    {
        setting_Panel.SetActive(!setting_Panel.activeSelf);
    }
    /// <summary>
    /// 退出游戏（退出编辑模式/退出整个进程）
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
    /// 开启协程加载场景
    /// </summary>
    public void Button_EnterGame()
    {
        StartCoroutine(EnterGame());
    }
    /// <summary>
    /// 打开场景的协程（进度条显示）
    /// </summary>
    /// <returns></returns>
    IEnumerator EnterGame()
    {
        if (game_Scene != null && game_Scene != "")
        {
            //初始化操作——关闭主页面，改变文字显示
            main_Panel.SetActive(false);
            loading_Text.text = "Loading..";
            loading_Panel.SetActive(true);
            //不管加载进度如何都默认先加载2秒钟
            yield return new WaitForSecondsRealtime(2);

            //开始进行异步加载，加载未完成之前持续更新滑动条的值
            AsyncOperation loading = SceneManager.LoadSceneAsync(game_Scene);
            loading.allowSceneActivation = false;
            while (loading.progress < 0.9f)
            {
                loading_Slider.value = loading.progress + 0.1f;
                yield return null;
            }

            //加载完成后添加点击事件并解锁进入按钮，并更新文本显示
            loading_Slider.value = loading.progress + 0.1f;
            loading_Text.text = "Loading Success";
            enterGame_Button.onClick.RemoveAllListeners();
            enterGame_Button.onClick.AddListener(
                () =>
                {
                    //允许切换场景，改变游戏状态为游戏白天
                    loading.allowSceneActivation = true;
                    AudioManager.Instance.GameStatu = GameStatus.game_DayTime;
                    Funcs.NewLog<UIManager_Button>("玩家成功进入游戏");
                }
                );
            enterGameButton.SetActive(true);
            Funcs.NewLog<UIManager_Button>("场景加载成功!");
        }
        else
        {
            Funcs.NewLog<UIManager_Button>("game_Scene参数未赋值或为空!请检查后重试");
            yield break;
        }
    }
}

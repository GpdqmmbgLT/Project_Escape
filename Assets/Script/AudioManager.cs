using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 记录当前游戏状态
/// </summary>
public enum GameStatus
{
    mainPage = 1,
    game_DayTime = 2,
    game_Night = 3
}
/// <summary>
/// 本单例类管理整个游戏的音频切换逻辑(永远保留)
/// </summary>
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;//唯一实例
    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }
    private GameStatus gameStatu;//记录当前的游戏状态
    public GameStatus GameStatu
    {
        get
        {
            return gameStatu;
        }
        set
        {
            //当改变游戏状态时检测音频切换
            gameStatu = value;
            ChangeVoice(gameStatu);
            //来自于游戏场景的委托，触发对应的文本更新功能
            UIManager_GameScene_Text.StatusText_Ac?.Invoke(gameStatu);
            //来自于游戏场景的委托，触发游戏主页面的外观显示
            UIManager_Show.Action_ChangeScreenShow?.Invoke(gameStatu);
        }
    }
    [Header("音频源组件")]
    public AudioSource source;
    [Header("音频列表,顺序<主页面,游戏白天,游戏黑夜>")]
    public AudioClip[] audioClips;
    [Header("不被销毁的总父物体")]
    public GameObject notDestroyFather;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(notDestroyFather);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //初始化游戏状态为主页面
        GameStatu = GameStatus.mainPage;
    }

    /// <summary>
    /// 根据当前的游戏状态切换对应的音频
    /// </summary>
    private void ChangeVoice(GameStatus status)
    {
        Funcs.NewLog<AudioManager>("当前游戏状态:" + status);
        switch (status)
        {
            case GameStatus.mainPage:
                source.clip = audioClips[0];
                source.Play();
                break;
            case GameStatus.game_DayTime:
                source.clip = audioClips[1];
                source.Play();
                break;
            case GameStatus.game_Night:
                source.clip = audioClips[2];
                source.Play();
                break;
        }
    }
}

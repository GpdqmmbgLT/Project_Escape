using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 本类封装部分静态功能奶=为外界调用
/// </summary>
public static class Funcs
{
    /// <summary>
    /// 封装输出日志 格式:[class][date][log]
    /// </summary>
    /// <typeparam name="T">类</typeparam>
    /// <param name="text">要封装输出的文本</param>
    public static void NewLog<T>(string text) where T : class
    {
        Debug.Log($"[{typeof(T).Name}.Log] [{DateTime.Now}] [{text}]");
    }
    /// <summary>
    /// 封装输出异常日志 格式:[Error][class][date][log]
    /// </summary>
    /// <typeparam name="T">类</typeparam>
    /// <param name="text">要封装输出的异常文本</param>
    public static void ErrorLog<T>(string text) where T : class
    {
        Debug.Log($"<color=red>[Error!!][{typeof(T).Name}.Log] [{DateTime.Now}] [{text}]</color>");
    }
}

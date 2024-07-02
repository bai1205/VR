using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using XueXi;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private SceneLoader sceneLoader;
    private string SceneName;
    public List<int> useSkill = new List<int>();
    public List<int> xuanLianSkill = new List<int>();
    public void Awake()//游戏开始入口
    {
        Instance = this;
        GameObject.DontDestroyOnLoad(this);
        WindowManager.Instance.Init();
       ConfigManager.Instance.Init();
        Save.Instance.Init();
        AudioManager.Instance.PlayAudio("Sound/背景音乐", AudioType.bg);

        SceneManager.LoadScene("Start");
        WindowManager.Instance.OpenWindow(WindowType.StartWindow);
    }

    public void Update()
    {
    }
    internal void SceneLoad(string v)
    {
        if (SceneName!="")
        {
            SceneManager.UnloadScene(SceneName);
        }
        SceneName= v;
        SceneManager.LoadScene(v,LoadSceneMode.Additive);
    }
}
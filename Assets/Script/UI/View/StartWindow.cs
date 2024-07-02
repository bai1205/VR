using DG.Tweening;
using Game.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartWindow : BaseWindow
{
    public StartWindow()
    {
        resName = "Prefab/UI/StartWindow";
        name = "StartWindow";
        resident = false ;
        selfType = WindowType.StartWindow;
        scenesType = ScenesType.None;
    }
    protected override void Awake()
    {
        base.Awake();

        foreach (var item in buttonList)
        {
            switch (item.name)
            {
                case "StartButton":
                    item.onClick.AddListener(() =>  //开始页面
                    {
                       
                        WindowManager.Instance.CloseWindow(WindowType.StartWindow);

                        SceneManager.LoadScene("1");
                        WindowManager.Instance.OpenWindow(WindowType.MainWindow);

                    });
                    break;
                case "SetButton":
                    item.onClick.AddListener(() =>
                    {
                        WindowManager.Instance.OpenWindow(WindowType.SettingWindow);
                    });
                    break;
                case "LevelButton":
                    item.onClick.AddListener(() =>
                    {

                        WindowManager.Instance.OpenWindow(WindowType.LevelWindow);
                    });
                    break;
                case "SkillButton":  //继续按钮
                    item.onClick.AddListener(() =>
                    {
                        WindowManager.Instance.OpenWindow(WindowType.SkillWindow);
                    });
                    break;
                case "QuitButton":  //退出按钮
                    item.onClick.AddListener(() =>
                    {
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
                    });
                    break;
            }
        }
    }
}

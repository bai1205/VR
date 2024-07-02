using Game.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionalWindow : BaseWindow
{
    public FunctionalWindow()
    {
        resName = "Prefab/UI/FunctionalWindow";
        name = "FunctionalWindow";
        resident = false;
        selfType = WindowType.FunctionalWindow;
        scenesType = ScenesType.None;
    }
    protected override void Awake()
    {
        base.Awake();
        foreach (var item in buttonList)
        {
            switch (item.name)
            {
                case "settingButton":
                    item.onClick.AddListener(() =>
                    {
                        WindowManager.Instance.OpenWindow(WindowType.SettingWindow);
                        WindowManager.Instance.CloseWindow(WindowType.FunctionalWindow);
                    });
                    break;
                case "StopButton":  
                    item.onClick.AddListener(() =>
                    {
                        if (Time.timeScale == 0)
                        {
                            Time.timeScale = 1;
                        }
                        else
                        {
                            Time.timeScale = 0;
                        }
                    });
                    break;
                case "ReturnButton":  //ÍË³ö
                    item.onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene("Start");
                        WindowManager.Instance.CloseWindow(WindowType.MainWindow);
                        WindowManager.Instance.CloseWindow(WindowType.FunctionalWindow);
                        WindowManager.Instance.OpenWindow(WindowType.StartWindow);
                    });
                    break;
                case "HideButton": //¹Ø±Õ
                    item.onClick.AddListener(() =>
                    {
                        if (Time.timeScale == 0)
                        {
                            Time.timeScale = 1;
                        }
                     
                        WindowManager.Instance.CloseWindow(WindowType.FunctionalWindow);
                    });
                    break;
            }
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
}

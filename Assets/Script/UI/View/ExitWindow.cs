using DG.Tweening;
using Game.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitWindow : BaseWindow
{
    Text info;
    public ExitWindow()
    {
        resName = "Prefab/UI/ExitWindow";
        name = "ExitWindow";
        resident = false ;
        selfType = WindowType.ExitWindow;
        scenesType = ScenesType.None;
    }
    protected override void Awake()
    {
        info = transform.Find("Image/info").GetComponent<Text>();
        base.Awake();
        foreach (var item in buttonList)
        {
            switch (item.name)
            {
                case"ExitButton":
                    item.onClick.AddListener(() =>
                    {
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
                    });
                    break;
                case "RestartButton":
                    item.onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene("Start");
                        WindowManager.Instance.CloseWindow(WindowType.ExitWindow);
                        WindowManager.Instance.CloseWindow(WindowType.FunctionalWindow);
                        WindowManager.Instance.CloseWindow(WindowType.MainWindow);
                        WindowManager.Instance.OpenWindow(WindowType.StartWindow);
                    });
                    break;
            }
        }
    }
    public void Init(string info)
    {
        this.info.text= info;
    }
}

using Game.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWindow : BaseWindow
{
    public LevelWindow()
    {
        resName = "Prefab/UI/LevelWindow";
        name = "LevelWindow";
        resident = true;
        selfType = WindowType.LevelWindow;
        scenesType = ScenesType.None;
    }
    protected override void Awake()
    {
        base.Awake();
        foreach (var item in buttonList)
        {
            switch (item.name)
            {
                case "common":
                    item.onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene("Common");
                        WindowManager.Instance.CloseWindow(WindowType.LevelWindow);
                        WindowManager.Instance.CloseWindow(WindowType.StartWindow);
                        WindowManager.Instance.OpenWindow(WindowType.MainWindow);
                    });
                    break;
                case "difficulty":
                    item.onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene("Difficult");
                        WindowManager.Instance.CloseWindow(WindowType.LevelWindow);
                        WindowManager.Instance.CloseWindow(WindowType.StartWindow);
                        WindowManager.Instance.OpenWindow(WindowType.MainWindow);
                    });
                    break;
                case "Training":
                    item.onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene("2");
                        WindowManager.Instance.CloseWindow(WindowType.LevelWindow);
                        WindowManager.Instance.CloseWindow(WindowType.StartWindow);
                        WindowManager.Instance.OpenWindow(WindowType.MainWindow);
                    });
                    break;
                case "HideButton":
                    item.onClick.AddListener(() =>
                    {

                        WindowManager.Instance.CloseWindow(WindowType.LevelWindow);

                    });
                    break;
            }
        }
    }
}

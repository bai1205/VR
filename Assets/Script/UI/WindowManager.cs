using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.View;
using System;

public class WindowManager: MonoSingleton<WindowManager>
{
    Dictionary<WindowType, BaseWindow> windowDIC = new Dictionary<WindowType, BaseWindow>();
    public void Init()  
    {
        if (!windowDIC.ContainsKey(WindowType.HintWindow))
        {
            windowDIC.Add(WindowType.HintWindow, new HintWindow());  
            windowDIC.Add(WindowType.ExitWindow, new ExitWindow());
            windowDIC.Add(WindowType.StartWindow, new StartWindow()); 
            windowDIC.Add(WindowType.MainWindow, new MainWindow()); 
            windowDIC.Add(WindowType.SkillWindow, new SkillWindow());
            windowDIC.Add(WindowType.SettingWindow, new SettingWindow());
            windowDIC.Add(WindowType.FunctionalWindow, new FunctionalWindow());
            windowDIC.Add(WindowType.LevelWindow, new LevelWindow());
        }
    }

    public void OpenHintWindow(string info)  
    {
        HintWindow hintWindow = OpenWindow(WindowType.HintWindow) as HintWindow;
        hintWindow.PlayInfo(info);
    }

    public void Update()  
    {
        foreach (var window in windowDIC.Values)
        {
            if (window.IsVisible())
            {
                window.Update(Time.deltaTime);
            }
        }
            
    }
    public BaseWindow OpenWindow(WindowType type) {
        BaseWindow window;
        if (windowDIC.TryGetValue(type, out window))
        {
            window.Open();
            return window;
        }
        else
        {
            Debug.LogError($"Open Error:{type}");
            return null;
        }
    }

    //关闭窗口
    public void CloseWindow(WindowType type) {
        BaseWindow window;
        if (windowDIC.TryGetValue(type, out window))
        {
            window.Close();
        }
        else
        {
            Debug.LogError($"Open Error:{type}");
        }
    }

    //预加载
    public void PreLoadWindow(ScenesType type)
    {
        foreach (var item in windowDIC.Values)
        {
            if (item.GetScenesType()==type)
            {
                item.PreLoad();
            }
        }
    }

    public void HideAllWindow(ScenesType type,bool isDestroy=false) {

        foreach (var item in windowDIC.Values)
        {
            if (item.GetScenesType() == type)
            {
                item.Close(isDestroy);
            }
        }
    }
    public void ShowAllWindow(ScenesType type)
    {

        foreach (var item in windowDIC.Values)
        {
            if (item.GetScenesType() == type)
            {
                item.Open();
            }
        }
    }

}

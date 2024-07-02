using Game.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingWindow : BaseWindow
{
    Slider slider;
    public SettingWindow()
    {
        resName = "Prefab/UI/SettingWindow";
        name = "SettingWindow";
        resident = false;
        selfType = WindowType.SettingWindow;
        scenesType = ScenesType.None;
    }
    protected override void Awake()
    {
        base.Awake();
        slider = transform.Find("Slider").GetComponent<Slider>();
        slider.onValueChanged.AddListener((v) =>
        {
            AudioManager.Instance.SetAudio(v);
        });
        foreach (var item in buttonList)
        {
            switch (item.name)
            {
                case "HideButton":
                    item.onClick.AddListener(() =>
                    {
                        WindowManager.Instance.CloseWindow(WindowType.SettingWindow);
                    });
                    break;
            }
        }
    }
}

using DG.Tweening;
using Game.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HintWindow : BaseWindow
{
    Text Info;
    public HintWindow()
    {
        resName = "Prefab/UI/HintWindow";
        name = "HintWindow";
        resident = true ;
        selfType = WindowType.TipsWindow;
        scenesType = ScenesType.Battle;
    }
    protected override void Awake()
    {
        base.Awake();
        Info = transform.Find("Image/Info").GetComponent<Text>();
    }
    public void PlayInfo(string info)  //²¥·ÅÐÅÏ¢
    {
        Info.text = info;
        Timer.Instance.PlayTimer(1, () =>
        {
            WindowManager.Instance.CloseWindow(WindowType.HintWindow);
        });
    }
}

using FSM.Playe;
using Game.View;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YuLongFSM;

public class MainWindow : BaseWindow
{
    Button startBusinessButton;
    Transform hitWindow;
    Text info;

    Text level;
    Text gold_Text;

    Image hp;
    Image mp;
    Image exp;
    Text time_text;
    float time;
   public  List<SkillShowItem> skillShowItems = new List<SkillShowItem>();
    public MainWindow()
    {
        resName = "Prefab/UI/MainWindow";
        name = "MainWindow";
        resident = false ;
        selfType = WindowType.MainWindow;
        scenesType = ScenesType.None;
    }
    protected override void Awake()
    {
        base.Awake();
        skillShowItems.Clear();
        hitWindow = transform.Find("HitWindow");
        info = hitWindow.Find("info").GetComponent<Text>();
        hp = transform.Find("RoleInfo/hp").GetComponent<Image>();
        Transform skillRoot = transform.Find("skillRoot");

        for (int i = 0; i < skillRoot.childCount; i++)
        {
            skillShowItems .Add( skillRoot.GetChild(i).AddComponent<SkillShowItem>());
        }
        gold_Text = transform.Find("gold/gold_Text").GetComponent<Text>();
        time_text = transform.Find("time/time_Text").GetComponent<Text>();
        time = 120;
    }
    protected override void OnAddListener()
    {
        base.OnAddListener();

    }


    protected override void OnRemoveListener()
    {
        base.OnRemoveListener();
    }

    public override void Update(float deltaTime) 
    {
        base.Update(deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            WindowManager.Instance.OpenWindow(WindowType.FunctionalWindow);
        }

        if (!Player.Instance)
        {
            return;
        }




        hp.fillAmount= Player.Instance.roleData.hp*1.0f/Player.Instance.roleData.maxHp*1.0f;
        gold_Text .text= KnapsackData.Instance.money.ToString();

        if (SceneManager.GetActiveScene().name != "2"&& Boss.Instanse.roleData.hp>0)
        {
            time_text.gameObject.SetActive(true);
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 0;
         
                WindowManager.Instance.CloseWindow(WindowType.MainWindow);
                ExitWindow exitWindow = WindowManager.Instance.OpenWindow(WindowType.ExitWindow) as ExitWindow;
                exitWindow.Init("FAIL");
                Boss.Instanse.enabled = false;
            }
            time_text.text = "Timer:" + time.ToString("0.00");
        }
        else
        {
            time_text.transform.parent.gameObject.SetActive(false);
        }


    }

}
public class SkillShowItem : MonoBehaviour
{
    SkillData skillData;
    Image image;
    Image cd;
    Text time;
    Text useInfo;
    internal void Init(SkillData skillData)
    {
        this.skillData = skillData;
        image = transform.Find("skill").GetComponent<Image>();
        cd = transform.Find("cd").GetComponent<Image>();
        time = transform.Find("time").GetComponent<Text>();
        useInfo = transform.Find("useInfo").GetComponent<Text>();
        useInfo.text = skillData.SkillInput;
        image.sprite = Resources.Load<Sprite>(skillData.icon);
        gameObject.SetActive(true);
    }
    public void Update()
    {
        if (skillData!=null)
        {
            if (skillData.time>= skillData.CD)
            {
                cd.gameObject.SetActive(false);
                time.gameObject.SetActive(false);
            }
            else
            {
                cd.gameObject.SetActive(true);
                time.gameObject.SetActive(true);
                cd.fillAmount = skillData.time / skillData.CD;
                time.text = (skillData.CD - skillData.time).ToString("0.00");
            }
        }
    }
}

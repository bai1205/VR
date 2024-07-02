using Game.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillWindow : BaseWindow
{
    Text gold;
    Transform skillUseRoot;
    Transform skillRoot;
    public SkillWindow()
    {
        resName = "Prefab/UI/SkillWindow";
        name = "SkillWindow";
        resident = true;
        selfType = WindowType.SkillWindow;
        scenesType = ScenesType.None;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        gold = transform.Find("gold").GetComponent<Text>();
        gold.text = "skill coin:" + KnapsackData.Instance.money;

    }
    protected override void Awake()
    {
        base.Awake();
        gold = transform.Find("gold").GetComponent<Text>();
        foreach (var item in buttonList)
        {
            switch (item.name)
            {
                case"skill1Button":
                    item.onClick.AddListener(() =>
                    {
                        OnClickSkill(item, 1);
                    });
                    break;
                case "skill2Button":
                    item.onClick.AddListener(() =>
                    {
                        OnClickSkill(item, 2);
                    });
                    break;
                case "skill3Button":
                    item.onClick.AddListener(() =>
                    {
                        OnClickSkill(item, 3);
                    });
                    break;
                case "skill4Button":
                    item.onClick.AddListener(() =>
                    {
                        OnClickSkill(item, 4);
                    });
                    break;
                case "skill5Button":
                    item.onClick.AddListener(() =>
                    {
                        OnClickSkill(item, 5);
                    });
                    break;
                case "HideButton":
                    item.onClick.AddListener(() =>
                    {
                        WindowManager.Instance.CloseWindow(WindowType.SkillWindow);
                    });
                    break;
            }
        }

        skillUseRoot = transform.Find("skillUseRoot");
        skillRoot = transform.Find("skillRoot");
        gold.text = "Skill coin :" + KnapsackData.Instance.money;
        foreach (var id in Save.Instance.gameData.skillIDList)
        {
            string path = id.ToString() + "/skill" + id + "Button/Text";
            skillRoot.Find(path).GetComponent<Text>().text = "Carry";
            skillRoot.Find(id.ToString()).SetParent(skillUseRoot);
          //  GameManager.Instance.useSkill.Add(id);
        }
    }
    private void OnClickSkill(Button button, int skillID)
    {
       Transform skillGo = button.transform.parent;
        if (skillGo.parent == skillRoot)
        {
            if (KnapsackData.Instance.money >= 1)
            {
                KnapsackData.Instance.money -= 1;
                Save.Instance.gameData.gold = KnapsackData.Instance.money;
                Save.Instance.gameData.skillIDList.Add(skillID);
                skillGo.SetParent(skillUseRoot);
                WindowManager.Instance.OpenHintWindow("Unlocking succeeded");
                gold.text = "Skill coin :" + KnapsackData.Instance.money;
                button.transform.GetChild(0).GetComponent<Text>().text = "Carry";
            }
            else
            {
                WindowManager.Instance.OpenHintWindow("Insufficient skill money");
            }
        }
        else
        {
            if (button.transform.GetChild(0).GetComponent<Text>().text == "Carried")
            {
                button.transform.GetChild(0).GetComponent<Text>().text = "Carry";
                GameManager.Instance.useSkill.Remove(skillID);
            }
            else
            {
                if (GameManager.Instance.useSkill.Count>=4)
                {
                    WindowManager.Instance.OpenHintWindow("Skills carry a maximum of 4");
                }
                else
                {
                    button.transform.GetChild(0).GetComponent<Text>().text = "Carried";
                    GameManager.Instance.useSkill.Add(skillID);
                }
            }
        }
    }
}

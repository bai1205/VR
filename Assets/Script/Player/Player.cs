using FSM.Playe;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YuLongFSM;

public class Player : Creature
{
    public static Player Instance;
    public GameObject shubiaoPos;

    public List<SkillData> allSkillDataList = new List<SkillData>();
    List<SkillData> skillList1 = new List<SkillData>();
    List<SkillData> skillList2 = new List<SkillData>();
    List<SkillData> skillList3 = new List<SkillData>();
    List<SkillData> skillList4 = new List<SkillData>();
    List<SkillData> skillList5 = new List<SkillData>();
    public override void Start()
    {
        base.Start();
        Instance = this;

        fSM.Register(FSMState.Idle, new Idle());  
        fSM.Register(FSMState.Walk, new Walk()); 
        fSM.Register(FSMState.Run, new Run());  
        fSM.Register(FSMState.Attack, new Attack());  
        fSM.Register(FSMState.Hit, new Hit());  
        fSM.Register(FSMState.Die, new Die());  

        fSM.Switch(FSMState.Idle);
        skillList1.Add(ConfigManager.Instance.GetSkillData(6));
        skillList2.Add(ConfigManager.Instance.GetSkillData(7));
        skillList3.Add(ConfigManager.Instance.GetSkillData(8));
        skillList4.Add(ConfigManager.Instance.GetSkillData(9));
        skillList5.Add(ConfigManager.Instance.GetSkillData(10));

        fSMData.shubiaoPos = shubiaoPos;
    }


    public override void Update()
    {
        base.Update();

        foreach (var skills in SkillDic.Values)
        {
            foreach (var skill in skills)
            {
                if (skill.time < skill.CD)
                {
                    skill.time += Time.deltaTime;
                    if (skill.time >= skill.CD)
                    {
                        skill.time = skill.CD;
                    }
                }

                if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), skill.SkillInput)))  
                {
                    cutSkillDatas = skills;
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (cutSkillDatas.Count != 0 && cutSkillDatas[0].time >= cutSkillDatas[0].CD && cutSkillDatas != null)
            {
                fSM.Switch(FSMState.Attack);

            }
        }
    }

    public void FixedUpdate()
    {
        if (fSM != null)
        {
            fSM.FixedUpdate();  
        }
    }
    public override void SkillAttack(SkillData skillData)
    {
        base.SkillAttack(skillData);
        if (skillData.SkillEffects != null && skillData.SkillEffects != "")
        {
            GameObject prefab = Resources.Load<GameObject>(skillData.SkillEffects);
            GameObject player = GameObject.Instantiate(prefab, fSMData.creature.transform.position + Vector3.up, Quaternion.identity);
            player.GetComponent<Skill>().Init(fSMData.creature);

            Ray rayTarget = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitTarget;
            Creature target = null;
            if (Physics.Raycast(rayTarget, out hitTarget))
            {
                target = hitTarget.transform.GetComponent<Creature>();
            }
            switch (skillData.EffectsType)
            {
                case "Move":
                    if (target)
                    {
                        Vector3 pos = new Vector3(target.transform.position.x, fSMData.creature.transform.position.y, target.transform.position.z);
                        fSMData.creature.transform.LookAt(pos);
                    }
                    player.GetComponent<Rigidbody>().AddForce(fSMData.creature.transform.forward * 300);
                    break;
                case "Front":
                    player.transform.position += fSMData.creature.transform.forward * 3;
                    break;
                case "Target":
                    if (target)
                    {
                        Vector3 pos = new Vector3(target.transform.position.x, fSMData.creature.transform.position.y, target.transform.position.z);
                        fSMData.creature.transform.LookAt(pos);
                    }
                    player.GetComponent<Rigidbody>().AddForce(fSMData.creature.transform.forward * 300);
                    break;
                case "Oneself":
                    player.GetComponent<Skill>().Target = fSMData.creature;
                    player.transform.position = fSMData.creature.transform.position + Vector3.up * 0.5f;
                    player.transform.SetParent(fSMData.creature.transform);
                    if (player.GetComponent<Rigidbody>())
                    {
                        player.GetComponent<Rigidbody>().isKinematic = true;
                    }
                    break;
                case "Parabola":
                    if (target != null)
                    {
                        player.GetComponent<PaoWuXianZiDan>().Target = target;
                        player.GetComponent<PaoWuXianZiDan>().Init(fSMData.creature);
  
                        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                    }
                    else
                    {
                        player.GetComponent<PaoWuXianZiDan>().targetPos = hitTarget.point;
                        player.GetComponent<PaoWuXianZiDan>().Init(fSMData.creature);
                        transform.LookAt(new Vector3(hitTarget.point.x, transform.position.y, hitTarget.point.z));
                    }

                    break;
                default:
                    break;
            }
        }
        else
        {
            if (skillData.SkillAudio != null && skillData.SkillAudio != "")
            {
                audioManager.PlayAudio(skillData.SkillAudio);
            }
            int colliderCount = Physics.OverlapSphereNonAlloc(transform.position + Vector3.up * 1.5f + transform.forward * 1.5f, 1.5f, colliders);
            Creature creature = null;
            for (int i = 0; i < colliderCount; i++)
            {

                creature = colliders[i].GetComponent<Creature>();
                if (creature != null && creature != this)
                {
                    if (creature)
                    {
                        audioManager.PlayAudio(skillData.SkillHitAudio);
                        break;
                    }
                }
            }

            for (int i = 0; i < colliderCount; i++)
            {

                creature = colliders[i].GetComponent<Creature>();
                if (creature != null && creature != this)
                {
                    creature.Hurt(roleData.attack);
                    switch (skillData.SkillHitMove[0])
                    {
                        case "front":
                            creature.GetComponent<Rigidbody>().AddForce(transform.forward * int.Parse(skillData.SkillMove[1]) * 150, ForceMode.Force);
                            break;
                    }
                }
            }
            colliders = new Collider[10];
        }
    }

    public void AddSkill1()
    {
        SkillDic.Add(1, skillList1);
        allSkillDataList.Add(skillList1[0]);

    }
    public void InitSkill()
    {
        MainWindow mainWindow = WindowManager.Instance.OpenWindow(WindowType.MainWindow) as MainWindow;
        for (int i = 0; i < allSkillDataList.Count; i++)
        {
            mainWindow.skillShowItems[i].Init(allSkillDataList[i]);
        }
    }
    public void AddSkill2()
    {

        SkillDic.Add(2, skillList2);

        allSkillDataList.Add(skillList2[0]);

    }
    public void AddSkill4()
    {

        SkillDic.Add(4, skillList4);

        allSkillDataList.Add(skillList4[0]);

    }
    public void AddSkill3()
    {

        SkillDic.Add(3, skillList3);

        allSkillDataList.Add(skillList3[0]);
    }
    public void AddSkill5()
    {
        SkillDic.Add(5, skillList5);
        allSkillDataList.Add(skillList5[0]);
    }

    public override void Hurt(int attack)
    {
        if (roleData.hp<=0)
        {
            return;
        }
        base.Hurt(attack);
        if (roleData.hp <= 0)
        {
            Timer.Instance.PlayTimer(5, () =>
            {
                ExitWindow exitWindow = WindowManager.Instance.OpenWindow(WindowType.ExitWindow) as ExitWindow;
                exitWindow.Init("Failed");
            });
        }
    }
}

using FSM.Playe;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YuLongFSM;

public class Boss : Creature
{
    public static Boss Instanse;
    WordHpUI wordHp;
    List<SkillData> skillList1 = new List<SkillData>();
    List<SkillData> skillList2 = new List<SkillData>();
    List<SkillData> skillList3 = new List<SkillData>();
    List<SkillData> skillList4 = new List<SkillData>();
    List<SkillData> skillList5 = new List<SkillData>();
    public override void Start()
    {
        base.Start();
        Instanse = this;
        wordHp = WorldUIManager.Instance.GetWordHpUI(this, 7);
        fSM.Register(FSMState.Idle, new MonsterIdle());  
        fSM.Register(FSMState.Attack, new Attack());  
        fSM.Register(FSMState.Hit, new Hit());  
        fSM.Register(FSMState.Die, new Die()); 

        fSM.Switch(FSMState.Idle);

        skillList1.Add(ConfigManager.Instance.GetSkillData(11));
        skillList2.Add(ConfigManager.Instance.GetSkillData(12));
        skillList3.Add(ConfigManager.Instance.GetSkillData(13));
        skillList4.Add(ConfigManager.Instance.GetSkillData(14));
        skillList5.Add(ConfigManager.Instance.GetSkillData(15));

        if (SceneManager.GetActiveScene().name!="2")
        {
            AddSkill1();
            AddSkill2();
            AddSkill3();
            AddSkill4();
            AddSkill5();
        }
        else
        {

        }

    }

    float dis;

    public float AttackTime=5;
    float attackTime;
    public override void Update()
    {
        base.Update();
        if (roleData.hp <= 0)
        {
            if (fSM.cutFSMState!=FSMState.Die)
            {
                fSM.Switch(FSMState.Die);
            }
            return;
        }
        if (Player.Instance.roleData.hp<=0)
        {
            return;
        }

        attackTime -= Time.deltaTime;

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
            }
        }

        if (SkillDic.Count != 0)
        {
            dis = Vector3.Distance(transform.position, Player.Instance.transform.position);
            if (dis < 5)
            {
                if (SkillDic[4][0].time == SkillDic[4][0].CD)
                {
                    if (fSM.cutFSMState != FSMState.Attack)
                    {
                        cutSkillDatas = SkillDic[4];
                        fSM.Switch(FSMState.Attack);
                    }
                }
            }
            else if (dis >= 5)
            {
                if (attackTime <= 0)
                {
                    attackTime = AttackTime / 2;
                    int index = UnityEngine.Random.Range(1, 4);
                    if (SkillDic[index][0].time == SkillDic[index][0].CD)
                    {
                        if (fSM.cutFSMState != FSMState.Attack)
                        {
                            cutSkillDatas = SkillDic[index];
                            fSM.Switch(FSMState.Attack);
                        }
                    }
                }

            }
        }


        Vector3 dir = Player.Instance.transform.position - transform.position;
        TurnTo(dir.normalized);
    }
    public override void Hurt(int attack)
    {
        if (roleData.hp<=0)
        {
            return;
        }
        base.Hurt(attack);
        wordHp.SetHp((roleData.hp * 1.0f) / (roleData.maxHp*1.0f),attack);
        if (roleData.hp<=0)
        {
            Timer.Instance.PlayTimer(5, () =>
            {
                ExitWindow exitWindow = WindowManager.Instance.OpenWindow(WindowType.ExitWindow) as ExitWindow;
                exitWindow.Init("Victory");
            });
        }
    }
    public override void SkillAttack(SkillData skillData)
    {
        base.SkillAttack(skillData);
        if (skillData.SkillEffects != null && skillData.SkillEffects != "")
        {
            GameObject prefab = Resources.Load<GameObject>(skillData.SkillEffects);
            GameObject go = GameObject.Instantiate(prefab, fSMData.creature.transform.position + Vector3.up, Quaternion.identity);
            go.GetComponent<Skill>().Init(fSMData.creature);


            Creature target = Player.Instance;

            switch (skillData.EffectsType)
            {
                case "Move":
                    go.transform.rotation = fSMData.creature.transform.rotation;
                    go.GetComponent<Rigidbody>().AddForce(fSMData.creature.transform.forward * 300);
                    break;
                case "Front":
                    go.transform.position += fSMData.creature.transform.forward * 3;
                    break;
                case "Target":
                    go.GetComponent<Rigidbody>().AddForce(fSMData.creature.transform.forward * 300);
                    break;
                case "Oneself":
                    go.GetComponent<Skill>().Target = fSMData.creature;
                    go.transform.position = fSMData.creature.transform.position + Vector3.up * 0.5f;
                    go.transform.SetParent(fSMData.creature.transform);
                    if (go.GetComponent<Rigidbody>())
                    {
                        go.GetComponent<Rigidbody>().isKinematic = true;
                    }
                    break;
                case "Parabola":
                    if (target != fSMData.creature)
                    {
                        go.GetComponent<PaoWuXianZiDan>().Target = target;
                        go.GetComponent<PaoWuXianZiDan>().Init(fSMData.creature);
                    }
                    go.GetComponent<PaoWuXianZiDan>().targetPos = target.transform.position;

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
                        //  player.transform.LookAt(creature.transform);
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

    }
    public void AddSkill2()
    {

        SkillDic.Add(2, skillList2);

    }
    public void AddSkill4()
    {

        SkillDic.Add(4, skillList3);

    }
    public void AddSkill3()
    {

        SkillDic.Add(3, skillList4);

    }
    public void AddSkill5()
    {
        SkillDic.Add(5, skillList5);
    }
    public void OnDestroy()
    {
        if (wordHp)
        {
            GameObject.Destroy(wordHp.gameObject);
        }
    }
}

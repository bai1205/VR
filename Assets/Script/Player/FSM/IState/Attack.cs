using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YuLongFSM;
using static HeroAnimations;

namespace FSM.Playe
{
    public enum AttackState
    {
        QianYao,//前摇
        GongJi,//攻击
        HouYao,//后摇
        Exit,//退出
    }
    public class Attack : FSMIState<FSMData>
    {
        Rigidbody rigidbody;
        AttackState state;
        CharacterController characterController;
        Animator animator;
        Creature player;

        Vector3 direction = Vector3.zero;
        int index;
        private HeroAnimations animations;
        float time;
        float attackTime;
        bool isAttack;
        SkillData skillData;

        RuntimeAnimatorController runtimeAnimatorController;

        //  SkillData
        public override void OnEnter()
        {
            index = 0;

            animations = fSMData.creature.animations;
            characterController = fSMData.creature.characterController;
            animator = fSMData.creature.GetComponent<Animator>();
            rigidbody = fSMData.creature.GetComponent<Rigidbody>();
            player = fSMData.creature;
            runtimeAnimatorController = animator.runtimeAnimatorController;

            Update();
        }
        private void NextAttack()
        {
            index++;
            animations.AttackEnd(skillData.SkillAnimatorValue);
            if (index >= player.cutSkillDatas.Count)
            {
                animator.runtimeAnimatorController = runtimeAnimatorController;
                animations.AttackEnd(skillData.SkillAnimatorValue);
                fSMManager.Switch(FSMState.Idle);
                return;
            }
            Update();
        }

        private void Update()
        {

            skillData = player.cutSkillDatas[index];
            skillData.time = 0;
            attackTime = skillData.SkillTime * skillData.SkillAttackTime;
            animator.runtimeAnimatorController = Resources.Load<AnimatorOverrideController>(skillData.SkillAnimatorPath);
            animations.PlayAttack(skillData.SkillAnimatorValue);
            time = 0;
            state = AttackState.QianYao;

            switch (skillData.SkillMove[0])
            {
                case "front":
                    rigidbody.AddForce(player.transform.forward * int.Parse(skillData.SkillMove[1]) * 150, ForceMode.Force);

                    break;
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (skillData == null)
            {
                return;
            }
            time += Time.deltaTime;
            switch (state)
            {
                case AttackState.QianYao:
                    if (time >= attackTime)
                    {
                        state = AttackState.GongJi;
                    }
                    break;
                case AttackState.GongJi:
                    //  Debug.Log("攻击");
                    fSMData.creature.SkillAttack(skillData);

                    state = AttackState.HouYao;
                    break;
                case AttackState.HouYao:

                    if (time > attackTime && time < skillData.SkillTime * 0.9)
                    {
                        if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), skillData.SkillInput)))
                        {
                            isAttack = true;
                        }

                    }
                    else if (time > skillData.SkillTime)
                    {
                        if (isAttack)
                        {
                            isAttack = false;
                            NextAttack();
                        }
                        else
                        {
                            state = AttackState.Exit;
                        }
                    }
                    break;
                case AttackState.Exit:
                    animations.AttackEnd(skillData.SkillAnimatorValue);
                    animator.runtimeAnimatorController = runtimeAnimatorController;
                    fSMManager.Switch(FSMState.Idle);
                    break;
                default:
                    break;
            }

        }


    }
}

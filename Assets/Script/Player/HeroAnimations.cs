
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimations : MonoBehaviour
{

    public enum HState
    {
        None,
        Idle,
        Walk,
        Run,
        Attack,
        Die,
        Gethit,
        Intonate,
            switchover,
    }


    public HState state = HState.Idle;

    Animator animator;


    void Awake()
    {
        animator = GetComponent<Animator>();

    }


    private void SetFalseAll()
    {
        animator.SetBool("idle", false);
        animator.SetBool("run", false);
        animator.SetBool("walk", false);
        animator.SetBool("Attack", false);
        animator.SetBool("die", false);
    }

    public void PlayIdle()
    {
        if (state == HState.Attack || state == HState.Gethit)
            return;
        SetFalseAll();
        animator.SetBool("idle", true);
        state = HState.Idle;
    }

    public void PlayRun()
    {
        
        if (state == HState.Attack)
            return;
        SetFalseAll();
        animator.SetBool("run", true);
        state = HState.Run;
    }
    public void PlayWalk()
    {
        if (state == HState.Attack)
            return;
        SetFalseAll();
        animator.SetBool("walk", true);
        state = HState.Walk;
    }

    public void PlayAttack(string value)
    {
        SetFalseAll();
        animator.SetBool(value, true);
        state = HState.Attack;

    }
    public void PlayAttack1()
    {
        SetFalseAll();
        animator.SetTrigger("attack1");
        state = HState.Attack;

    }

    public void PlayDie()
    {
        SetFalseAll();
        animator.SetTrigger("die");
        state = HState.Die;
    }

    public void PlayGethit()
    {
        SetFalseAll();
        animator.SetTrigger("gethit");
        state = HState.Gethit;
    }
    public void PlayIntonate()
    {
        SetFalseAll();
        animator.SetBool("intonate", true);
        state = HState.Intonate;
    }

    public void AttackEnd(string value)
    {
        state = HState.None;
        animator.SetBool(value, false );
        PlayIdle();
    }

    public void GethitEnd()
    {
        state = HState.None;
        PlayIdle();
    }
    public void PlaySwitchover()
    {
        state = HState.switchover;
        animator.SetTrigger("switchover");
    }
    public void SwitchoverEnd()
    {
        state = HState.None;
        PlayIdle();
    }
}

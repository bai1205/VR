using FSM.Playe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuLongFSM;

public class Monster : Creature
{
    public override void Start()
    {
        base.Start();
 
        fSM.Register(FSMState.Idle, new MonsterIdle());  
        fSM.Register(FSMState.Attack,new MonsterAttack());  
        fSM.Register(FSMState.Hit, new Hit());  
        fSM.Register(FSMState.Die, new Die());  
        fSM.Switch(FSMState.Idle);
    }
    public void FixedUpdate()
    {
        fSM.FixedUpdate();
    }
}

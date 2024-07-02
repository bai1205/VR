using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuLongFSM;

public class MonsterIdle : FSMIState<FSMData>
{

    private HeroAnimations animations;


    public override void OnEnter()
    {
        animations = fSMData.creature.animations;
        animations.PlayIdle();

    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}

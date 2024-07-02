using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESkill : Skill
{
    public float time=2;
    public override void Init(Creature creature)
    {
        base.Init(creature);
        GameObject.Destroy(gameObject, time);
    }
}

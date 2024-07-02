using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Skill
{
    public override void Init(Creature creature)
    {
        base.Init(creature);
        creature.roleData.hp += 10;
        GameObject.Destroy(gameObject, 1);
    }
    public override void OnTriggerEnter(Collider other)
    {

    }
}

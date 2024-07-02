using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Creature creature;
    public Creature Target
    {
        get { return target; }
        set {
            if (value==null)
            {
            }
            target = value; }
    }
    private Creature target;

    public virtual void Init(Creature creature)
    {
        this.creature = creature;
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        target = other.GetComponent<Creature>();
        if (target != null&& target != creature)
        {
            target.Hurt(30);
        }
    }

}

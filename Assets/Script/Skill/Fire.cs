using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Skill
{
    float time;
    public float skillTime = 10;
    public int timer=2;
    bool isAttack;
    public override void OnTriggerEnter(Collider other)
    {
        if (isAttack==false )
        {
            Target = other.GetComponent<Creature>();
            if (Target != null && Target != creature)
            {
                isAttack = true;
                transform.GetComponent<Rigidbody>().isKinematic = true;
                transform.position = Target.transform.position + Vector3.up;
                transform.SetParent(Target.transform);
                Target.Hurt(15);
            }
        }
    }
    public void Update()
    {
        if (isAttack)
        {
            time += Time.deltaTime;
            if (time >= timer)
            {
                time = 0;
                Target.Hurt(5); 
            }

            skillTime -= Time.deltaTime;
            if (skillTime <= 0)
            {
                GameObject.Destroy(gameObject);
            }
        }

    }
}

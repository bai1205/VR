using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Skill
{
    float time;
    public void Update()
    {
        time += Time.deltaTime;
        if (time>=10)
        {
            GameObject.Destroy(gameObject);
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        Target = other.GetComponent<Creature>();
        if (other.tag=="zhangaiwu")
        {
            GameObject.Destroy(gameObject);
        }
        if (Target != null && Target != creature)
        {
            Target.Hurt(15);
            GameObject.Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaoWuXianZiDan : Skill
{
    public Vector3 targetPos;
    public float speed = 1f;
    public float height = 10f;
    private Vector3 startPos;
    private Vector3 pos;
    private Vector3 controlPoint;
    private float t = 0f;

    public override void Init(Creature creature)
    {
        base.Init(creature);
        startPos = transform.position;
        if (Target==null)
        {
            pos = targetPos;
        }
        else
        {
            pos = Target.transform.position;
        }

        controlPoint = startPos + (pos - startPos) / 2f + Vector3.up * height;
    }
    private void Update()
    {
        t += Time.deltaTime * speed;

        if (t > 1f)
        {
            t = 1f;
        }

        Vector3 newPos = Mathf.Pow(1 - t, 2) * startPos + 2 * (1 - t) * t * controlPoint + Mathf.Pow(t, 2) * pos;
        transform.position = newPos;
        transform.LookAt(pos);
        if (transform.position == pos)
        {
            Destroy(gameObject);
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}

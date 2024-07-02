using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuLongFSM;
using static HeroAnimations;

public class MonsterAttack : FSMIState<FSMData>
{
    Rigidbody rigidbody;
    HeroAnimations animations;

    Vector3 direction = Vector3.zero;
    Monster monster;
    private Collider[] colliders=new Collider[10];

    float attackTime = 5;
    float time;
    public override void OnEnter()
    {
        monster = fSMData.creature as Monster;
        rigidbody = fSMData.creature.GetComponent<Rigidbody>();
        animations = fSMData.creature.animations;
        animations.PlayWalk();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        fSMData.creature.Gravity();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }
    public void Move()
    {
        time += Time.deltaTime;
        direction = Player.Instance.transform.position - monster.transform.position;
        if (direction != Vector3.zero)
        {
            fSMData.creature.TurnTo(direction);
            float dis = Vector3.Distance(monster.transform.position, Player.Instance.transform.position);
            if (dis<=1.5f)
            {
                if (time>=attackTime)
                {
                    time = 0;
                    animations.PlayAttack("Attack");
                    monster.audioManager.PlayAudio("Audio/Player/attack");
                    Timer.Instance.PlayTimer(0.5f, () =>
                    {
                        int colliderCount = Physics.OverlapSphereNonAlloc(monster.transform.position + Vector3.up * 1.5f + monster.transform.forward * 1.5f, 1.5f, colliders);
                        for (int i = 0; i < colliderCount; i++)
                        {
                            Creature creature = colliders[i].GetComponent<Creature>();
                            if (creature != null && creature != monster)
                            {
                                creature.Hurt(monster.roleData.attack);
                            }
                        }
                    });
                    Timer.Instance.PlayTimer(1, () =>
                    {
                        animations.AttackEnd("Attack");
                    });

                }
                else
                {
                    animations.PlayIdle();
                }
            }
            else
            {
                if (animations.state != HState.Walk)
                {
                    animations.PlayWalk();
                }
                rigidbody.MovePosition(fSMData.creature.transform.position + direction.normalized * Time.deltaTime * PlayerData.speed);
            }

        }
    }
}

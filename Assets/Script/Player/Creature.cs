using System.Collections.Generic;
using UnityEngine;
using YuLongFSM;

public class Creature : MonoBehaviour
{
    public RoleData roleData = new RoleData();
    public HeroAnimations animations;
    public CharacterController characterController;
    public FSMManager<FSMData> fSM;
    public FSMData fSMData = new FSMData();
    public AudioManager audioManager;
    public List<SkillData> cutSkillDatas = new List<SkillData>();
    public Dictionary<int, List<SkillData>> SkillDic = new Dictionary<int, List<SkillData>>();
    protected Collider[] colliders = new Collider[10];

    public virtual void Start()
    {
        animations = gameObject.AddComponent<HeroAnimations>();
        characterController = GetComponent<CharacterController>();
        audioManager = gameObject.AddComponent<AudioManager>();

        fSM = new FSMManager<FSMData>(fSMData);
        fSMData.creature = this;
    }

    // Turn towards the given direction
    public void TurnTo(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            return;
        }
        Quaternion q = Quaternion.identity;
        q.SetLookRotation(direction); // Define the rotation to look towards the specified direction
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, q.eulerAngles.y, 0), Time.deltaTime * 4);
    }

    // Character takes damage
    public virtual void Hurt(int attack)
    {
        if (roleData.hp <= 0)
        {
            fSM.Switch(FSMState.Die);
        }
        else
        {
            roleData.hp -= attack;
            if (fSM.cutFSMState != FSMState.Attack)
            {
                fSM.Switch(FSMState.Hit);
            }
            if (roleData.hp <= 0)
            {
                fSM.Switch(FSMState.Die);
            }
        }
    }

    // Apply gravity
    public void Gravity(float gravity = 5)
    {
    }

    public virtual void Update()
    {
        fSM?.OnUpdate();  
    }

    public virtual void SkillAttack(SkillData skillData)
    {
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Draw a wireframe sphere to represent the range
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 1.5f + transform.forward * 1.5f, 1.5f);
    }
}

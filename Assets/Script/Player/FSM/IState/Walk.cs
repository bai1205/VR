using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YuLongFSM;
using static HeroAnimations;

namespace FSM.Playe
{
    public class Walk : FSMIState<FSMData>
    {
        Rigidbody rigidbody;
        HeroAnimations animations;

        Vector3 direction=Vector3.zero;
        public override void OnEnter()
        {
            rigidbody = fSMData.creature.GetComponent<Rigidbody>();
            animations= fSMData.creature.animations; 
            animations.PlayWalk();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (animations.state!= HState.Walk)
            {
                animations.PlayWalk();
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            fSMData.creature.Gravity();
            Move();
        }
        public  void Move()
        {
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.z = Input.GetAxisRaw("Vertical");
            Vector3 forward=Vector3.zero;
                forward = Camera.main.transform.TransformDirection(direction).normalized;
                // Vector3 forward = Camera.main.transform.forward;
                forward.y = 0;
            if (direction != Vector3.zero)
            {

                rigidbody.MovePosition(fSMData.creature.transform.position+forward.normalized * Time.deltaTime * PlayerData.speed);
                fSMData.creature.TurnTo(forward);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    fSMManager.Switch(FSMState.Run);
                }
            }
            else
            {
                fSMManager.Switch(FSMState.Idle);

            }

        
        }
    }
}

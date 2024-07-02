using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using YuLongFSM;
using static HeroAnimations;

namespace FSM.Playe
{
    public class Run : FSMIState<FSMData>
    {
        Rigidbody rigidbody;
        HeroAnimations animations;

        Vector3 direction=Vector3.zero;
        public override void OnEnter()
        {
            rigidbody = fSMData.creature.GetComponent<Rigidbody>();
            animations = fSMData.creature.animations; 
            animations.PlayRun();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (animations.state!= HState.Run)
            {
                animations.PlayRun();
            }
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Ground")
                {
                    fSMData.pos = hit.point;
                    fSMData.shubiaoPos.gameObject.SetActive(true);
                    fSMData.shubiaoPos.transform.position = hit.point;
                }

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
            float dis = Vector3.Distance(fSMData.pos, fSMData.creature.transform.position);
            if (dis <= 0.5f)
            {
                fSMData.pos = Vector3.zero;
                fSMManager.Switch(FSMState.Idle);
                fSMData.shubiaoPos.gameObject.SetActive(false );
            }
            else
            {
                direction = fSMData.pos - fSMData.creature.transform.position;
                rigidbody.MovePosition(fSMData.creature.transform.position + direction.normalized * Time.deltaTime * PlayerData.speed*2);
                fSMData.creature.TurnTo(direction.normalized);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YuLongFSM;

namespace FSM.Playe
{
    public class Idle : FSMIState<FSMData>
    {
        HeroAnimations animations;
        private Vector3 direction;
        bool isMove;
        private Vector3 forward;

        public override void OnEnter()
        {
            animations = fSMData.creature.animations;
            animations.PlayIdle();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.z = Input.GetAxisRaw("Vertical");
            if (animations.state!=HeroAnimations.HState.Idle)
            {
                animations.PlayIdle();
            }


            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray,out hit)&&hit.collider.tag== "Ground")
                {
                    fSMData.pos = hit.point;
                    fSMData.shubiaoPos.gameObject.SetActive(true);
                    fSMData.shubiaoPos.transform.position = hit.point;
                }
            }
            if (fSMData.pos!=Vector3.zero)
            {
                fSMManager.Switch(FSMState.Run);
            }
        }

        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YuLongFSM;

namespace FSM.Playe
{
    public class Hit : FSMIState<FSMData>
    {
        HeroAnimations animations;
        private Vector3 direction;

        float time;
        public override void OnEnter()
        {
            animations = fSMData.creature.animations;
            animations.PlayGethit();

            time = 0.8f;
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            time -= Time.deltaTime;
            if (time<=0)
            {
                fSMManager.Switch(FSMState.Idle);
            }
        }

        

    }
}

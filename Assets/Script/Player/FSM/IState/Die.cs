using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YuLongFSM;

namespace FSM.Playe
{
    public class Die : FSMIState<FSMData>
    {
        HeroAnimations animations;
        private Vector3 direction;

        public override void OnEnter()
        {
            animations = fSMData.creature.animations;
            animations.PlayDie();
            animations.AttackEnd("Attack1");
            animations.AttackEnd("Attack");
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (animations.state!=HeroAnimations.HState.Die)
            {
                animations.PlayDie();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }

    }
}

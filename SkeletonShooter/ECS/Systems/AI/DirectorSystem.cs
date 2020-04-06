using BabelEngine4;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.AABB;
using BabelEngine4.ECS.Systems;
using DefaultEcs;
using SkeletonShooter.ECS.Components.AI;
using SkeletonShooter.ECS.Components.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Systems.AI
{
    public class DirectorSystem : SystemSkeleton
    {
        EntitySet entities;

        public override void Reset()
        {
            entities = App.world.GetEntities().With<Director>().With<Body>().With<Walking>().AsSet();
        }

        public override void Update()
        {
            bool UsingJumpMod;

            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref AABB aabb = ref entity.Get<AABB>();
                ref Body body = ref entity.Get<Body>();
                ref Director director = ref entity.Get<Director>();
                ref Gravity gravity = ref entity.Get<Gravity>();
                ref Walking walking = ref entity.Get<Walking>();

                UsingJumpMod = false;

                if (director[Actions.MoveRight] != director[Actions.MoveLeft])
                {
                    if (director[Actions.MoveRight])
                    {
                        walking.Current = Math.Min(walking.Current + walking.Acc, walking.Max);
                    }

                    if (director[Actions.MoveLeft])
                    {
                        walking.Current = Math.Max(walking.Current - walking.Acc, -walking.Max);
                    }
                }

                if (
                    director[Actions.Jump]
                    &&
                    gravity.CoyoteCounter <= 5
                )
                {
                    body.Velocity.Y = -gravity.Jump;
                    gravity.Current = -gravity.Jump;
                    gravity.Jumping = true;
                }

                // Don't allow jump pressure if you've let go of jump button
                if (
                    !director[Actions.JumpHold]
                    &&
                    gravity.Current < 0
                )
                {
                    gravity.StoppedHolding = true;
                }

                if (gravity.CoyoteCounter <= 0)
                {
                    gravity.StoppedHolding = false;
                    gravity.floatTimer.Reset();
                }

                if (
                    director[Actions.JumpHold]
                    &&
                    gravity.Current < 0
                    &&
                    !gravity.StoppedHolding
                )
                {
                    gravity.Current += gravity.Current * gravity.JumpHoldMod;

                    UsingJumpMod = true;
                }

                if (gravity.Current < 0)
                {
                    gravity.Current += -Math.Abs(body.EffectiveVelocity.X) * (gravity.JumpHoldMod * (UsingJumpMod ? 0.5f : 0));
                }

                // Floating if you've re-pressed jump button mid-jump
                if (
                    director[Actions.JumpHold]
                    &&
                    gravity.CoyoteCounter >= 6
                    &&
                    (
                        gravity.StoppedHolding
                        ||
                        director[Actions.Jump]
                    )
                    &&
                    !gravity.floatTimer.GetIsFinished()
                )
                {
                    gravity.Current = 0;
                    gravity.StoppedHolding = true;
                    gravity.CurrentlyHolding = true;
                }
                else
                {
                    gravity.CurrentlyHolding = false;
                }
            }
        }
    }
}

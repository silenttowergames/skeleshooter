using BabelEngine4;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.AABB;
using BabelEngine4.ECS.Systems;
using DefaultEcs;
using SkeletonShooter.ECS.Components.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Systems.Physics
{
    public class GravitySystem : SystemSkeleton
    {
        EntitySet entities;

        public override void Reset()
        {
            entities = App.world.GetEntities().With<Gravity>().With<Body>().With<AABB>().AsSet();
        }

        public override void Update()
        {
            bool
                HitBottom,
                HitTop
            ;

            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref Gravity gravity = ref entity.Get<Gravity>();

                if (gravity.Jumping)
                {
                    gravity.Jumping = false;

                    continue;
                }

                ref AABB aabb = ref entity.Get<AABB>();
                ref Body body = ref entity.Get<Body>();

                if (!gravity.CurrentlyHolding)
                {
                    gravity.Current = Math.Min(gravity.Current + (gravity.Acc * (gravity.Current < 0 ? 0.5f : 1)), gravity.Max);
                }

                gravity.CoyoteCounter = Math.Min(gravity.CoyoteCounter + 1, gravity.CoyoteCounterMax);

                HitBottom = aabb.HitBottom();
                HitTop = aabb.HitTop();

                if (HitBottom || HitTop)
                {
                    gravity.Current = HitTop ? gravity.Acc : 0;

                    if (HitBottom)
                    {
                        gravity.CoyoteCounter = 0;
                    }
                    else
                    {
                        gravity.CoyoteCounter = gravity.CoyoteCounterMax;
                    }
                }

                body.Velocity.Y += gravity.Current;
            }
        }
    }
}

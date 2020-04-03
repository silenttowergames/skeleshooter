using BabelEngine4;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.AABB;
using BabelEngine4.ECS.Systems;
using BabelEngine4.Misc;
using DefaultEcs;
using SkeletonShooter.ECS.Components.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Systems.Physics
{
    public class WalkingSystem : SystemSkeleton
    {
        EntitySet entities;

        public override void Reset()
        {
            entities = App.world.GetEntities().With<Body>().With<Walking>().AsSet();
        }

        public override void Update()
        {
            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref Walking walking = ref entity.Get<Walking>();

                // If you're not trying to walk, then there's nothing to do here
                if (walking.Current == 0)
                {
                    continue;
                }

                ref Body body = ref entity.Get<Body>();
                ref AABB aabb = ref entity.Get<AABB>();

                // If you hit a wall, stop walking & continue
                if (
                    (walking.Current > 0 && aabb.HitRight())
                    ||
                    (walking.Current < 0 && aabb.HitLeft())
                )
                {
                    walking.Current = 0;

                    continue;
                }

                // Move based on your momentum
                body.Velocity.X += walking.Current;

                // If you haven't attempted to walk this frame, then slow down
                if (!walking.WalkedThisFrame && aabb.HitBottom())
                {
                    float Unit = Math.Min(Math.Abs(walking.Current), walking.Acc);

                    walking.Current -= Unit * (walking.Current > 0 ? 1 : -1);
                }
                else
                {
                    walking.WalkedThisFrame = false;
                }
            }
        }
    }
}

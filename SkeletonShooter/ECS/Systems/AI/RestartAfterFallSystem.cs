using BabelEngine4;
using BabelEngine4.ECS.Components;
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
    public class RestartAfterFallSystem : SystemSkeleton
    {
        EntitySet entities;

        public override void Reset()
        {
            entities = App.world.GetEntities().With<Body>().With<Gravity>().With<RestartAfterFall>().AsSet();
        }

        public override void Update()
        {
            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref Body body = ref entity.Get<Body>();
                ref Gravity gravity = ref entity.Get<Gravity>();
                ref RestartAfterFall res = ref entity.Get<RestartAfterFall>();
                ref Walking walking = ref entity.Get<Walking>();

                if (gravity.CoyoteCounter >= 1)
                {
                    if (body.Position.Y > App.renderTargets[0].Resolution.Y + 32)
                    {
                        body.Position = res.LastSpot;
                        res.Reset();
                        walking.Current = 0;
                        gravity.Current = 0;

                        return;
                    }
                }
                else if (Program.data.TimeSpeed == 1)
                {
                    res.LastSpot = body.Position;
                }
            }
        }
    }
}

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
    public class DirectorSystem : SystemSkeleton
    {
        EntitySet entities;

        public override void Reset()
        {
            entities = App.world.GetEntities().With<Director>().With<Body>().With<Walking>().AsSet();
        }

        public override void Update()
        {
            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref Body body = ref entity.Get<Body>();
                ref Director director = ref entity.Get<Director>();
                ref Gravity gravity = ref entity.Get<Gravity>();
                ref Walking walking = ref entity.Get<Walking>();

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
            }
        }
    }
}

using BabelEngine4;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.Rendering;
using BabelEngine4.ECS.Systems;
using DefaultEcs;
using SkeletonShooter.ECS.Components.AI;
using SkeletonShooter.ECS.Components.Shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Systems.Physics
{
    public class FlipSpriteOnXSpecialSystem : SystemSkeleton
    {
        EntitySet entities;

        public override void Reset()
        {
            entities = App.world.GetEntities().With<Sprite>().With<Body>().With<FlipSpriteOnX>().AsSet();
        }

        public override void Update()
        {
            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref Body body = ref entity.Get<Body>();
                ref Director director = ref entity.Get<Director>();
                ref Sprite sprite = ref entity.Get<Sprite>();

                if (
                    director[Actions.Shoot]
                    ||
                    (!director[Actions.MoveRight] && !director[Actions.MoveLeft])
                )
                {
                    continue;
                }

                if (body.EffectiveVelocity.X < 0)
                {
                    sprite.FlippedX = true;

                    continue;
                }

                if (body.EffectiveVelocity.X > 0)
                {
                    sprite.FlippedX = false;
                }
            }
        }
    }
}

using BabelEngine4;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.Rendering;
using BabelEngine4.ECS.Systems;
using DefaultEcs;
using Microsoft.Xna.Framework;
using SkeletonShooter.ECS.Components.AI;
using SkeletonShooter.ECS.Components.Physics;
using SkeletonShooter.ECS.Components.Shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Systems.Shooting
{
    public class ShootingSystem : SystemSkeleton
    {
        EntitySet entities;

        public override void Reset()
        {
            entities = App.world.GetEntities().With<Shoot>().With<Body>().AsSet();
        }

        public override void Update()
        {
            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref Director director = ref entity.Get<Director>();
                ref Shoot shoot = ref entity.Get<Shoot>();

                if (!shoot.Cooldown.GetIsFinished())
                {
                    continue;
                }

                if (!director[Actions.Shoot])
                {
                    continue;
                }

                ref Body body = ref entity.Get<Body>();
                ref Sprite sprite = ref entity.Get<Sprite>();
                ref Walking walking = ref entity.Get<Walking>();

                shoot.Cooldown.Reset();

                float Rotation = sprite.FlippedX ? 270 : 90;

                body.Velocity.X += shoot.Knockback * (sprite.FlippedX ? 1 : -1);

                Entity _bullet = App.Factories["bullet"].Create(1, 1, 1, body.Position + new Vector2(6 * (Rotation > 180 ? -1 : 1), 0));
                ref Bullet bullet = ref _bullet.Get<Bullet>();

                bullet.compass.Rotation = Rotation;
                
                bullet.Speed = 3;
            }
        }
    }
}

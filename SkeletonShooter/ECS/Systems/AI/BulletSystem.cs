using BabelEngine4;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Systems;
using BabelEngine4.Misc;
using DefaultEcs;
using SkeletonShooter.ECS.Components.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Systems.AI
{
    public class BulletSystem : SystemSkeleton
    {
        EntitySet entities;

        public override void Reset()
        {
            entities = App.world.GetEntities().With<Body>().With<Bullet>().AsSet();
        }

        public override void Update()
        {
            entities = App.world.GetEntities().With<Body>().With<Bullet>().AsSet();

            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref Body body = ref entity.Get<Body>();
                ref Bullet bullet = ref entity.Get<Bullet>();

                if (Math.Abs(body.EffectiveVelocity.X - body.InitialVelocity.X) > 0.0001f)
                {
                    App.RemoveEntity(entity);
                }

                body.Velocity += Compass.VelocityToMovement(bullet.Speed + bullet.InitialMovement, bullet.compass.Rotation) * Program.data.TimeSpeed;
            }
        }
    }
}

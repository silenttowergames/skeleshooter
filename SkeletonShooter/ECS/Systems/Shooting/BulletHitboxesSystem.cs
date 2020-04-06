using BabelEngine4;
using BabelEngine4.ECS.Components.AABB;
using BabelEngine4.ECS.Systems;
using DefaultEcs;
using SkeletonShooter.ECS.Components.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Systems.Shooting
{
    public class BulletHitboxesSystem : SystemSkeleton
    {
        EntitySet entities;

        public override void Reset()
        {
            entities = App.world.GetEntities().With<Bullet>().With<AABB>().AsSet();
        }

        public override void Update()
        {
            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref AABB aabb = ref entity.Get<AABB>();

                for (int i = 0; i < aabb.Hitboxes.Length; i++)
                {
                    aabb.Hitboxes[i].CanPass = Program.data.TimeSpeed == 1;
                }
            }
        }
    }
}

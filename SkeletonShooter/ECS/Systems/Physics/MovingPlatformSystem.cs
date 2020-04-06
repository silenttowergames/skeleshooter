﻿using BabelEngine4;
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
    public class MovingPlatformSystem : SystemSkeleton
    {
        EntitySet
            platforms,
            recipients
        ;

        public override void Reset()
        {
            platforms = App.world.GetEntities().With<Body>().With<AABB>().With<MovingPlatform>().AsSet();
            recipients = App.world.GetEntities().With<Body>().With<AABB>().With<MovingPlatformRecipient>().AsSet();
        }

        public override void Update()
        {
            RectangleF
                rBounds,
                pBounds
            ;

            float vAdd;

            foreach (ref readonly Entity platform in platforms.GetEntities())
            {
                ref AABB pAABB = ref platform.Get<AABB>();
                ref Body pBody = ref platform.Get<Body>();

                for (int ph = 0; ph < pAABB.Hitboxes.Length; ph++)
                {
                    if(pAABB.Hitboxes[ph].CanPass)
                    {
                        continue;
                    }

                    pBounds = pAABB.Hitboxes[ph].GetRealBounds(pBody.Position);

                    foreach (ref readonly Entity recipient in recipients.GetEntities())
                    {
                        vAdd = 0;

                        ref AABB rAABB = ref recipient.Get<AABB>();
                        ref Body rBody = ref recipient.Get<Body>();

                        for (int rh = 0; rh < rAABB.Hitboxes.Length; rh++)
                        {
                            if (!rAABB.Hitboxes[rh].Solid)
                            {
                                continue;
                            }

                            rBounds = rAABB.Hitboxes[rh].GetRealBounds(rBody.Position);

                            if (
                                pBounds.LineX.Intersects(rBounds.LineX)
                                &&
                                pBounds.Top - rBounds.Bottom < 1
                            )
                            {
                                vAdd = Math.Max(vAdd, pBody.Velocity.X);
                            }
                        }

                        rBody.Velocity.X += vAdd;
                    }
                }
            }
        }
    }
}
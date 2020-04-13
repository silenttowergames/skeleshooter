using BabelEngine4;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.Rendering;
using BabelEngine4.ECS.Systems;
using BabelEngine4.Misc;
using DefaultEcs;
using Microsoft.Xna.Framework;
using SkeletonShooter.ECS.Components.AI;
using SkeletonShooter.ECS.Components.HP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Systems.HP
{
    public class HealthSystem : SystemSkeleton
    {
        EntitySet
            bullets,
            entities
        ;

        public override void Reset()
        {
            bullets = App.world.GetEntities().With<Bullet>().With<Body>().AsSet();

            entities = App.world.GetEntities().With<Health>().With<Body>().With<Sprite>().AsSet();
        }

        public override void Update()
        {
            RectangleF bounds;
            Vector2 boundsSize = new Vector2(16);

            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref Body eBody = ref entity.Get<Body>();
                ref Health hp = ref entity.Get<Health>();
                ref Sprite sprite = ref entity.Get<Sprite>();

                if(hp.HP <= 0)
                {
                    App.RemoveEntity(entity);

                    continue;
                }

                if (!hp.hitTicker.IsFinished(Program.data.TimeSpeed) && hp.hitTicker.Current != 0)
                {
                    hp.hitTicker.Update(Program.data.TimeSpeed);

                    sprite.Invisible = hp.hitTicker.Current % 4 < 2;

                    continue;
                }

                sprite.Invisible = false;

                hp.hitTicker.Reset();

                bounds = new RectangleF(eBody.Position - (boundsSize / 2), boundsSize);

                foreach (ref readonly Entity bullet in bullets.GetEntities())
                {
                    ref Bullet ai = ref bullet.Get<Bullet>();

                    // If the bullet is from the same team as the current entity, do nothing
                    if (ai.team == hp.team)
                    {
                        continue;
                    }

                    ref Body bBody = ref bullet.Get<Body>();

                    if (bounds.Intersects(new RectangleF(bBody.Position, new Vector2(1))))
                    {
                        hp.HP--;

                        App.RemoveEntity(bullet);
                    }
                }
            }
        }
    }
}

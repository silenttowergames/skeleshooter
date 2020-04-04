using BabelEngine4;
using BabelEngine4.Assets.Tiled;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.AABB;
using BabelEngine4.ECS.Components.Rendering;
using BabelEngine4.ECS.Entities;
using BabelEngine4.Misc;
using DefaultEcs;
using Microsoft.Xna.Framework;
using SkeletonShooter.ECS.Components.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Entities
{
    public class BulletFactory : IEntityFactory
    {
        public Entity Create(float LayerDepth, int LayerID, float Parallax, Vector2 Position = default, TiledObject obj = null)
        {
            Entity e = App.world.CreateEntity();

            e.Set(new Body() { Position = Position });
            e.Set(new Sprite("blank", Animations.PlayerIdle) { LayerDepth = LayerDepth, LayerID = LayerID });
            e.Set(new Bullet());
            e.Set(new AABB()
            {
                Hitboxes = new Hitbox[]
                {
                    new Hitbox() { Bounds = new RectangleF(0, 0, 1, 1), CanPass = true, }
                }
            });

            return e;
        }
    }
}

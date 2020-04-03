using BabelEngine4.Assets.Tiled;
using BabelEngine4.ECS.Entities;
using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BabelEngine4;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.Rendering;
using BabelEngine4.ECS.Components.AABB;
using BabelEngine4.Misc;
using SkeletonShooter.ECS.Components.Physics;
using SkeletonShooter.ECS.Components.AI;

namespace SkeletonShooter.ECS.Entities
{
    public class PlayerFactory : IEntityFactory
    {
        public Entity Create(float LayerDepth, int LayerID, float Parallax, Vector2 Position = default, TiledObject obj = null)
        {
            Entity e = App.world.CreateEntity();
            
            e.Set(new Body() { Position = Position });
            e.Set(new Sprite("skeleshooter-16x16", Animations.PlayerIdle) { LayerDepth = LayerDepth, LayerID = LayerID });
            e.Set(new Gravity() { Max = 5f, Acc = 0.4f, CoyoteCounterMax = 10, Jump = 5f });
            e.Set(new AIPlayer());
            e.Set(new Director());
            e.Set(new Walking() { Acc = 0.1f, Max = 1f });
            e.Set(new FlipSpriteOnX());
            e.Set(new AABB()
            {
                Hitboxes = new Hitbox[]
                {
                    new Hitbox() { Bounds = new RectangleF(0, 2, 6, 12) }
                }
            });

            return e;
        }
    }
}

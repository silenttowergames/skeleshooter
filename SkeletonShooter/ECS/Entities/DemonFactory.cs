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
using SkeletonShooter.ECS.Components.HP;
using SkeletonShooter.ECS.Components.Physics;
using SkeletonShooter.ECS.Components.Shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Entities
{
    public class DemonFactory : IEntityFactory
    {
        public Entity Create(float LayerDepth, int LayerID, float Parallax, Vector2 Position = default, TiledObject obj = null)
        {
            Position -= new Vector2(-8, 8);

            Entity e = App.world.CreateEntity();

            e.Set(new MovingPlatformRecipient());
            e.Set(new Body() { Position = Position });
            e.Set(new Sprite("skeleshooter-16x16", Animations.DemonIdle) { LayerDepth = LayerDepth, LayerID = LayerID });
            e.Set(new Gravity()
            {
                Max = 5f,
                Acc = 0.4f,
                CoyoteCounterMax = 10,
                Jump = 3f,
                JumpHoldMod = 0.04f,
                JumpSpeedMod = 0.1f,
                floatTimer = new Ticker(90)
                {
                    ShouldReset = false,
                }
            });
            //e.Set(new AIPlayer());
            e.Set(new Director());
            e.Set(new Walking() { Acc = 0.1f, Max = 1f });
            e.Set(new FlipSpriteOnX());
            e.Set(new Shoot() { Cooldown = new Ticker(10) { ShouldReset = false }, Knockback = 1f, team = Team.BadGuy, });
            e.Set(new Health() { team = Team.BadGuy, HP = 2, hitTicker = new Ticker(30), });
            e.Set(new AABB()
            {
                Hitboxes = new Hitbox[]
                {
                    new Hitbox() { Bounds = new RectangleF(0, 2, 6, 12), PassThrough = false, }
                }
            });

            return e;
        }
    }
}

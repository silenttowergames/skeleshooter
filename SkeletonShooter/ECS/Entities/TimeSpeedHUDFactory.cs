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
using BabelEngine4.ECS.Components.Rendering;
using BabelEngine4.ECS.Components;
using SkeletonShooter.ECS.Components.HUD;

namespace SkeletonShooter.ECS.Entities
{
    public class TimeSpeedHUDFactory : IEntityFactory
    {
        public Entity Create(float LayerDepth, int LayerID, float Parallax, Vector2 Position = default, TiledObject obj = null)
        {
            Entity e = App.world.CreateEntity();

            e.Set(new HUDTimeSpeed());

            e.Set(new Body()
            {
                Position = new Vector2(6),
            });

            e.Set(new Text("Time Speed")
            {
                spriteFont = Program.DefaultFont,
                RenderTargetID = (int)RenderTargets.HUD,
                color = Color.White,
            });

            return e;
        }
    }
}

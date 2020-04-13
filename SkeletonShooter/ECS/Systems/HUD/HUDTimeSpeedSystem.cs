using BabelEngine4;
using BabelEngine4.ECS.Components.Rendering;
using BabelEngine4.ECS.Systems;
using BabelEngine4.Misc;
using DefaultEcs;
using SkeletonShooter.ECS.Components.HUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Systems.HUD
{
    public class HUDTimeSpeedSystem : SystemSkeleton
    {
        EntitySet entities;

        float TimeSpeed = -1;

        string TimeSpeedMessage = null;

        public override void Reset()
        {
            entities = App.world.GetEntities().With<HUDTimeSpeed>().With<Sprite>().With<Text>().AsSet();
        }

        public override void Update()
        {
            float TimeSpeedUse = (float)Math.Floor(Program.data.TimeSpeed * 100);

            if (TimeSpeed != TimeSpeedUse)
            {
                TimeSpeed = TimeSpeedUse;

                TimeSpeedMessage = "Time Speed: " + TimeSpeed + "%";
            }

            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref Sprite sprite = ref entity.Get<Sprite>();
                ref Text text = ref entity.Get<Text>();

                float Ymod = 8 / (Program.data.TimeSpeedLimit / Program.data.TimeSpeedCounter);

                sprite.Scale.Y = 8 - Ymod;
                text.Message = TimeSpeedMessage;
            }
        }
    }
}

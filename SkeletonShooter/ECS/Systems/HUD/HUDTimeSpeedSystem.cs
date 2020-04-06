using BabelEngine4;
using BabelEngine4.ECS.Components.Rendering;
using BabelEngine4.ECS.Systems;
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
            entities = App.world.GetEntities().With<HUDTimeSpeed>().With<Text>().AsSet();
        }

        public override void Update()
        {
            if (TimeSpeed == Program.data.TimeSpeed)
            {
                return;
            }

            TimeSpeed = Program.data.TimeSpeed;

            TimeSpeedMessage = "Time Speed: " + TimeSpeed;

            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref Text text = ref entity.Get<Text>();

                text.Message = TimeSpeedMessage;
            }
        }
    }
}

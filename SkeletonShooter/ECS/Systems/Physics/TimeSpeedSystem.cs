using BabelEngine4;
using BabelEngine4.ECS.Systems;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Systems.Physics
{
    public class TimeSpeedSystem : SystemSkeleton
    {
        public static float
            FullTime = 1f,
            PartTime = 0.1f,
            Step = 0.0005f
        ;

        public override void Update()
        {
            if (Program.data.CanTimeSpeed && App.input.gamepad1.Pressed(Buttons.LeftTrigger))
            {
                Program.data.TimeSpeed = (Program.data.TimeSpeed == FullTime ? PartTime : FullTime);
            }

            if (Program.data.TimeSpeed != FullTime)
            {
                if (Program.data.TimeSpeedCounter >= Program.data.TimeSpeedLimit)
                {
                    Program.data.CanTimeSpeed = false;
                    Program.data.TimeSpeed = FullTime;

                    return;
                }

                Program.data.TimeSpeed += Step;

                Program.data.TimeSpeedCounter = Math.Min(Program.data.TimeSpeedCounter + 1, Program.data.TimeSpeedLimit);
            }
            else
            {
                if (--Program.data.TimeSpeedCounter <= 0)
                {
                    Program.data.TimeSpeedCounter = 0;
                    Program.data.CanTimeSpeed = true;
                }
            }
        }
    }
}

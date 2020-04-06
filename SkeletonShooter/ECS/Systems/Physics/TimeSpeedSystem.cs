﻿using BabelEngine4;
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
        public override void Update()
        {
            if (Program.data.CanTimeSpeed && App.input.gamepad1.Pressed(Buttons.LeftTrigger))
            {
                Program.data.TimeSpeed = (Program.data.TimeSpeed == 1 ? 0.1f : 1);
            }

            if (Program.data.TimeSpeed != 1)
            {
                if (Program.data.TimeSpeedCounter >= Program.data.TimeSpeedLimit)
                {
                    Program.data.CanTimeSpeed = false;
                    Program.data.TimeSpeed = 1;

                    return;
                }

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

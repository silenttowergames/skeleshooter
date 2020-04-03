using BabelEngine4;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Systems;
using DefaultEcs;
using Microsoft.Xna.Framework.Input;
using SkeletonShooter.ECS.Components.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Systems.AI
{
    public class AIPlayerSystem : SystemSkeleton
    {
        EntitySet entities;

        public override void Reset()
        {
            entities = App.world.GetEntities().With<Director>().With<AIPlayer>().AsSet();
        }

        public override void Update()
        {
            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref Director director = ref entity.Get<Director>();

                director[Actions.MoveRight] = App.input.keyboard.Down(Keys.Right) || App.input.gamepad1.Down(Buttons.DPadRight);
                director[Actions.MoveLeft] = App.input.keyboard.Down(Keys.Left) || App.input.gamepad1.Down(Buttons.DPadLeft);
                director[Actions.Jump] = App.input.keyboard.Pressed(Keys.Up) || App.input.gamepad1.Pressed(Buttons.A);
            }
        }
    }
}

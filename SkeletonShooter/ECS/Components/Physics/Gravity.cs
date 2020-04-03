using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Components.Physics
{
    public struct Gravity
    {
        public static Gravity Default()
        {
            return new Gravity()
            {
                Acc = 0.25f,
                Max = 2f,
            };
        }

        public bool Jumping;

        public float
            Acc,
            Current,
            Max,

            Jump
        ;

        public int
            CoyoteCounter,
            CoyoteCounterMax
        ;
    }
}

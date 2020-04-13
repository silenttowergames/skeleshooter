using BabelEngine4.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Components.Shooting
{
    public struct Shoot
    {
        public Ticker Cooldown;

        public float Knockback;

        public Team team;
    }
}

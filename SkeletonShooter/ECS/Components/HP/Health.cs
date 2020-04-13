using BabelEngine4.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Components.HP
{
    public struct Health
    {
        int hp;

        public int HP
        {
            get
            {
                return hp;
            }

            set
            {
                if (!hitTicker.IsFinished(Program.data.TimeSpeed) && hitTicker.Current != 0)
                {
                    return;
                }

                hp = value;

                hitTicker.Reset();
                hitTicker.Update();
            }
        }

        public Team team;

        public Ticker hitTicker;
    }
}

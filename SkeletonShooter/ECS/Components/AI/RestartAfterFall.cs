using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.ECS.Components.AI
{
    public struct RestartAfterFall
    {
        public Vector2 LastSpot
        {
            get
            {
                return Spots[Spots.Length - 1];
            }

            set
            {
                if(Spots == null)
                {
                    Refill(value);

                    return;
                }

                for(int i = 1; i < Spots.Length; i++)
                {
                    Spots[i] = Spots[i - 1];
                }

                Spots[0] = value;
            }
        }

        Vector2[] Spots;

        public void Reset()
        {
            Refill(Spots[Spots.Length - 1]);
        }

        void Refill(Vector2 value)
        {
            Spots = new Vector2[32];

            for (int i = 0; i < Spots.Length; i++)
            {
                Spots[i] = value;
            }
        }
    }
}

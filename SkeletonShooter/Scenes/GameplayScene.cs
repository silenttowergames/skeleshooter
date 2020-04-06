using BabelEngine4;
using BabelEngine4.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonShooter.Scenes
{
    public class GameplayScene : TiledScene
    {
        public GameplayScene(string _Filename) : base(_Filename)
        {
        }

        public override void Load()
        {
            base.Load();

            App.Factories["hud-time"].Create(0, 0, 0);
        }
    }
}

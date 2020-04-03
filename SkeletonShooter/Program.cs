using BabelEngine4;
using BabelEngine4.Assets.Sprites;
using BabelEngine4.Assets.Tiled;
using BabelEngine4.ECS.Systems;
using BabelEngine4.Rendering;
using BabelEngine4.Scenes;
using DefaultEcs;
using Microsoft.Xna.Framework;
using SkeletonShooter.ECS.Entities;
using SkeletonShooter.ECS.Systems.AI;
using SkeletonShooter.ECS.Systems.Physics;
using System;

namespace SkeletonShooter
{
    public static class Animations
    {
        public static string
            // Enemy animations
            EnemyIdle = "enemy-idle",
            EnemyWalk = "enemy-walk",

            // Player animations
            PlayerIdle = "player-idle",
            PlayerWalk = "player-walk"
        ;
    }

    public enum RenderTargets
    {
        Main,
        HUD,
    }

    public enum Actions
    {
        None,
        MoveRight,
        MoveLeft,
        Jump,
    }

    public static class Program
    {
        public static Array ActionsValues = Enum.GetValues(typeof(Actions));

        [STAThread]
        static void Main()
        {
            Point
                resolution = new Point(320, 180),
                window = new Point(1280, 720)
            ;
            int HUDHeight = 20;

            using (App app = new App("Game Name", "v1.0.0", resolution, window))
            {
                //App.windowManager.RoundZoom = false;

                App.assets.addMaps(
                    new Map("skeleshooter-testmap")
                );

                App.assets.addShaders(
                    //new Shader("TestShader")
                );

                App.assets.addSprites(
                    new SpriteSheet("skeleshooter-16x16")
                );

                App.renderTargets = new RenderTarget[]
                {
                    new RenderTarget((int)RenderTargets.Main, resolution - new Point(0, HUDHeight), new Point(0, HUDHeight)) { BGColor = Color.Black },
                    new RenderTarget((int)RenderTargets.HUD, new Point(resolution.X, HUDHeight)) { BGColor = Color.Black },
                };

                App.systems = new IBabelSystem[]
                {
                    // Menu systems
                    // TODO: Add ChangeSelected event
                    new MenuSystem(),
                    new MenuItemSelectGoToSceneSystem(),

                    // Gravity before AI
                    //new GravitySystem(),

                    // AI systems
                    new AIPlayerSystem(),
                    // The last AI system!
                    new DirectorSystem(),

                    // Physics systems
                    new WalkingSystem(),
                    new GravitySystem(),
                    new AABBSystem(),

                    // Your post-movement systems
                    new CameraFollowSystem(),
                    new AnimationSystem(),
                    new MusicBasicSystem(),
                    new FlipSpriteOnXSystem(),
                };

                App.Scenes.Add("test", new TiledScene("skeleshooter-testmap"));

                App.Factories.Add("player", new PlayerFactory());

#if DEBUG
                App.SaveConfig = false;
#endif

                App.Scene = App.Scenes["test"];

                app.Run();
            }
        }
    }
}

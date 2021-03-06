﻿using BabelEngine4;
using BabelEngine4.Assets.Fonts;
using BabelEngine4.Assets.Shaders;
using BabelEngine4.Assets.Sprites;
using BabelEngine4.Assets.Tiled;
using BabelEngine4.ECS.Systems;
using BabelEngine4.Rendering;
using BabelEngine4.Scenes;
using DefaultEcs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkeletonShooter.ECS.Entities;
using SkeletonShooter.ECS.Systems.AI;
using SkeletonShooter.ECS.Systems.HP;
using SkeletonShooter.ECS.Systems.HUD;
using SkeletonShooter.ECS.Systems.Physics;
using SkeletonShooter.ECS.Systems.Shooting;
using SkeletonShooter.Scenes;
using System;

namespace SkeletonShooter
{
    /**
     * TODO LIST:
     * - Ability to slow down time
     * - Float?
     */

    public static class Animations
    {
        public static string
            // Enemy animations
            EnemyIdle = "enemy-idle",
            EnemyWalk = "enemy-walk",

            // Player animations
            PlayerIdle = "player-idle",
            PlayerWalk = "player-walk",
            PlayerShooting = "player-shooting",

            // Demon animations
            DemonIdle = "demon-idle"
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

        Run,

        Jump,
        JumpHold,

        Shoot,
    }

    public enum Team
    {
        GoodGuy,
        BadGuy,
    }

    public static class Program
    {
        public static Array ActionsValues = Enum.GetValues(typeof(Actions));

        public static GameData data = new GameData()
        {
            CanTimeSpeed = true,
            TimeSpeed = 1f,
            TimeSpeedLimit = 300,
        };

        public static string
            DefaultFont = "PressStart2P"
        ;

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

                App.assets.addFonts(
                    new Font(DefaultFont)
                );

                App.assets.addMaps(
                    new Map("skeleshooter-testmap")
                );

                App.assets.addShaders(
                    new Shader("SlowMotionShader", (Effect e, RenderTarget rt) =>
                    {
                        if (data.TimeSpeed != 1)
                        {
                            e.Parameters["SlowTime"].SetValue((1 - (1 / (data.TimeSpeedLimit / data.TimeSpeedCounter))) / 2);
                        }
                        else
                        {
                            e.Parameters["SlowTime"].SetValue(1f);
                        }
                    })
                );

                App.assets.addSprites(
                    new BlankSpriteSheet("blank"),
                    new SpriteSheet("skeleshooter-16x16")
                );

                App.renderTargets = new RenderTarget[]
                {
                    new RenderTarget((int)RenderTargets.Main, resolution - new Point(0, HUDHeight), new Point(0, HUDHeight))
                    {
                        BGColor = Color.Black,
                        shaders = new Shader[]
                        {
                            App.assets.shader("SlowMotionShader"),
                        },
                    },
                    new RenderTarget((int)RenderTargets.HUD, new Point(resolution.X, HUDHeight)) { BGColor = Color.Black },
                };

                App.systems = new IBabelSystem[]
                {
                    // Menu systems
                    // TODO: Add ChangeSelected event
                    new MenuSystem(),
                    new MenuItemSelectGoToSceneSystem(),

                    // Time systems
                    new TimeSpeedSystem(),
                    new BulletHitboxesSystem(),

                    // AI systems
                    new AIPlayerSystem(),
                    // The last AI system!
                    new DirectorSystem(),

                    // Physics systems
                    new HealthSystem(),
                    new BulletSystem(),
                    new ShootingSystem(),
                    new WalkingSystem(),
                    new GravitySystem(),
                    new MovingPlatformSystem(),
                    new AABBSystem(),

                    // Your post-movement systems
                    new RestartAfterFallSystem(),
                    new CameraFollowSystem(),
                    new AnimationSystem(),
                    new MusicBasicSystem(),
                    new FlipSpriteOnXSpecialSystem(),
                    new HUDTimeSpeedSystem(),
                };

                App.Scenes.Add("test", new GameplayScene("skeleshooter-testmap"));

                App.Factories.Add("hud-time", new TimeSpeedHUDFactory());
                App.Factories.Add("bullet", new BulletFactory());

                App.Factories.Add("player", new PlayerFactory());
                App.Factories.Add("demon", new DemonFactory());

#if DEBUG
                App.SaveConfig = false;
#endif

                App.Scene = App.Scenes["test"];

                app.Run();
            }
        }
    }
}

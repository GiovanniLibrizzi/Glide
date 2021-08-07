﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

using Glide.Content;
using System.Diagnostics;
using Glide.Content.Entities;

namespace Glide {
    public class Game1 : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public const int SCREEN_WIDTH = 320;
        public const int SCREEN_HEIGHT = 180;

        public const int wres2 = SCREEN_WIDTH * 2;
        public const int hres2 = SCREEN_HEIGHT * 2;
        public static int wScr = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int hScr = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        Rectangle CANVAS = new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);
        public static int ScreenWidth = SCREEN_WIDTH * 4;
        public static int ScreenHeight = SCREEN_HEIGHT * 4;
        private RenderTarget2D _renderTarget;


        World world;
        Camera camera;
        Player player;
        Texture2D tPlayer;
        SpriteFont font1;


        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = false;
            graphics.SynchronizeWithVerticalRetrace = true;


           /* graphics.PreferredBackBufferWidth = ScreenWidth;  
            graphics.PreferredBackBufferHeight = ScreenHeight;  
            graphics.ApplyChanges();*/

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        public void Resolution(int w, int h, bool b) {
            graphics.PreferredBackBufferWidth = w;
            graphics.PreferredBackBufferHeight = h;
            ScreenWidth = w;
            ScreenHeight = h;
            graphics.IsFullScreen = b;
            graphics.ApplyChanges();
        }
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();

            //world.addCollision(0);
        }

        protected override void LoadContent() {
            _renderTarget = new RenderTarget2D(GraphicsDevice, CANVAS.Width, CANVAS.Height);
            if (world != null)
                camera = new Camera(world.worldSize);
            else
                camera = new Camera(new Vector2Int(int.MaxValue, int.MaxValue));

            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            tPlayer = Content.Load<Texture2D>("Player");
            font1 = Content.Load<SpriteFont>("Font1");
            //Texture2D tile_main = Content.Load<Texture2D>("tileset_glide");
            /*world = new World("level1.json", Content);
            player = new Player(tPlayer, new Vector2(120, 20), world);
            world.scene.Add(player);*/

            /*world.scene = new List<Entity> {
                player,
                new NormalTile(tPlayer, new Vector2(190, 80), world)
            };*/

        }

        protected override void Update(GameTime gameTime) {
            Input.GetState();
            Input.GetMouseState();

            if (camera != null) 
                camera.Follow(player);

            float framerate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            Window.Title = "Game - " + System.String.Format("{0:0.00}", framerate) + " FPS";

            if (Input.keyPressed(Keys.F)) {
                if (graphics.IsFullScreen) {
                    Resolution(wres2, hres2, false);
                } else if (!graphics.IsFullScreen) {
                    Resolution(wScr, hScr, true);
                }
            }

            if (Input.keyPressed(Keys.F1)) {
                world = new World("level1.json", Content);
                player = new Player(tPlayer, new Vector2(120, 20), world);
                world.scene.Add(player);
                camera = new Camera(world.worldSize);
            }
            if (Input.keyPressed(Keys.F2)) {
                world = new World("level2.json", Content);
                player = new Player(tPlayer, new Vector2(120, 20), world);
                world.scene.Add(player);
            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (world != null) {
                foreach (Entity entity in world.scene) {
                    entity.Update(gameTime);
                    foreach (Component component in entity.components) {
                        component.Update(gameTime);
                    }
                }
            }
            
            /*TransformSystem.Update(gameTime);
            SpriteSystem.Update(gameTime);*/
            //ColliderSystem.Update(gameTime);

            Input.oldMouseState = Input.newMouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Debug.WriteLine("Test");

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.Transform);//, transformMatrix: _camera.Transform);

            if (world != null) {
                world.drawLevel(0, spriteBatch);
            }
            //spriteBatch.Begin();

            if (world != null) {
                foreach (Entity entity in world.scene) {
                    if (entity.HasComponent<Sprite>()) {
                        entity.GetComponent<Sprite>().Draw(spriteBatch);
                        //spriteBatch.Draw(entity.GetComponent<Sprite>().texture, entity.GetComponent<Transform>().position, Color.White);
                    }
                }
            }
           /* string test = "test";
            spriteBatch.DrawString(font1, , new Vector2(0, 0), Color.White, 0, new Vector2(0,0), 1.0f, SpriteEffects.None, 0.5f);*/

            spriteBatch.End();

            // Don't mess with
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
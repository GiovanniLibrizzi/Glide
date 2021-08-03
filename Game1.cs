using Microsoft.Xna.Framework;
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


        Scene scene = new Scene();


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
        }

        protected override void LoadContent() {
            _renderTarget = new RenderTarget2D(GraphicsDevice, CANVAS.Width, CANVAS.Height);
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D tPlayer = Content.Load<Texture2D>("Player");

            scene.scene = new List<Entity> {
                new Player(tPlayer, new Vector2(20, 20), scene),
                new NormalTile(tPlayer, new Vector2(20, 70), scene)
            };
        }

        protected override void Update(GameTime gameTime) {
            Input.GetState();
            Input.GetMouseState();

            if (Input.keyPressed(Keys.F)) {
                if (graphics.IsFullScreen) {
                    Resolution(wres2, hres2, false);
                } else if (!graphics.IsFullScreen) {
                    Resolution(wScr, hScr, true);
                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            foreach(Entity entity in scene.scene) {
                entity.Update(gameTime);
                foreach(Component component in entity.components) {
                    component.Update(gameTime);
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

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);//, transformMatrix: _camera.Transform);

            //spriteBatch.Begin();

            foreach (Entity entity in scene.scene) {
                //if (entity.HasComponent<Sprite>()) {
                    entity.GetComponent<Sprite>().Draw(spriteBatch);
                    //spriteBatch.Draw(entity.GetComponent<Sprite>().texture, entity.GetComponent<Transform>().position, Color.White);
                //}
            }
            
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

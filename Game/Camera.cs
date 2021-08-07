using Glide.Content.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glide.Content {
    class Camera {

        private Vector2Int worldSize;

        public static Vector2 pos1;

        public static Vector2Int mod = new Vector2Int(2, 3);

        public Matrix Transform { get; private set; }
        public Vector2 approach = new Vector2(0, 0);
        private float speed = 0.1f;
        private Vector2 minRange = new Vector2((-Game1.SCREEN_WIDTH / mod.x), -Game1.SCREEN_HEIGHT / mod.y);
       
        public Camera(Vector2Int worldSize) {
            this.worldSize = worldSize;
        }
        public void Follow(Actor target) {
            if (target == null) return;
            //approach.X = Math.Max(approach.X, worldSize.x-Game1.SCREEN_WIDTH/2);
            approach.X = Util.Lerp(approach.X, -target.position.X - (target.texture.Width / mod.x), speed);
            
            approach.Y = Util.Lerp(approach.Y, -target.position.Y - (target.texture.Height / mod.y), speed);

            //approach.X = Util.Clamp(approach.X, minRange.X, -(worldSize.x + minRange.X));
            approach.X = Math.Max(approach.X, -(worldSize.x + minRange.X));
            approach.X = Math.Min(approach.X, minRange.X);

            approach.Y = Math.Max(approach.Y, -worldSize.y);
            approach.Y = Math.Min(approach.Y, minRange.Y + minRange.Y);

            //Util.Log("approach.X: " + approach.X);
            //float xappr;

            var position = Matrix.CreateTranslation(
              approach.X,
              approach.Y,
              0);

            var offset = Matrix.CreateTranslation(
                Game1.SCREEN_WIDTH / mod.x,
                Game1.SCREEN_HEIGHT - Game1.SCREEN_HEIGHT / mod.y,
                0);

            //Console.WriteLine(position);
            pos1.X = position.M41;
            pos1.X = position.M42;

            Transform = position * offset;
        }
    }
}

using Glide.Game.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glide.Game {
    class Camera {

        private Vector2Int worldSize;

        public static Vector2 pos1;

        public static Vector2Int mod = new Vector2Int(2, 3);

        public Matrix Transform { get; private set; }
        public Vector2 approach = new Vector2(0, 0);
        private float speed = 0.1f;
        private float speedSlow = 0.01f;
        private Vector2 minRange = new Vector2((-Game1.SCREEN_WIDTH / mod.x), -Game1.SCREEN_HEIGHT / mod.y);
       
        public Camera(Vector2Int worldSize) {
            this.worldSize = worldSize;
        }
        public void Follow(Actor target) {
            // Camera moves vertically iff player on ground or wall
            if (target == null) return;
            if (target.GetType() == typeof(Player)) {
                Player p = (Player)target;
                if (p.touchingGround || p.state == Player.pState.WallClimb) {
                    approach.Y = Util.Lerp(approach.Y, -target.position.Y - (target.texture.Height / mod.y), speedSlow);
                }
            }
            
            // If player is too low or high, speed camera up
            if (target.position.Y > -(approach.Y) + 32 || target.position.Y < -approach.Y-Game1.SCREEN_HEIGHT) {
                approach.Y = Util.Lerp(approach.Y, -target.position.Y - (target.texture.Height / mod.y), speed);
            }

            approach.X = Util.Lerp(approach.X, -target.position.X - (target.texture.Width / mod.x), speed);
            //approach.Y = Util.Lerp(approach.Y, -target.position.Y - (target.texture.Height / mod.y), speed);

            // Clamp camera to map bounds
            approach.X = Math.Max(approach.X, -(worldSize.x + minRange.X));
            approach.X = Math.Min(approach.X, minRange.X);

            approach.Y = Math.Max(approach.Y, -worldSize.y - minRange.Y);
            approach.Y = Math.Min(approach.Y, minRange.Y + minRange.Y);


            var position = Matrix.CreateTranslation(
              approach.X,
              approach.Y,
              0);

            var offset = Matrix.CreateTranslation(
                Game1.SCREEN_WIDTH / mod.x,
                Game1.SCREEN_HEIGHT - Game1.SCREEN_HEIGHT / mod.y,
                0);

            pos1.X = position.M41;
            pos1.X = position.M42;

            Transform = position * offset;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glide.Content {
    class Sprite : Component {
        public Texture2D texture { get; set; }
        public Vector2 scale = new Vector2(1f, 1f);
        public SpriteEffects spriteEffect;

        public Sprite(Texture2D texture) {
            this.texture = texture;
            SpriteSystem.Register(this);
        }

        public void Scale(Vector2 sc) {
            scale = new Vector2(Math.Abs(sc.X), Math.Abs(sc.Y));

            // Flip if scale is less than 0
            if (sc.X < 0) {
                spriteEffect = SpriteEffects.FlipHorizontally;
            } else {
                spriteEffect = SpriteEffects.None;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            Transform t = entity.GetComponent<Transform>();

            if (texture != null)
                spriteBatch.Draw(texture, t.position, null, Color.White, 0f, Vector2.Zero, scale, spriteEffect, 0f);
            //Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale
        }

    }
}

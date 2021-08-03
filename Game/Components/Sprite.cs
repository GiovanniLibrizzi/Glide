using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glide.Content {
    class Sprite : Component {
        public Texture2D texture { get; set; }

        public Sprite(Texture2D texture) {
            this.texture = texture;
            SpriteSystem.Register(this);
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            Transform t = entity.GetComponent<Transform>();

            if (texture != null)
                spriteBatch.Draw(texture, t.position, Color.White);

        }

    }
}

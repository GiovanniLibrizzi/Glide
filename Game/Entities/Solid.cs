using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glide.Content.Entities {
    class Solid : Entity {
        public Vector2 position;
        public Texture2D texture { get; set; }
        public Transform transform;

        public Solid(Texture2D texture, Vector2 position, World scene) : base(scene) {
            this.texture = texture;
            this.position = position;

            transform = new Transform(position);
            AddComponent(transform);

            Sprite sprite = new Sprite(texture);
            AddComponent(sprite);
        }


    }
}

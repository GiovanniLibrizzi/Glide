using Glide.Content;
using Glide.Content.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glide.Content.Entities {
    class Collision : Solid {
        Vector2Int size;
        public Collision(Vector2 position, Vector2Int size, World world) : base(position, world) {
            this.size = size;
            rectangle = new Rectangle((int)position.X, (int)position.Y, size.x, size.y);
        }
    }
}

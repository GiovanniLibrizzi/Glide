using Glide.Content;
using Glide.Content.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glide.Content.Entities {
    class Collision : SolidNoTexture {
        public Collision(Vector2 position, Vector2Int size, World world) : base(position, size, world) {
            
        }
    }
}

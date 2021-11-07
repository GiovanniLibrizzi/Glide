﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glide.Game.Entities {
    class NormalTile : SolidTexture {
        public NormalTile(Texture2D texture, Vector2 position, World scene) : base(texture, position, scene) {

        }
    }
}

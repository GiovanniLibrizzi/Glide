using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glide.Content.Entities {
    class LevelTrigger : SolidNoTexture {
        public string worldNew;
        public LevelTrigger(string worldNew, Vector2 position, Vector2Int size, World worldCurrent) : base(position, size, worldCurrent) {
            this.worldNew = worldNew;
        }
    }
}

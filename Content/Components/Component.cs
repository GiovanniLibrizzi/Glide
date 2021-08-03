using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glide.Content {
    class Component {
        public Entity entity;
        public virtual void Update(GameTime gameTime) { }
    }
}

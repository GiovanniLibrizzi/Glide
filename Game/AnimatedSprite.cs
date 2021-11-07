using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glide.Content {
    class AnimatedSprite {
        public Texture2D texture;
        public Vector2Int frameSize;
        public int frameAmt;
        public int ticksPerFrame;
        public Rectangle frameRect;
        public int frameCurrent = 0;
        public int speed;

        public AnimatedSprite(Texture2D texture, Vector2Int frameSize, int speed) {
            this.texture = texture;
            this.frameSize = frameSize;
            this.speed = speed;
            frameAmt = texture.Width / frameSize.x;
            frameRect = new Rectangle(frameSize.x, frameSize.y, frameSize.x, frameSize.y);
        }

        public Rectangle GetRect(int tick) {
            // Update current frame
            if (speed != 0) {
                // Sprite Speed Management
                int tickspd = (int)(tick % (speed+Game1.FRAME_RATE/speed));
                if (tickspd == 0) frameCurrent++;
                if (frameCurrent == frameAmt) frameCurrent = 0;
            }
            
            // This will be the drawn portion of the sprite
            return new Rectangle((int)(frameSize.x * frameCurrent), 0, frameSize.x, frameSize.y);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Glide.Content.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Glide.Content {
    class Player : Actor {

        
       
        public Player(Texture2D texture, Vector2 pos, World scene) : base(texture, pos, scene) {
            mspd = 2.0f;
            jspd = 5.0f;
            friction = 0.1f;
        }

        public override void Update(GameTime gameTime) { 

            // debug reset
            if (Input.keyPressed(Keys.R)) {
                position = new Vector2(20, 20);
                velocity = new Vector2(0, 0);
            }


            // Movement Controls
            if (Input.keyDown(Input.Right)) {
                Move(Dir.Right);
            }

            if (Input.keyDown(Input.Left)) {
                Move(Dir.Left);
            }
            if (!Input.keyDown(Input.Right) && !Input.keyDown(Input.Left)) {
                Move(Dir.Stop);
            }

            if (Input.keyPressed(Input.Jump) && onGround ) {
                System.Diagnostics.Debug.WriteLine("JUMP!");
                Jump();
            }

            // Jump height variation
            if (velocity.Y < 0) {
                if (!Input.keyPressed(Input.Jump)) {
                    velocity.Y = Util.Lerp(velocity.Y, 0, 0.05f);
                }
                if (Input.keyReleased(Input.Jump)) {
                    velocity.Y = Util.Lerp(velocity.Y, 0, 0.5f);
                }
            }

            Gravity();

            CollisionStop();


            position += velocity;
           
            transform.position = position;

        }


    }
}

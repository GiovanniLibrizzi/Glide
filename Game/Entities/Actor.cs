using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Glide.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Glide.Content.Entities {
    class Actor : Entity {
        public Vector2 position;
        public Texture2D texture { get; set; }
        public Transform transform;

        public Vector2 velocity;

        public float friction;
        public float mspd { get; set; }
        public float jspd;
        public float gravity = 0.1f;

        public bool onGround;

        public enum Dir {
            Stop,
            Right,
            Left,
            Top,
            Bottom
        }




        public Actor(Texture2D texture, Vector2 pos, World scene) : base(scene) {

            position = pos;
            this.texture = texture;


            transform = new Transform(position);
            AddComponent(transform);

            Sprite sprite = new Sprite(texture);
            AddComponent(sprite);
        }

        // Moves actor downwards
        public void Gravity() {
            if (velocity.Y < 10) {
                velocity.Y += gravity;
            }
        }

        // Stops actor at collision objects 
        public void CollisionStop() {
            bool groundCheck = false;
            foreach (Solid s in world.scene.OfType<Solid>()) {
                if ((velocity.X > 0 && IsTouching(s, Dir.Left)) ||
                    (velocity.X < 0 && IsTouching(s, Dir.Right))) velocity.X = 0;

                if (IsTouching(s, Dir.Top)) {
                    groundCheck = true;
                }

                if ((velocity.Y > 0 && IsTouching(s, Dir.Top)) ||
                    (velocity.Y < 0 && IsTouching(s, Dir.Bottom))) velocity.Y = 0;

            }
            onGround = groundCheck;
        }

        // Moves actor the direction specified
        public void Move (Dir move) {
            switch (move) {
                case Dir.Right: MoveRight(); break;
                case Dir.Left: MoveLeft(); break;
                case Dir.Stop: StopMoving(); break;
                default: StopMoving(); break;
            }
        }
        public void MoveRight() {
            if (velocity.X < mspd - friction) velocity.X += friction;
            else velocity.X = mspd;
        }

        public void MoveLeft() {
            if (velocity.X > -mspd + friction) velocity.X -= friction;
            else velocity.X = -mspd;
        }

        public void StopMoving() {
            if (velocity.X >= friction) velocity.X -= friction;
            else if (velocity.X <= -friction) velocity.X += friction;
            else if (velocity.X > -friction && velocity.X < friction) velocity.X = 0;
        }

        public void Jump() {
            velocity.Y = -jspd;
            onGround = false;
        }


        // Checks if actor is touching an entity on a certain side of said entity
        public bool IsTouching(Entity entity, Dir dir) {
            Rectangle entRect;
            Sprite sprite = entity.GetComponent<Sprite>();
            Rectangle myRect = texture.Bounds;
            Vector2 entPos = entity.GetComponent<Transform>().position;

            if (entity.HasComponent<Sprite>()) {
                entRect = sprite.texture.Bounds;

            } else {
  
                Collision s = (Collision)entity;
                entRect = new Rectangle(0, 0, s.rectangle.Width, s.rectangle.Height);
            }

            switch (dir) {
                case Dir.Left:
                    return position.X + myRect.Right + velocity.X > entPos.X + entRect.Left &&
                       position.X + myRect.Left < entPos.X + entRect.Left &&
                       position.Y + myRect.Bottom > entPos.Y + entRect.Top &&
                       position.Y + myRect.Top+1 < entPos.Y + entRect.Bottom;
                case Dir.Right:
                    return position.X + (myRect.Left) + velocity.X < entPos.X + entRect.Right &&
                       position.X + myRect.Right > entPos.X + entRect.Right &&
                       position.Y + myRect.Bottom > entPos.Y + entRect.Top &&
                       position.Y + myRect.Top+1 < entPos.Y + entRect.Bottom;

                case Dir.Top:
                    return position.Y + myRect.Bottom + velocity.Y > entPos.Y + entRect.Top &&
                        position.Y + myRect.Top+1 < entPos.Y + entRect.Top &&
                        position.X + myRect.Right > entPos.X + entRect.Left &&
                        position.X + myRect.Left < entPos.X + entRect.Right;
                case Dir.Bottom:
                    return position.Y + (myRect.Top+1) + velocity.Y < entPos.Y + (entRect.Bottom-1) &&
                        position.Y + myRect.Bottom > entPos.Y + entRect.Bottom &&
                        position.X + myRect.Right > entPos.X + entRect.Left &&
                        position.X + myRect.Left < entPos.X + entRect.Right;

                default: return false;

            }
        }


    }
}

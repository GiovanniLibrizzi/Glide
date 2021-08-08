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

        protected Vector2 velocity;

        protected float friction;
        protected float mspd { get; set; }
        protected float jspd;
        protected float gravity = 0.15f;
        protected const float gravityDef = 0.15f;

        protected bool touchingGround;
        protected bool touchingWall;
        protected bool touchingClimbable;


        protected enum Dir {
            Stop = 0,
            Right = 1,
            Left = -1,
            Up = 2,
            Down = 3,
            Top = 4,
            Bottom = 5

        }

        protected Dir direction;




        public Actor(Texture2D texture, Vector2 pos, World scene) : base(scene) {

            position = pos;
            this.texture = texture;


            transform = new Transform(position);
            AddComponent(transform);

            Sprite sprite = new Sprite(texture);
            AddComponent(sprite);
        }

        // Moves actor downwards
        protected void Gravity() {
            if (velocity.Y < 10) {
                velocity.Y += gravity;
            }
        }

        // Stops actor at collision objects 
        protected void CollisionStop() {
            touchingGround = false;
            touchingWall = false;
            touchingClimbable = false;

            foreach (Collision s in world.scene.OfType<Collision>()) {
                if ((velocity.X > 0 && IsTouching(s, Dir.Left)) || (velocity.X < 0 && IsTouching(s, Dir.Right))) {
                    velocity.X = 0;
                    touchingWall = true;
                    if (s.GetType() == typeof(Climbable)) {
                        touchingClimbable = true;
                    }
                }

                if (IsTouching(s, Dir.Top)) {
                    touchingGround = true;
                }

                if ((velocity.Y > 0 && IsTouching(s, Dir.Top)) ||
                    (velocity.Y < 0 && IsTouching(s, Dir.Bottom))) velocity.Y = 0;

            }
        }



        // Moves actor the direction specified
        protected void Move (Dir move, float mspd) {
            switch (move) {
                case Dir.Right: MoveRight(mspd); break;
                case Dir.Left: MoveLeft(mspd); break;
                case Dir.Up: MoveUp(mspd); break;
                case Dir.Down: MoveDown(mspd); break;
                case Dir.Stop: StopMoving(); break;
                default: StopMoving(); break;
            }
        }
        private void MoveRight(float mspd) {
            if (velocity.X < mspd - friction) velocity.X += friction;
            else velocity.X = mspd;
        }

        private void MoveLeft(float mspd) {
            if (velocity.X > -mspd + friction) velocity.X -= friction;
            else velocity.X = -mspd;
        }

        private void MoveUp(float mspd) {
            if (velocity.Y > -mspd + friction) velocity.Y -= friction;
            else velocity.Y = -mspd;
        }

        private void MoveDown(float mspd) {
            if (velocity.Y < mspd - friction) velocity.Y += friction;
            else velocity.Y = mspd;
        }

        protected void StopMoving() {
            if (velocity.X >= friction) velocity.X -= friction;
            else if (velocity.X <= -friction) velocity.X += friction;
            else if (velocity.X > -friction && velocity.X < friction) velocity.X = 0;
        }

        protected void Jump(float jspd) {
            velocity.Y = -jspd;
            touchingGround = false;
        }


        // Checks if actor is touching an entity on a certain side of said entity
        protected bool IsTouching(Entity entity, Dir dir) {
            Rectangle entRect;
            Sprite sprite = entity.GetComponent<Sprite>();
            Rectangle myRect = texture.Bounds;
            Vector2 entPos = entity.GetComponent<Transform>().position;

            if (entity.HasComponent<Sprite>()) {
                entRect = sprite.texture.Bounds;

            } else {
                
                SolidNoTexture s = (SolidNoTexture)entity;
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

        public static bool Colliding(Vector2 posA, Rectangle boxA, Vector2 posB, Rectangle boxB) {
            return posA.X + boxA.Left < posB.X + boxB.Right &&
                    posA.X + boxA.Right > posB.X + boxB.Left &&
                    posA.Y + boxA.Top < posB.Y + boxB.Bottom &&
                    posA.Y + boxA.Bottom > posB.Y + boxB.Top;
        }

        protected void FlipDirection() {
            if (direction == Dir.Left) direction = Dir.Right; else direction = Dir.Left;
        }

    }
}

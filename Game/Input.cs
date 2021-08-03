using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glide.Content {
    public class Input {
        public static Keys Up;
        public static Keys Down;
        public static Keys Left = Keys.A;
        public static Keys Right = Keys.D;
        public static Keys Jump = Keys.K;


        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;

        public static MouseState mouseState;
        public static MouseState oldMouseState;
        public static MouseState newMouseState;
        public Vector2 mPos;
        public int mx;
        public int my;



        public static KeyboardState GetState() {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            return currentKeyState;
        }

        public static bool keyDown(Keys key) {

            return currentKeyState.IsKeyDown(key);
        }

        public static bool keyPressed(Keys key) {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }

        public static bool keyReleased(Keys key) {
            return previousKeyState.IsKeyDown(key) && !currentKeyState.IsKeyDown(key);
        }


        public static MouseState GetMouseState() {
            mouseState = Mouse.GetState();
            newMouseState = Mouse.GetState();
            //mx = curMouseState.X;
            return mouseState;
        }


        // 0 = left, 1 = right (maybe make enum)
        public static bool Click(int a) {
            switch (a) {
                case 0: return newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released;
                case 1: return newMouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released;
            }
            return false;
        }

        public static bool MouseHold(int a) {
            switch (a) {
                case 0: return newMouseState.LeftButton == ButtonState.Pressed;
                case 1: return newMouseState.RightButton == ButtonState.Pressed;
            }
            return false;
        }


    }
}

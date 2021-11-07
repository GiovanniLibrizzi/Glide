﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glide.Game {
    public class Input {
        

        //public static Btn Up = new Btn(new List<Keys>() { Keys.W, Keys.Up }, new List<Buttons>() { Buttons.LeftThumbstickUp, Buttons.DPadUp });
        public static Btn Up = new Btn(Keys.W, Keys.Up, Buttons.LeftThumbstickUp, Buttons.DPadUp);
        public static Btn Down = new Btn(Keys.S, Keys.Down, Buttons.LeftThumbstickDown, Buttons.DPadDown);
        public static Btn Left = new Btn(Keys.A, Keys.Left, Buttons.LeftThumbstickLeft, Buttons.DPadLeft);
        public static Btn Right = new Btn(Keys.D, Keys.Right, Buttons.LeftThumbstickRight, Buttons.DPadRight);
        public static Btn Jump = new Btn(Keys.K, Keys.Space, Buttons.A);


        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;

        static GamePadState currentButtonState;
        static GamePadState previousButtonState;

        public static MouseState mouseState;
        public static MouseState oldMouseState;
        public static MouseState newMouseState;
        public Vector2 mPos;
        public int mx;
        public int my;



        //public static KeyboardState GetState() {
        public static void GetState() { 
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            previousButtonState = currentButtonState;
            currentButtonState = GamePad.GetState(PlayerIndex.One);
            
            //return currentKeyState;
        }

        public static bool keyDown(Btn btn) {
            bool down = false;
            foreach (Keys key in btn.keys) {
                if (keyDown(key)) return true;
            }
            foreach (Buttons button in btn.buttons) {
                if (keyDown(button)) return true;
            }
            return false;
        }
        public static bool keyDown(Keys key) {

            return currentKeyState.IsKeyDown(key);
        }
        public static bool keyDown(Buttons button) {
            return currentButtonState.IsButtonDown(button);
        }

        public static bool keyPressed(Btn btn) {
            bool down = false;
            foreach (Keys key in btn.keys) {
                if (keyPressed(key)) return true;
            }
            foreach (Buttons button in btn.buttons) {
                if (keyPressed(button)) return true;
            }
            return false;
        }
        public static bool keyPressed(Keys key) {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }
        public static bool keyPressed(Buttons button) {
            return currentButtonState.IsButtonDown(button) && !previousButtonState.IsButtonDown(button);
        }


        public static bool keyReleased(Btn btn) {
            bool down = false;
            foreach (Keys key in btn.keys) {
                if (keyReleased(key)) return true;
            }
            foreach (Buttons button in btn.buttons) {
                if (keyReleased(button)) return true;
            }
            return false;
        }
        public static bool keyReleased(Keys key) {
            return previousKeyState.IsKeyDown(key) && !currentKeyState.IsKeyDown(key);
        }
        public static bool keyReleased(Buttons button) {
            return previousButtonState.IsButtonDown(button) && !currentButtonState.IsButtonDown(button);
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


        // Button struct
        public struct Btn {

            public List<Keys> keys;
            public List<Buttons> buttons;

            public Btn(List<Keys> keys, List<Buttons> buttons) {
                this.keys = keys;
                this.buttons = buttons;
            }

            public Btn(Keys key, Buttons button) {
                keys = new List<Keys>() { key };
                buttons = new List<Buttons>() { button };
            }

            public Btn(Keys key1, Keys key2, Buttons button1, Buttons button2) {
                keys = new List<Keys>() { key1, key2 };
                buttons = new List<Buttons>() { button1, button2 };
            }
            public Btn(Keys key1, Buttons button1, Buttons button2) {
                keys = new List<Keys>() { key1 };
                buttons = new List<Buttons>() { button1, button2 };
            }

            public Btn(Keys key1, Keys key2, Buttons button1) {
                keys = new List<Keys>() { key1, key2 };
                buttons = new List<Buttons>() { button1 };
            }

        }
    }
}

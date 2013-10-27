using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Kupiakos.KEngine
{
    public static class Input
    {

        /// <summary>
        /// Whether the input stack is initialized or not.
        /// </summary>
        private static bool isInit = false;

        /// <summary>
        /// The current state of the keyboard.
        /// </summary>
        public static KeyboardState KeyState { get; private set; }

        /// <summary>
        /// The state of the keyboard in the last frame.
        /// </summary>
        public static KeyboardState KeyLastState { get; private set; }

        /// <summary>
        /// The current state of the mouse.
        /// </summary>
        public static MouseState MouseState { get; private set; }

        /// <summary>
        /// The state of the mouse in the last frame.
        /// </summary>
        public static MouseState MouseLastState { get; private set; }

        public static GamePadState PadLastState { get; private set; }

        /// <summary>
        /// Initialize the state of this input manager.
        /// You cannot call any functions until this is called.
        /// </summary>
        public static void Initialize()
        {
            KeyState = Keyboard.GetState();
            KeyLastState = KeyState;

            MouseState = Mouse.GetState();
            MouseLastState = Mouse.GetState();
            
            isInit = true;
        }

        /// <summary>
        /// Update the input states for a frame.
        /// </summary>
        public static void Update()
        {
            KeyLastState = KeyState;
            KeyState = Keyboard.GetState();

            MouseLastState = MouseState;
            MouseState = Mouse.GetState();
        }

        /// <summary>
        /// Determines if the given keyboard key is currently depressed.
        /// </summary>
        /// <returns><c>true</c> if is the key is currently depressed; otherwise, <c>false</c>.</returns>
        /// <param name="key">The key to check</param>
        public static bool IsKeyDown(Keys key)
        {
            return KeyState.IsKeyDown(key);
        }

        /// <summary>
        /// Determines if the given keyboard key is not depressed.
        /// </summary>
        /// <returns><c>true</c> if is key is currently not depressed; otherwise, <c>false</c>.</returns>
        /// <param name="key">The key to check</param>
        public static bool IsKeyUp(Keys key)
        {
            return KeyState.IsKeyUp(key);
        }

        /// <summary>
        /// Determines if the given keyboard key has been depressed since the last frame, i.e. the key has just been pressed.
        /// This will only return <c>true</c> once until the keyboard key is released and pressed again.
        /// </summary>
        /// <returns><c>true</c> if is key has just been pressed; otherwise, <c>false</c>.</returns>
        /// <param name="key">The key to check</param>
        public static bool IsKeyPressed(Keys key)
        {
            return (KeyState.IsKeyDown(key) && KeyLastState.IsKeyUp(key));
        }

        /// <summary>
        /// Determines if the given keyboard key has stopped being depressed since the last frame, i.e. the key has just been released.
        /// This will only return <c>true</c> once until the keyboard key is pressed and released again.
        /// </summary>
        /// <returns><c>true</c> if is key has just been released; otherwise, <c>false</c>.</returns>
        /// <param name="key">The key to check</param>
        public static bool IsKeyReleased(Keys key)
        {
            return (KeyState.IsKeyUp(key) && KeyLastState.IsKeyDown(key));
        }



    }
}


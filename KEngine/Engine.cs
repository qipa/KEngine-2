using System;
using Microsoft.Xna.Framework;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Kupiakos.KEngine
{
    /// <summary>
    /// The Game Manager class manages everything high level in a game -
    /// Scenes that are created, controls to stop and start the game, amongst other
    /// things.
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// The game that created this Engine.
        /// </summary>
        /// <value>The game.</value>
        public Game Game { get; private set; }

        /// <summary>
        /// The SpriteManager registered here.
        /// </summary>
        /// <value>The sprites.</value>
        public SpriteManager Sprites { get; private set; }

        /// <summary>
        /// The Scene that is currently in view.
        /// </summary>
        /// <value>The current scene.</value>
        public Scene CurrentScene { get; private set; }


       

        public Engine(Game game)
        {
            this.Game = game;
            Sprites = new SpriteManager(Game);
            KInput.Initialize();
        }

        public void SwitchScene<T>() where T : Scene
        {
            if (CurrentScene != null)
                this.Game.Components.Remove(CurrentScene);
            this.CurrentScene = (T)Activator.CreateInstance(typeof(T), this);
//            this.CurrentScene.Initialize();
            this.Game.Components.Add(CurrentScene);
        }


        public void Exit()
        {
            this.Game.Exit();
        }



    }

}


using System;
using Microsoft.Xna.Framework;

namespace Kupiakos.KEngine
{
    /// <summary>
    /// The Game Manager class manages everything high level in a game -
    /// Scenes that are created, controls to stop and start the game, amongst other
    /// things.
    /// </summary>
    public class Engine
    {
        public Game Game { get; private set; }
        public SpriteManager Sprites { get; private set; }
        public Scene CurrentScene { get; private set; }

        public Engine(Game game)
        {
            this.Game = game;
            Sprites = new SpriteManager(Game);
            Input.Initialize();
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


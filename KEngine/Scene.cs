using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Kupiakos.KEngine
{
    public abstract class Scene : DrawableGameComponent
    {
        public Engine Engine { get; private set; }

        public SpriteBatch SpriteBatch { get; set; }

        private GameComponentCollection items;

        public IEnumerable<GameComponent> Items { get { return items.OfType<GameComponent>(); } }

        public IEnumerable<Entity> Entities { get { return items.OfType<Entity>(); } }

        private IEnumerable<DrawableGameComponent> Drawables { get { return items.OfType<DrawableGameComponent>(); } }

        public Rectangle Dimensions { get; private set; }

        public int Width { get { return Dimensions.Width; } }

        public int Height { get { return Dimensions.Height; } }

        public Rectangle View { get; private set; }

        public Scene(Engine engine) : base(engine.Game)
        {
            this.Engine = engine;
            this.SpriteBatch = new SpriteBatch(engine.Game.GraphicsDevice);
            this.items = new GameComponentCollection();
        }

        public void AddEntity(Entity e)
        {
            this.items.Add(e);
        }

        /// <summary>
        /// Creates an Entity in the Scene and returns it.
        /// The Entity is also initialized.
        /// </summary>
        /// <returns>The Entity Type to create.</returns>
        /// <param name="position">The position of the Entity when it is created.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T CreateEntity<T>(Vector2 position) where T : Entity
        {
            T e = (T)Activator.CreateInstance(typeof(T), this);
            e.Position = position;
            this.items.Add(e);
            e.Initialize();
            return e;
        }

        public T CreateEntity<T>() where T : Entity
        {
            return this.CreateEntity<T>(Vector2.Zero);
        }

        /// <Docs>To be added.</Docs>
        /// <summary>
        /// Initialize this Scene. 
        /// The base.Initialize() call should be done before anything else in any subclasses.
        /// </summary>
        public override void Initialize()
        {
            Dimensions = Game.GraphicsDevice.PresentationParameters.Bounds;
            View = Dimensions;
            base.Initialize();
        }

        /// <summary>
        /// Returns all Entities of type T in this scene.
        /// </summary>
        /// <returns>The entity.</returns>
        /// <typeparam name="T">The Entity type to search for</typeparam>
        public IEnumerable<T> FindEntity<T>() where T : Entity
        {
            return items.OfType<T>();
        }

        /// <summary>
        /// Find an entity in the this scene of type T, matching the function provided.
        /// </summary>
        /// <returns>The entity.</returns>
        /// <param name="f">F.</param>
        /// <typeparam name="T">The type of Entity to search for</typeparam>
        public IEnumerable<T> FindEntity<T>(Func<T, bool> f) where T : Entity
        {
            return FindEntity<T>().Where<T>(f);
        }

        /// <summary>
        /// Find all entities in the scene that collide with the rectangle.
        /// </summary>
        /// <returns>The collisions.</returns>
        /// <param name="rect">Rect.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public IEnumerable<T> FindCollisions<T>(Rectangle rect) where T : Entity
        {
            // Until the QuadTree implementation is completely finished, this is the best we got.
            return FindEntity<T>((e => rect.Intersects(e.BoundingBox)));
        }

        /// <Docs>To be added.</Docs>
        /// <remarks>To be added.</remarks>
        /// <summary>
        /// Called with each frame of the game.
        /// Override to define your own, but make sure to call the base!
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public override void Update(GameTime gameTime)
        {
            KInput.Update();
            foreach (GameComponent g in Items)
                g.Update(gameTime);

            base.Update(gameTime);
        }


        /// <Docs>To be added.</Docs>
        /// <remarks>To be added.</remarks>
        /// <summary>
        /// Called to draw each frame of the game.
        /// This should be callable at any moment and should never store any
        /// lasting information.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            foreach (DrawableGameComponent g in Drawables)
                g.Draw(gameTime);

            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


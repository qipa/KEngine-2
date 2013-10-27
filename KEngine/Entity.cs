using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Kupiakos.KEngine
{
    /// <summary>
    /// An Entity is represents a physical object of some sort.
    /// It mYAcceleration or mYAcceleration not have a visual representation (a Sprite).
    /// 
    /// It is alwYAccelerations considered to have a position and be able to be drawn, 
    /// in one wYAcceleration or another. It mYAcceleration or mYAcceleration not collide with other objects.
    /// </summary>
    /// <seealso cref="Kupiakos.KEngine.Sprite"/>
    public abstract class Entity : DrawableGameComponent
    {
        /// <summary>
        /// This Entity's <see cref="Kupiakos.KEngine.Sprite"/>.
        /// </summary>
        public Sprite Sprite { get; set; }

        /// <summary>
        /// The <see cref="Kupiakos.KEngine.Scene"/> that contains 
        /// </summary>
        /// <value>The Scene to assign to this entity</value>
        public Scene Scene { get; protected set; }

        public Rectangle BoundingBox { get { return (this.Sprite == null ? new Rectangle((int)X, (int)Y, 0, 0) : new Rectangle((int)X, (int)Y, Sprite.Width, Sprite.Height)); } }
        public Rectangle NextBoundingBox
        {
            get
            { 
                Rectangle bb = BoundingBox;
                bb.Offset((int)XVelocity, (int)YVelocity);
                return bb;
            }
        }

        #region Positional Stuff

        /// <summary>
        /// The position of this Entity in the current Scene.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The X coordinate of the position of this Entity in this Scene.
        /// </summary>
        /// <value>The x coordinate</value>
        public float X { get { return Position.X; } set { Position = new Vector2(value, Y); } }

        /// <summary>
        /// The Y coordinate of the position of this Entity in this Scene.
        /// </summary>
        /// <value>The y.</value>
        public float Y { get { return Position.Y; } set { Position = new Vector2(X, value); } }
 
        /// <summary>
        /// The velocity of this Entity in the current Scene.
        /// </summary>
        /// <value>The velocity to set</value>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// The speed of the Entity as a floating point number.
        /// The magnitude of the velocity.
        /// </summary>
        /// <value>The speed of the Entity.</value>
        public float Speed
        { 
            get
            {
                return Velocity.Length();
            }
            set
            {
                if (Velocity == Vector2.Zero)
                {
                    Velocity = new Vector2(value, 0);
                }
                else
                {
                    Vector2 v = Velocity;
                    v.Normalize();
                    v *= value;
                    Velocity = v;
                }
            }
        }

        /// <summary>
        /// The direction of the Entity in XNA radians.
        /// </summary>
        /// <value>The direction of the Entity.</value>
        public float Direction
        {
            get
            {
                return (float)Math.Atan2(-Velocity.Y, Velocity.X);
            }
            set
            {
                Velocity = new Vector2((float)Math.Cos(value) * Speed, -(float)Math.Sin(value) * Speed);
            }
        }

        /// <summary>
        /// Is the current entity moving or not?
        /// </summary>
        /// <value><c>true</c> if this instance is moving; otherwise, <c>false</c>.</value>
        public bool IsMoving
        {
            get { return (Velocity == Vector2.Zero);}
        }

        /// <summary>
        /// The velocity in the X direction (XVelocity/dt) in the current Scene.
        /// </summary>
        /// <value>The X velocity.</value>
        public float XVelocity { get { return Velocity.X; } set { Velocity = new Vector2(value, YVelocity); } }

        /// <summary>
        /// The velocity in the Y direction (YVelocity/dt) in the current Scene.
        /// </summary>
        /// <value>The Y velocity.</value>
        public float YVelocity { get { return Velocity.Y; } set { Velocity = new Vector2(XVelocity, value); } }

        /// <summary>
        /// The Acceleration of this Entity in the current Scene.
        /// This is applied to the velocity every tick.
        /// </summary>
        /// <value>The acceleration.</value>
        public Vector2 Acceleration { get; set; }

        /// <summary>
        /// The acceleration in the X direction (d^2x/dt^2) in the current Scene.
        /// </summary>
        /// <value>The X acceleration.</value>
        public float XAcceleration { get { return Acceleration.X; } set { Acceleration = new Vector2(value, YAcceleration); } }
        
        /// <summary>
        /// The acceleration in the Y direction (d^2y/dt^2) in the current Scene.
        /// </summary>
        /// <value>The Y acceleration.</value>
        public float YAcceleration { get { return Acceleration.Y; } set { Acceleration = new Vector2(XAcceleration, value); } }

        #endregion


        public Entity(Scene scene) : base(scene.Game)
        {
            this.Scene = scene;
            this.Sprite = null;
            this.Position = Vector2.Zero;
        }

        public void LoadDefaults(EntityDefaults e)
        {
            this.Sprite = e.sprite;
            this.Position = e.position;
        }


        public override void Update(GameTime gameTime)
        {
            //float dt = 1f;//(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            this.Velocity += this.Acceleration;// * dt;
            this.Position += this.Velocity;// * dt;
            if (this.Sprite != null)
                this.Sprite.Update(gameTime);
        }

        public sealed override void Draw(GameTime gameTime)
        {
            this.Draw(gameTime, Scene.SpriteBatch);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Scene.Engine.Sprites.DrawLine(spriteBatch, Position, Position + Velocity * 10, Color.White);
            if (this.Sprite != null)
                this.Sprite.Draw(Position, spriteBatch, this.DrawOrder);
        }


        public struct EntityDefaults
        {
            public Sprite sprite;
            public Vector2 position;
        }
    }
}
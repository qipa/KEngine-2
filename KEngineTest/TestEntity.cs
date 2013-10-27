using System;
using Kupiakos.KEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KEngineTest
{
    public class TestEntity : Entity
    {
        public TestEntity(Scene scene) : base(scene)
        {
        }


        public override void Initialize()
        {
            this.Sprite = new Sprite(Game.Content.Load<Texture2D>("ball"), new Vector2(8, 8));
            //this.YAcceleration = .2f;
            //this.XVelocity = (float)((new Random()).NextDouble() * 20 - 10);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //KeyboardState k = Keyboard.GetState();

            if (KInput.IsKeyDown(Keys.S))
            {
                Speed = 6f;
                YAcceleration = 0f;
            }
            if (KInput.IsKeyDown(Keys.Left))
                Direction += .1f;
            if (KInput.IsKeyDown(Keys.Right))
                Direction -= .1f;
            if (KInput.IsKeyDown(Keys.Down))
                YAcceleration = .2f;

            if (!Scene.Dimensions.Contains(NextBoundingBox))
            {
                // Lookup Graphics Viewport about XNA on the interwebs
                if (BoundingBox.Left < 0 || 
                    BoundingBox.Right > Scene.Width)
                    XVelocity *= -1;
                if (BoundingBox.Top < 0 ||
                    BoundingBox.Bottom > Scene.Height)
                {
                    YVelocity *= -1;
                    if (YVelocity < .001f && YVelocity > -.001f)
                    {
                        YVelocity = 0;
                        Y = Scene.Height - (Sprite.Height - Sprite.Origin.Y);
                    }
                }
                if (!Scene.Dimensions.Intersects(NextBoundingBox))
                    Y = 50;
                
            }

            base.Update(gameTime);
        }
    }
}


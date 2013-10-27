using System;
using Kupiakos.KEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KEngineTest
{
    public class TestScene : Scene
    {
        public TestScene(Engine engine) : base(engine)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            int n = 0;
            Random r = new Random();

            while (n<5)
            {
                this.CreateEntity<TestEntity>(new Vector2(r.Next(16, Dimensions.Width - 16), r.Next(16, Dimensions.Height - 16)));
                n++;
            }
        }


        public override void Update(GameTime gameTime)
        {
            if (KInput.IsKeyPressed(Keys.Escape))
                Engine.Exit();

            base.Update(gameTime);
        }
    }
}


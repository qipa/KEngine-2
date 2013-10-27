using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kupiakos.KEngine
{
    public class SpriteManager
    {
        public Game Game { get; set; }
        public static Texture2D WhitePixel;

        public SpriteManager(Game game)
        {
            this.Game = game;
            WhitePixel = new Texture2D(this.Game.GraphicsDevice, 1, 1);
            WhitePixel.SetData<uint>(new uint[] {0xffffffff});
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            Vector2 diff = end - start;

            float angle = (float)Math.Atan2(diff.Y, diff.X);
            float scale = (float)diff.Length();

            spriteBatch.Draw(WhitePixel, start, new Rectangle(0, 0, 1, 1), 
                             color, angle, Vector2.Zero, new Vector2(scale, 1), SpriteEffects.None, 0);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rect)
        {
            DrawLine(spriteBatch, new Vector2(rect.Left, rect.Top), new Vector2(rect.Right, rect.Top), Color.White);
            DrawLine(spriteBatch, new Vector2(rect.Left, rect.Top), new Vector2(rect.Left, rect.Bottom), Color.White);
            DrawLine(spriteBatch, new Vector2(rect.Left, rect.Bottom), new Vector2(rect.Right, rect.Bottom), Color.White);
            DrawLine(spriteBatch, new Vector2(rect.Right, rect.Top), new Vector2(rect.Right, rect.Bottom), Color.White);
        }

    }
}


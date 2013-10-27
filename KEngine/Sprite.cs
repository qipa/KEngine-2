using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kupiakos.KEngine
{
    public sealed class Sprite
    {
        #region Drawing Properties
        /// <summary>
        /// The internal <see cref="Microsoft.Xna.Framework.Graphics.Texture2D" /> that this class uses
        /// </summary>
        public Texture2D Texture { get; private set;}
        
        /// <summary>
        /// The Color that this Sprite will be blended with.
        /// White is no blending - a normal sprite
        /// </summary>
        public Color BlendColor {get; set;}
        
        /// <summary>
        /// The origin within the sprite that t 
        /// </summary>
        public Vector2 Origin { get; set;}
        
        /// <summary>
        /// How the sprite is scaled along the X and Y XAccelerationis.
        /// </summary>
        public Vector2 Scaling { get; set;}
        
        /// <summary>
        /// The amount the Sprite is rotated, in radians.
        /// </summary>
        public float Rotation {get; set;}
        #endregion


        /// <summary>
        /// The <see cref="Rectangle"/> that is used to select a subimage
        /// in the Texture.
        /// </summary>
        private Rectangle srcRect;

        /// <summary>
        /// The time since the last frame was shown in milliseconds.
        /// </summary>
        private int timeSinceLastFrame = 0;


        #region Subimage Properties and Values

        /// <summary>
        /// The width of each subimage
        /// </summary>
        public int Width { get; private set;}

        /// <summary>
        /// The height of each subimage
        /// </summary>
        public int Height { get; private set;}

        /// <summary>
        /// The number of subimages in the texture.
        /// </summary>
        public int ImageNumber { get; private set;}

        /// <summary>
        /// The number of subimages in each row in the texture.
        /// </summary>
        private int subRowLen;

        // Note: The X and Y are separated because 

        /// <summary>
        /// The X offset in the texture for the first subimage.
        /// Can be used to hold multiple sprites in a single texture.
        /// </summary>
        private Point subOff;


        private int _subIndex;
        /// <summary>
        /// Gets or sets which subimage the Sprite is currently focused on.
        /// </summary>
        /// <value>The zero-based index of the subimage to have the sprite be on.</value>
        public int ImageIndex 
        {
            get 
            {
                return _subIndex;
            }
            set 
            {
                timeSinceLastFrame = 0;
                this._subIndex = value % this.ImageNumber;

                this.srcRect.X = (this._subIndex % this.subRowLen) * this.Width + this.subOff.X;
                // Note this is integer division
                this.srcRect.Y = (this._subIndex / this.subRowLen) * this.Height + this.subOff.Y;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value>The percentage through that the animation is done</value>
        public double ImageCompletion
        {
            get {return ((double)this.ImageIndex / this.ImageNumber);}
            set {this.ImageIndex = (int)Math.Round(value * this.ImageNumber);}
        }

        /// <summary>
        /// The number of milliseconds in between each frame
        /// </summary>
        /// <value>The milliseconds of time in between each frame</value>
        public int ImageTime { get; set;}

        /// <summary>
        /// The number of times per second the subimage frame will be advanced.
        /// Provided for convenience from ImageTime 
        /// </summary>
        /// <value>The number of times per second e.g. 60 (fps)</value>
        public int ImageRate
        {
            get {return (int)(1000f / this.ImageTime);}
            set {this.ImageTime = (int)(1000f / value);}
        }


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Kupiakos.KEngine.Sprite"/> class.
        /// A sprite represents an image on-screen. The Sprite handles animation and other
        /// miscellaneous animation-related functions.
        /// This is an initializer for a typical image.
        /// </summary>
        /// <param name="texture">The Texture for this Sprite to use</param>
        /// <param name="origin">Where to set the origin of the Sprite</param>
        /// <param name="width">The width of each subimage in the Texture</param>
        /// <param name="height">The height of each subimage in the Texture</param>
        /// <param name="subNum">The number of subimages in the Texture</param>
        /// <param name="subRowLen">The number of subimages per row in the Texture</param>
        /// <param name="subOffset">The XY offset in the image to start reading subimages</param>
        /// <param name="animTime">The time in milliseconds per animation cycle</param>
        public Sprite(Texture2D texture, Vector2 origin, int width, int height, int subNum, int subRowLen, Point subOffset, int animTime)
        {
            this.Texture = texture;
            this.Width = width;
            this.Height = height;
            this.subOff = subOffset;
            this.Origin = origin;
            this.srcRect = new Rectangle(this.subOff.X, this.subOff.Y,
                                         this.Width, this.Height);
            this.ImageNumber = subNum;
            this.subRowLen = subRowLen;
            this.ImageTime = animTime;
            this.Scaling = Vector2.One;
            this.BlendColor = Color.White;
            this.Rotation = 0f;
            ImageIndex = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Kupiakos.KEngine.Sprite"/> class.
        /// A sprite represents an image on-screen. The Sprite handles animation and other
        /// miscellaneous animation-related functions.
        /// This initializer is for a single image in a texture containing many images.
        /// </summary>
        /// <param name="texture">The Texture for this Sprite to use</param>
        /// <param name="origin">Where to set the origin of the Sprite</param>
        /// <param name="width">The width of each subimage in the Texture</param>
        /// <param name="height">The height of each subimage in the Texture</param>
        /// <param name="subOffset">The XY offset in the image to start reading subimages</param>
        public Sprite(Texture2D texture, Vector2 origin, int width, int height, Point subOffset)
        : this(texture, origin, width, height, 1, 1, subOffset, 0) {}


        /// <summary>
        /// Initializes a new instance of the <see cref="Kupiakos.KEngine.Sprite"/> class.
        /// A sprite represents an image on-screen. The Sprite handles animation and other
        /// miscellaneous animation-related functions.
        /// This initializer is for a single image in a texture with a single image.
        /// </summary>
        /// <param name="texture">The Texture for this Sprite to use</param>
        /// <param name="origin">Where to set the origin of the Sprite</param>
        public Sprite(Texture2D texture, Vector2 origin)
        : this(texture, origin, texture.Width, texture.Height, Point.Zero) {}



        /// <summary>
        /// Progress the animation of the Sprite based on the time passed
        /// </summary>
        /// <param name="gameTime">The gameTime determining how much time has passed</param>
        public void Update(GameTime gameTime)
        {
            // Update using timesincelastframe
            if (ImageNumber > 1)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                int advance = (timeSinceLastFrame / this.ImageTime);
                if (advance > 0)
                {
                    this.timeSinceLastFrame -= advance * this.ImageTime;
                    this.ImageIndex += advance;
                }
            }
        }

        /// <summary>
        /// Draw the Sprite
        /// </summary>
        /// <param name="pos">The position at which to draw the Sprite.</param>
        /// <param name="spriteBatch">The spriteBatch to draw with. It must be reaYVelocity to draw.</param>
        /// <param name="depth">The depth in the spriteBatch at which to draw.</param>
        public void Draw(Vector2 pos, SpriteBatch spriteBatch, int depth) 
        {
            spriteBatch.Draw(this.Texture,
                             pos,
                             this.srcRect,
                             this.BlendColor,
                             this.Rotation,
                             this.Origin,
                             this.Scaling,
                             SpriteEffects.None,
                             depth);
        }
    }
}


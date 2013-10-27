using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Kupiakos.KEngine
{
    internal class QuadTree
    {

        QuadTree root = null;

        Rectangle bounds;
        
        List<QuadTree> children;
        List<ISolid> items;

        
        /// <summary>
        /// The number of items in this node.
        /// </summary>
        int numItems = 0;

        IEnumerable<ISolid> itemsMoving { get { return items.Where(x => x.IsMoving); } }

        const int MaxItems = 20;

        const int MinItems = 5;


        public bool HasSplit;

        public QuadTree(QuadTree root)
        {
            this.root = root;
            this.children = new List<QuadTree>();
            this.items = new List<ISolid>();
        }

        public QuadTree() : this(null)
        { 
        }
        


        public bool AddEntity(ISolid entity)
        {
            if (!bounds.Intersects(entity.BoundingBox))
                return false;

            numItems++;
            return true;

//            if (numItems > )
//
//                return true;
        }

        //public bool DoesCollide(Rectangle bounds)

        public void DrawBounds(SpriteBatch spriteBatch)
        {
            SpriteManager.DrawRectangle(spriteBatch, bounds);
        }
    }
}


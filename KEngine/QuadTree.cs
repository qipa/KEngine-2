using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Kupiakos.KEngine
{
    internal class QuadTree
    {
        static List<ISolid> _emptyEnumerable = new List<ISolid>();
        /// <summary>
        /// The parent of this node.
        /// The root node of the scene will have no node.
        /// </summary>
        QuadTree root = null;
        /// <summary>
        /// The bounds of the QuadTree. 
        /// </summary>
        Rectangle bounds;
        /// <summary>
        /// The children of this QuadTree.
        /// </summary>
        List<QuadTree> children;
        /// <summary>
        /// The solid items within this QuadTree.
        /// A QuadTree can be split and still have items within it.
        /// This will happen when an item is on the boundary of its splits.
        /// </summary>
        List<ISolid> items;
        /// <summary>
        /// The number of items in this node.
        /// </summary>
        int numItems = 0;

        /// <summary>
        /// Gets the items that are moving within the node.
        /// </summary>
        /// <value>The items moving.</value>
        public IEnumerable<ISolid> itemsMoving { get { return items.Where(x => x.IsMoving); } }

        /// <summary>
        /// The maximum number of items allowed in this QuadTree before it will split.
        /// </summary>
        const int MaxItems = 20;
        /// <summary>
        /// The minimum number of items allowed in this QuadTree before it will try to conjoin its children.
        /// </summary>
        const int MinItems = 5;
        /// <summary>
        /// The minimum width of a node before it will be unable to split further.
        /// </summary>
        const int MinSplitWidth = 32;
        /// <summary>
        /// The minimum height of a node before it will be unable to split further.
        /// </summary>
        const int MinSplitHeight = 32;
        /// <summary>
        /// Whether the current QuadTree has split.
        /// This does not necessarily mean it will always try to 
        /// </summary>
        public bool HasSplit = false;
        public bool CanSplit = true;

        public QuadTree(QuadTree root, Rectangle rect)
        {
            this.root = root;
            this.children = new List<QuadTree>();
            this.items = new List<ISolid>();
            this.bounds = rect;
            if (rect.Height < MinSplitHeight ||
                rect.Width < MinSplitWidth)
                this.CanSplit = false;
        }

        public QuadTree(Rectangle rect) : this(null, rect)
        { 
        }

        public bool AddEntity(ISolid entity)
        {
            // If we're not the root node and this node doesn't contain the given entity, we can't add it.
            // If it just intersects, and doesn't contain, it will end up adding it to the parent node.
            if (this.root != null && !bounds.Contains(entity.BoundingBox))
                return false;

            numItems++;

            if (!this.HasSplit && this.CanSplit && numItems > MaxItems)
                this.Split();

            // Add to the children nodes
            // If they all return false for AddEntity, add it to this node and not a child.
            // It's on a border.
            if (this.HasSplit)
                foreach (var child in this.children)
                    if (child.AddEntity(entity))
                        return true;

            // If this node hasn't split or the entity was on a border of the split,
            // add it directly to this node's items list.
            this.items.Add(entity);

            return true;

        }

        public void Split()
        {
            if (this.HasSplit || !this.CanSplit)
                return;


            // It was originally considered to take a constant-size
            // rectangle and simply move that around and use that as the
            // bounds for the children, but it was then realized that 
            // imprecision due to integer rounding could occur.
            // Now, if the width of the parent is an odd number, 
            // the southeast rectangle will be the biggest.
            int nwWidth = this.bounds.Width / 2;
            int nwHeight = this.bounds.Height / 2;

            // Northwest
            this.children.Add(new QuadTree(this,
                                           new Rectangle(this.bounds.Left, 
                                                         this.bounds.Top,
                                                         nwWidth, 
                                                         nwHeight)));
            // Northeast
            this.children.Add(new QuadTree(this,
                                           new Rectangle(this.bounds.Left + nwWidth, 
                                                         this.bounds.Top,
                                                         this.bounds.Right - (this.bounds.Left + nwWidth), 
                                                         nwHeight)));

            // Southwest
            this.children.Add(new QuadTree(this,
                                           new Rectangle(this.bounds.Left, 
                                                         this.bounds.Top + nwHeight,
                                                         nwWidth, 
                                                         this.bounds.Bottom - (this.bounds.Top + nwHeight))));

            // Southeast
            this.children.Add(new QuadTree(this,
                                           new Rectangle(this.bounds.Left + nwWidth, 
                                                         this.bounds.Top + nwHeight,
                                                         this.bounds.Right - (this.bounds.Left + nwWidth), 
                                                         this.bounds.Bottom - (this.bounds.Top + nwHeight))));



            List<ISolid> newItems = new List<ISolid>();
            bool inserted = false;
            foreach (var item in this.items)
            {
                foreach (var child in this.children)
                {
                    if (child.AddEntity(item))
                    {
                        inserted = true;
                        break;
                    }
                }
                if (!inserted)
                    newItems.Add(item);
            }

            this.items = newItems;
        }

        public IEnumerable<ISolid> GetCollisions(Rectangle testBounds, bool future)
        {
            if (this.root != null && !this.bounds.Intersects(testBounds))
                return QuadTree._emptyEnumerable;

            List<ISolid> foundList = new List<ISolid>();

            if (future)
                foreach (var itemNext in this.items)
                    if (itemNext.NextBoundingBox.Intersects(testBounds))
                        foundList.Add(itemNext);
                    else
                        foreach (var itemNow in this.items)
                            if (itemNow.BoundingBox.Intersects(testBounds))
                                foundList.Add(itemNow);

            if (this.HasSplit)
                foreach (var child in this.children)
                    foundList.AddRange(child.GetCollisions(testBounds, future));


            return foundList;
        }

        public void DrawBounds(SpriteBatch spriteBatch)
        {
            SpriteManager.DrawRectangle(spriteBatch, bounds);
        }
    }
}
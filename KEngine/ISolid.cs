using System;
using Microsoft.Xna.Framework;

namespace Kupiakos.KEngine
{
    /// <summary>
    /// An interfrace representing that an Entity is solid.
    /// </summary>
    public interface ISolid
    {
        Rectangle BoundingBox { get; }
        Rectangle NextBoundingBox { get; }
        bool IsMoving { get; }

    }
}


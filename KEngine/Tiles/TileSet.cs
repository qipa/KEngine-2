using System;
using System.Collections.Generic;

namespace Kupiakos.KEngine.Tiles
{
    public struct TileSet
    {
        public string Name;
        public string SpriteName;
        public int Width;
        public int Height;
        public int TileWidth;
        public int TileHeight;

        public List<string> EntityMatch;
    }
}


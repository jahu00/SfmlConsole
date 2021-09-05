using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfmlConsole.TileMapper
{
    public class StringTileMapper : ITileMapper
    {
        public Dictionary<string, int> TileMap { get; set; }

        public Type TileIdType => typeof(string);

        public object GetOtherTileId(int tileId)
        {
            var otherTileId = TileMap.Single(pair => pair.Value == tileId).Key;
            return otherTileId;
        }

        public int GetTileId(object otherTileId)
        {
            var tileName = (string)otherTileId;
            var tileId = TileMap[tileName];
            return tileId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfmlConsole.TileMapper
{
    public class AsciiTileMapper : ITileMapper
    {
        public Type TileIdType => typeof(char);

        public object GetOtherTileId(int tileId)
        {
            var otherTileId = (char)tileId;
            return otherTileId;
        }

        public int GetTileId(object alternativeTileId)
        {
            var tileId = (char)alternativeTileId;
            return tileId;
        }
    }
}

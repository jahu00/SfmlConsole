using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallHax.SfmlExtensions.TileMapper
{
    public interface ITileMapper
    {
        public Type TileIdType { get; }

        /// <summary>
        /// Maps some tileId to int tileId (for example from string, utf8 char)
        /// </summary>
        /// <param name="otherTileId"></param>
        /// <returns></returns>
        public int GetTileId(object otherTileId);

        /// <summary>
        /// Maps int tileId to the originalOne
        /// </summary>
        /// <param name="tileId"></param>
        /// <returns></returns>
        public object GetOtherTileId(int tileId);
    }
}

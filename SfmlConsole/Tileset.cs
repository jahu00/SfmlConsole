using SFML.Graphics;
using SfmlConsole.TileMapper;
using System;
using System.Collections.Generic;

namespace SfmlConsole
{
    public class Tileset
    {
        private Dictionary<Type, ITileMapper> TileMappers { get; set; } = new Dictionary<Type, ITileMapper>();

        private Texture Texture { get; set; }
        private int? TileWidth { get; set; }
        private int? TileHeight { get; set; }

        public Tileset(Texture texture)
        {
            Texture = texture;
        }

        public Tileset(Texture texture, int tileWidth, int tileHeight) : this (texture)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        private Sprite GetSprite()
        {
            var sprite = new Sprite(Texture);
            return sprite;
        }

        public Sprite GetTileSprite(object anyTileId)
        {
            if (anyTileId == null)
            {
                return GetSprite();
            }

            var tileId = MapTileId(anyTileId);

            var columns = Texture.Size.X / TileWidth.Value;
            var y = tileId / columns;
            var x = tileId % columns;
            var rect = new IntRect()
            {
                Left = (int)(x * TileWidth.Value),
                Top = (int)(y * TileHeight.Value),
                Width = TileWidth.Value,
                Height = TileHeight.Value,
            };
            var sprite = new Sprite(Texture, rect);
            return sprite;
        }

        public int MapTileId(object otherTileId)
        {
            if (otherTileId is int)
            {
                return (int)otherTileId;
            }
            var tileIdType = otherTileId.GetType();
            var tileMapper = TileMappers[tileIdType];
            var tileId = tileMapper.GetTileId(otherTileId);
            return tileId;
        }

        public void SetTileMapper(ITileMapper tileMapper)
        {
            TileMappers[tileMapper.TileIdType] = tileMapper;
        }

    }
}

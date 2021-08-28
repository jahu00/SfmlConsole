using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfmlConsole
{
    public class Tileset
    {
        private Texture Texture { get; set; }
        private int? TileWidth { get; set; }
        private int? TileHeight { get; set; }

        public Tileset(Texture texture)
        {
            Texture = texture;
        }

        public Tileset(Texture texture, int tileWidth, int tileHeight) : this (texture)
        {
            TileWidth = (int)tileWidth;
            TileHeight = (int)tileHeight;
        }

        private Sprite GetSprite()
        {
            var sprite = new Sprite(Texture);
            return sprite;
        }

        public Sprite GetTileSprite(char? character)
        {
            if (!character.HasValue)
            {
                return GetSprite();
            }

            var columns = Texture.Size.X / TileWidth.Value;
            var y = character.Value / columns;
            var x = character.Value % columns;
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
    }
}

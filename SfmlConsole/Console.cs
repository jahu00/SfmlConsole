using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace SfmlConsole
{
    public class Console : Transformable, Drawable
    {
        private ConsoleCharacter[,] Characters { get; set; }
        public int Height => Characters.GetLength(0);
        public int Width => Characters.GetLength(1);

        public float TileWidth { get; set; }
        public float TileHeight { get; set; }

        public Dictionary<string,Tileset> Tilesets { get; set; }

        public Console(Dictionary<string, Tileset> tilesets, float tileWidth, float tileHeight, int width, int height) : this(width, height)
        {
            Tilesets = tilesets;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        public Console(int width, int height)
        {
            Characters = new ConsoleCharacter[height, width];
        }

        public ConsoleCharacter GetCharacter(int x, int y)
        {
            var character = Characters[y, x];
            return character;
        }

        public void SetCharacter(int x, int y, ConsoleCharacter characterToBeSet)
        {
            Characters[y, x] = characterToBeSet;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            var sprites = PrepareSprites();
            foreach (var sprite in sprites)
            {
                sprite.Draw(target, states);
            }
        }

        public IEnumerable<Sprite> PrepareSprites()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var character = GetCharacter(x, y);
                    if (character == null)
                    {
                        continue;
                    }
                    var tileset = Tilesets[character.TilesetName];
                    var sprite = tileset.GetTileSprite(character.Character);
                    sprite.Position = new SFML.System.Vector2f(x * TileWidth, y * TileHeight);
                    if (TileWidth != sprite.TextureRect.Width || TileHeight != sprite.TextureRect.Height)
                    {
                        sprite.Scale = new SFML.System.Vector2f
                        (
                            TileWidth / sprite.TextureRect.Width,
                            TileHeight / sprite.TextureRect.Height
                        );
                    }
                    yield return sprite;
                }
            }
        }
    }
}

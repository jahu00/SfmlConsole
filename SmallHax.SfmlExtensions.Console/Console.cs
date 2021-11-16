using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace SfmlConsole
{
    public class Console : Transformable, Drawable
    {
        private ConsoleCharacter[,] Tiles { get; set; }
        public int Height => Tiles.GetLength(0);
        public int Width => Tiles.GetLength(1);

        public float TileWidth { get; set; }
        public float TileHeight { get; set; }

        public Color? BackgroundColor { get; set; }

        public Dictionary<string,Tileset> Tilesets { get; set; }

        public Console(Dictionary<string, Tileset> tilesets, float tileWidth, float tileHeight, int width, int height) : this(width, height)
        {
            Tilesets = tilesets;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        public Console(int width, int height)
        {
            Tiles = new ConsoleCharacter[height, width];
        }

        public ConsoleCharacter GetCharacter(int x, int y)
        {
            var character = Tiles[y, x];
            return character;
        }

        public void SetCharacter(int x, int y, ConsoleCharacter characterToBeSet)
        {
            Tiles[y, x] = characterToBeSet;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            var drawables = PrepareDrawables();
            foreach (var drawable in drawables)
            {
                drawable.Draw(target, states);
            }
        }

        public IEnumerable<Drawable> PrepareDrawables()
        {
            var tileOffset = new SFML.System.Vector2f(TileWidth / 2, TileHeight / 2);
            if (BackgroundColor.HasValue)
            {
                var background = GetBackground(BackgroundColor.Value, Width, Height);
                yield return background;
            }
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
                    var sprite = tileset.GetTileSprite(character.TileId);
                    sprite.Origin = new SFML.System.Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);
                    sprite.Position = new SFML.System.Vector2f(x * TileWidth, y * TileHeight) + tileOffset;
                    sprite.Rotation = character.Rotation;
                    
                    if (character.BackgroundColor.HasValue)
                    {
                        var background = GetBackground(character.BackgroundColor.Value, 1, 1, x, y);
                        yield return background;
                    }
                    if (character.ForegroundColor.HasValue)
                    {
                        sprite.Color = character.ForegroundColor.Value;
                    }
                    if (TileWidth != sprite.TextureRect.Width || TileHeight != sprite.TextureRect.Height)
                    {
                        sprite.Scale = new SFML.System.Vector2f
                        (
                            TileWidth / sprite.TextureRect.Width,
                            TileHeight / sprite.TextureRect.Height
                        );
                    }

                    var flip = new SFML.System.Vector2f(character.HorizontalFlip ? -1 : 1, character.VerticalFlip ? -1 : 1);
                    sprite.Scale = new SFML.System.Vector2f(sprite.Scale.X * flip.X, sprite.Scale.Y * flip.Y);

                    yield return sprite;
                }
            }
        }

        public IEnumerable<ConsoleCharacter> GetCharacters(int x, int y, int length)
        {
            for(var i = 0; i < length; i++)
            {
                var consoleCharacter = GetCharacter(x, y);
                yield return consoleCharacter;
                x++;
                x = x % Width;
                if (x == 0)
                {
                    y++;
                }
                if (y >= Height)
                {
                    break;
                }
            }
        }

        public void SetText(int x, int y, string text, ConsoleCharacter brush)
        {
            foreach(var character in text)
            {
                var consoleCharacter = brush with { TileId = character };
                SetCharacter(x, y, consoleCharacter);
                x++;
                x = x % Width;
                if (x == 0)
                {
                    y++;
                }
                if (y >= Height)
                {
                    break;
                }
            }
        }

        public Drawable GetBackground(Color color, int width, int height, int x = 0, int y = 0)
        {
            var background = new RectangleShape(new SFML.System.Vector2f(width * TileWidth, height * TileHeight));
            background.FillColor = color;
            background.Position = new SFML.System.Vector2f(x * TileWidth, y * TileHeight);
            return background;
        }
    }
}

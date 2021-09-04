﻿using SFML.Graphics;
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
            var drawables = PrepareDrawables();
            foreach (var drawable in drawables)
            {
                drawable.Draw(target, states);
            }
        }

        public IEnumerable<Drawable> PrepareDrawables()
        {
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
                    var sprite = tileset.GetTileSprite(character.Character);
                    sprite.Position = new SFML.System.Vector2f(x * TileWidth, y * TileHeight);
                    if (character.BackgroundColor.HasValue)
                    {
                        var background = GetBackground(character.BackgroundColor.Value, 1, 1, x, y);
                        yield return background;
                    }
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
                var consoleCharacter = brush with { Character = character };
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

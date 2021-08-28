using FluentAssertions;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SfmlConsole.Tests
{
    public class TilesetTests
    {
        [Fact]
        public void GetTileSpriteTest()
        {
            //Arrange
            var tileWidth = 64;
            var tileHeight = 64;
            var character = 'a';
            var texture = new Texture("Data/font.png");
            var columns = texture.Size.X / tileWidth;
            var y = character / columns;
            var x = character % columns;
            var top = (int)(y * tileHeight);
            var left = (int)x * tileWidth;
            var tileset = new Tileset(texture, tileWidth, tileHeight);
            
            //Act
            var sprite = tileset.GetTileSprite('a');

            //Assert
            sprite.TextureRect.Left.Should().Be(left, "Expected sprite offset");
            sprite.TextureRect.Top.Should().Be(top, "Expected sprite offset");
            sprite.TextureRect.Width.Should().Be(tileWidth, "Expected sprite size");
            sprite.TextureRect.Height.Should().Be(tileHeight, "Expected sprite size");

        }

        [Fact]
        public void GetTileSprite()
        {
            //Arrange
            var texture = new Texture("Data/font.png");
            var tileset = new Tileset(texture);

            //Act
            var sprite = tileset.GetTileSprite(null);

            //Assert
            sprite.TextureRect.Left.Should().Be(0, "Expected sprite offset");
            sprite.TextureRect.Top.Should().Be(0, "Expected sprite offset");
            sprite.TextureRect.Width.Should().Be((int)texture.Size.X, "Expected sprite size");
            sprite.TextureRect.Height.Should().Be((int)texture.Size.Y, "Expected sprite size");

        }
    }
}

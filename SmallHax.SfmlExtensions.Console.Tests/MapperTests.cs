using FluentAssertions;
using SmallHax.SfmlExtensions.TileMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmallHax.SfmlExtensions.Tests
{
    public class MapperTests
    {
        [Fact]
        public void AsciiTileMapperTest()
        {
            //Arrange
            var character = 'a';
            var expectedTileId = 97;
            var tileMapper = new AsciiTileMapper();

            //Act
            var tileId = tileMapper.GetTileId(character);

            //Assert
            tileId.Should().Be(expectedTileId);
        }

        [Fact]
        public void Utf8TileMapperTest()
        {
            //Arrange
            var character = '█';
            var expectedTileId = 219;
            var tileMapper = new Utf8TileMapper();

            //Act
            var tileId = tileMapper.GetTileId(character);

            //Assert
            tileId.Should().Be(expectedTileId);
        }

        [Fact]
        public void StringTileMapperTest()
        {
            //Arrange
            var tileName = "sword";
            var expectedTileId = 128;
            var tileMapper = new StringTileMapper();
            tileMapper.TileMap = new Dictionary<string, int>()
            {
                { tileName, expectedTileId }
            };

            //Act
            var tileId = tileMapper.GetTileId(tileName);

            //Assert
            tileId.Should().Be(expectedTileId);
        }
    }
}

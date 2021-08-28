using FluentAssertions;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SfmlConsole.Tests
{
    public class ConsoleTests
    {
        [Fact]
        public void ConstructorTest()
        {
            //Arrange
            var width = 5;
            var height = 10;

            //Act
            var console = new SfmlConsole.Console(5, 10);

            //Assert
            console.Width.Should().Be(width, $"We set width to {width}.");
            console.Height.Should().Be(height, $"We set height to {height}.");
        }

        /*[Fact]
        public void PreinitializationTest()
        {
            var console = new SfmlConsole.Console(1, 1);

            var character = console.GetCharacter(0, 0);

            character.Should().NotBeNull("All tiles should be preinitialized.");
        }*/

        [Fact]
        public void GetSetTest()
        {
            //Arrange
            var console = new SfmlConsole.Console(10, 10);
            var x = 5;
            var y = 6;
            var characterToBeSet = new ConsoleCharacter() { TilesetName = "SomeTilemap", Character = 'a' };

            //Act
            console.SetCharacter(x, y, characterToBeSet);
            var characterStored = console.GetCharacter(x, y);

            //Assert
            characterStored.Should().BeEquivalentTo(characterToBeSet, "This is the character that we set");
        }

        // I have no idea how to test this
        
        [Fact]
        public void GetSpritesTest()
        {
            //Arrange
            //var renderTarget = new TestRenderTarget();
            var texture = new Texture("Data/font.png");
            var tilesetName = "SomeTilemap";
            var tileset = new Tileset(texture, 64, 64);
            var tilesets = new Dictionary<string, Tileset>()
            {
                {
                    tilesetName, tileset
                }
            };

            var console = new SfmlConsole.Console(tilesets, 32, 32, 10, 10);
            var character = new ConsoleCharacter() { TilesetName = tilesetName, Character = 'a' };
            console.SetCharacter(1, 1, character);
            console.SetCharacter(2, 2, character);
            console.SetCharacter(3, 3, character);

            //Act
            //renderTarget.Draw(console);
            var sprites = console.PrepareSprites().ToArray();

            //Assert
            sprites.Count().Should().BeGreaterThan(0, "We intended to draw something.");
        }
    }
}

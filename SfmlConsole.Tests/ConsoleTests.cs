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
            characterStored.Should().BeEquivalentTo(characterToBeSet, "This is the character that we set.");
        }

        [Fact]
        public void SetText()
        {
            var console = new SfmlConsole.Console(10, 10);
            var x = 5;
            var y = 6;
            var length = 26;
            var brush = new ConsoleCharacter() { TilesetName = "SomeTilemap", BackgroundColor = Color.Red };
            //var text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            var text = "Lorem ipsum dolor sit amet";
            var expectedCharacters = text.Select(character => brush with { Character = character });

            //Act
            console.SetText(x, y, text, brush);
            var charactersStored = console.GetCharacters(x, y, length);
            //var storedText = string.Join("", charactersStored.Select(consoleCharacter => consoleCharacter.Character));

            //Assert
            //storedText.Should().Be(text, "That was the text that we wrote");
            
            charactersStored.Should().BeEquivalentTo(expectedCharacters, "This is the character that we set.");
        }

        [Fact]
        public void GetSpritesTest()
        {
            //Arrange
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
            var sprites = console.PrepareDrawables().ToArray();

            //Assert
            sprites.Count().Should().Be(3, "We intended to draw something so sprites were expected.");
        }
    }
}

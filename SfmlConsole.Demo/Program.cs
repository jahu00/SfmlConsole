using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SfmlConsole.Demo
{
    class Program
    {
        static SfmlConsole.Console Demo1(Dictionary<string, Tileset> tilesets, string tilesetName)
        {
            var console = new SfmlConsole.Console(tilesets, 24, 32, 16, 16);
            for (var i = 0; i < 256; i++)
            {
                var x = i % 16;
                var y = i / 16;
                var character = new ConsoleCharacter()
                {
                    Character = (char)i,
                    TilesetName = tilesetName
                };
                console.SetCharacter(x, y, character);
            }
            return console;
        }

        static SfmlConsole.Console Demo2(Dictionary<string, Tileset> tilesets, string tilesetName)
        {
            var console = new SfmlConsole.Console(tilesets, 32, 32, 16, 16);
            for (var i = 0; i < 256; i++)
            {
                var x = i % 16;
                var y = i / 16;
                if (x == 0 || y == 0 || x == 15 || y == 15)
                {
                    var character = new ConsoleCharacter()
                    {
                        Character = (char)219,
                        TilesetName = tilesetName
                    };
                    console.SetCharacter(x, y, character);
                }
            }
            return console;
        }

        static SfmlConsole.Console MapConsole(Dictionary<string, Tileset> tilesets, string tilesetName)
        {
            var console = new SfmlConsole.Console(tilesets, 32, 32, 30, 24);
            for (var y = 0; y < console.Height; y++)
            {
                for (var x = 0; x < console.Width; x++)
                {
                    var character = new ConsoleCharacter()
                    {
                        Character = '.',
                        TilesetName = tilesetName
                    };
                    console.SetCharacter(x, y, character);
                }
            }
            console.BackgroundColor = new Color(0, 170, 0);
            return console;
        }

        static SfmlConsole.Console SidebarConsole(Dictionary<string, Tileset> tilesets, string tilesetName)
        {
            var console = new SfmlConsole.Console(tilesets, 32, 32, 10, 24);
            for (var y = 0; y < console.Height; y++)
            {
                for (var x = 0; x < console.Width; x++)
                {
                    var character = new ConsoleCharacter()
                    {
                        Character = '▒',
                        TilesetName = tilesetName
                    };
                    console.SetCharacter(x, y, character);
                }
            }
            console.Position = new SFML.System.Vector2f(32 * 30, 0);
            console.BackgroundColor = Color.Black;
            return console;
        }

        static SfmlConsole.Console ToolbarConsole(Dictionary<string, Tileset> tilesets, string tilesetName)
        {
            var console = new SfmlConsole.Console(tilesets, 32, 32, 21, 3);
            var brush = new ConsoleCharacter()
            {
                TilesetName = tilesetName,
                BackgroundColor = new Color(254,254,84)
            };
            console.SetText(0, 0, "┌─┬─┬─┬─┬─┬─┬─┬─┬─┬─┐", brush);
            console.SetText(0, 1, "│ │ │ │ │ │ │ │ │ │ │", brush);
            console.SetText(0, 2, "└─┴─┴─┴─┴─┴─┴─┴─┴─┴─┘", brush);
            console.Position = new SFML.System.Vector2f(32 * 4, 720 - 32 * console.Height);
            //console.BackgroundColor = Color.Yellow;
            return console;
        }

        static void Main(string[] args)
        {
            var window = new RenderWindow(new VideoMode(1280, 720, 32), "SfmlConsole Demo");
            window.Closed += (sender, e) => { window.Close(); };
            window.SetFramerateLimit(60);

            var texture = new Texture("Data/font.png");
            var tilesets = new Dictionary<string, Tileset>();
            var tileset = new Tileset(texture, 64, 64) { MapUtf8ToAscii = true };
            tilesets.Add("font", tileset);

            var map = MapConsole(tilesets, "font");
            var sidebar = SidebarConsole(tilesets, "font");
            var toolbar = ToolbarConsole(tilesets, "font");
            //console.Position = new SFML.System.Vector2f(32, 32);

            var demo = Demo1(tilesets, "font");

            while (window.IsOpen)
            {
                window.DispatchEvents();
                
                window.Clear();

                window.Draw(map);
                window.Draw(sidebar);
                window.Draw(toolbar);
                //window.Draw(demo);

                window.Display();
            }

        }
    }
}

using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;

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

        static void Main(string[] args)
        {
            var window = new RenderWindow(new VideoMode(512, 512, 32), "SfmlConsole Demo");
            window.Closed += (sender, e) => { window.Close(); };
            window.SetFramerateLimit(60);

            var texture = new Texture("Data/font.png");
            var tilesets = new Dictionary<string, Tileset>();
            var tileset = new Tileset(texture, 64, 64);
            tilesets.Add("font", tileset);

            var console = Demo2(tilesets, "font");
            

            while (window.IsOpen)
            {
                window.DispatchEvents();
                
                window.Clear();

                window.Draw(console);

                window.Display();
            }

        }
    }
}

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
            console.BackgroundColor = EgaColor.Green;
            return console;
        }

        static SfmlConsole.Console SidebarConsole(Dictionary<string, Tileset> tilesets, string tilesetName)
        {
            var console = new SfmlConsole.Console(tilesets, 32, 32, 10, 24);
            var sidebarTemlate =
@"┌────────┐
│ Mon 1  │
├─┬────┬─┤
│ │    │ │
├─┴────┴─┤
│        │
├────────┤
│G    500│
├───┬┬───┤
│ H ││ E │
├───┼┼───┤
│   ││ █ │
│   ││ █ │
│ ▄ ││ █ │
│ █ ││ █ │
│ █ ││ █ │
│ █ ││ █ │
│ █ ││ █ │
│ █ ││ █ │
│ █ ││ █ │
│ █ ││ █ │
│ █ ││ █ │
│ █ ││ █ │
└───┴┴───┘";
            sidebarTemlate = sidebarTemlate.Replace("\r\n", "");

            var brush = new ConsoleCharacter()
            {
                TilesetName = tilesetName,
                ForegroundColor = EgaColor.Orange
            };
            console.SetText(0, 0, sidebarTemlate, brush);

            console.Position = new SFML.System.Vector2f(32 * 30, 0);
            console.BackgroundColor = EgaColor.Yellow;
            return console;
        }

        static SfmlConsole.Console ToolbarConsole(Dictionary<string, Tileset> tilesets, string tilesetName)
        {
            var console = new SfmlConsole.Console(tilesets, 32, 32, 21, 3);
            var brush = new ConsoleCharacter()
            {
                TilesetName = tilesetName,
                ForegroundColor = EgaColor.Orange
            };
            console.SetText(0, 0, "┌─┬─┬─┬─┬─┬─┬─┬─┬─┬─┐", brush);
            console.SetText(0, 1, "│ │ │ │ │ │ │ │ │ │ │", brush);
            console.SetText(0, 2, "└─┴─┴─┴─┴─┴─┴─┴─┴─┴─┘", brush);
            console.Position = new SFML.System.Vector2f(32 * 4, 768 - 32 * console.Height);
            console.BackgroundColor = EgaColor.Yellow;
            return console;
        }

        static void Main(string[] args)
        {
            var window = new RenderWindow(new VideoMode(1280, 768, 32), "SfmlConsole Demo");
            window.Closed += (sender, e) => { window.Close(); };
            window.SetFramerateLimit(60);

            var texture = new Texture("Data/font.png");
            var tilesets = new Dictionary<string, Tileset>();
            var tileset = new Tileset(texture, 64, 64) { MapUtf8ToAscii = true };
            var tilesetName = "font";

            tilesets.Add(tilesetName, tileset);
            tilesets.Add("sword", new Tileset(new Texture("Data/sword.png")) );

            var map = MapConsole(tilesets, tilesetName);
            var sidebar = SidebarConsole(tilesets, tilesetName);
            var toolbar = ToolbarConsole(tilesets, tilesetName);
            
            var time = new DateTime(1900, 1, 1, 6, 0, 0);

            var timer = DateTime.UtcNow;

            toolbar.SetCharacter(1, 1, new ConsoleCharacter() { TilesetName = "sword", ForegroundColor = EgaColor.Black });

            while (window.IsOpen)
            {
                window.DispatchEvents();
                
                window.Clear();

                UpdateTime(ref time, ref timer);

                DrawTime(sidebar, time, tilesetName);

                window.Draw(map);
                window.Draw(sidebar);
                window.Draw(toolbar);

                window.Display();
            }

        }

        private static void DrawTime(Console target, DateTime time, string tilesetNmae)
        {
            var hourStr = time.ToString("%h");
            if (hourStr.Length == 1)
            {
                hourStr = " " + hourStr;
            }
            var minuteStr = (time.Minute / 10).ToString();
            if (minuteStr.Length == 1)
            {
                minuteStr =  minuteStr + "0";
            }
            
            var timeStr = $"{hourStr}:{minuteStr} {time.ToString("tt")}";
            var brush = new ConsoleCharacter() { TilesetName = tilesetNmae, ForegroundColor = EgaColor.Black };
            target.SetText(1, 5, timeStr, brush);
        }

        private static void UpdateTime(ref DateTime time, ref DateTime timer)
        {
            var utcNow = DateTime.UtcNow;
            var nextDay = new DateTime(1900, 1, 2, 2, 0, 0);
            if ((utcNow - timer).Seconds >= 1)
            {
                timer = utcNow;
                time = time.AddMinutes(1);
                if (time == nextDay)
                {
                    time = new DateTime(1900, 1, 1, 6, 0, 0);
                }
            }
        }
    }
}

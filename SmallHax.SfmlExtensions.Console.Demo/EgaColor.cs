using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallHax.SfmlExtensions.Demo
{
    public static class EgaColor
    {
        public static readonly Color Black = new Color(0, 0, 0);
        public static readonly Color Blue = new Color(0, 0, 168);
        public static readonly Color Green = new Color(0, 168, 0);
        public static readonly Color Cyan = new Color(0, 168, 168);
        public static readonly Color Red = new Color(168, 0, 0);
        public static readonly Color Magenta = new Color(168, 0, 168);
        public static readonly Color Orange = new Color(168, 84, 0);
        public static readonly Color Gray = new Color(168, 168, 168);
        public static readonly Color DarkGray = new Color(84, 84, 84);
        public static readonly Color LightBlue = new Color(84, 84, 254);
        public static readonly Color LightGreen = new Color(84, 254, 84);
        public static readonly Color LightCyan = new Color(84, 254, 254);
        public static readonly Color LightRed = new Color(254, 84, 84);
        public static readonly Color LightMagenta = new Color(254, 84, 254);
        public static readonly Color Yellow = new Color(254, 254, 84);
        public static readonly Color White = new Color(254, 254, 254);

        public static Color[] ColorMap = new Color[]
        {
            Black,
            Blue,
            Green,
            Cyan,
            Red,
            Magenta,
            Orange,
            Gray,
            DarkGray,
            LightBlue,
            LightGreen,
            LightCyan,
            LightRed,
            LightMagenta,
            Yellow,
            White
        };

        public static Color FromId(int id)
        {
            var color = ColorMap[id];
            return color;
        }
    }
}

using SFML.Graphics;

namespace SfmlConsole
{
    public record ConsoleCharacter
    {
        public string TilesetName { get; init; }
        public char? Character { get; init; }
        public Color? BackgroundColor { get; init; }
    }
}

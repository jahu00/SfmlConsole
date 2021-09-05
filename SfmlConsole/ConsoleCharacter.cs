using SFML.Graphics;

namespace SfmlConsole
{
    public record ConsoleCharacter
    {
        public string TilesetName { get; init; }
        public object TileId { get; init; }
        public Color? BackgroundColor { get; init; }
        public Color? ForegroundColor { get; init; }
    }
}

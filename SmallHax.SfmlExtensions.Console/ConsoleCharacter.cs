using SFML.Graphics;

namespace SmallHax.SfmlExtensions
{
    public record ConsoleCharacter
    {
        public string TilesetName { get; init; }
        public object TileId { get; init; }
        public Color? BackgroundColor { get; init; }
        public Color? ForegroundColor { get; init; }
        public float Rotation { get; init; }
        public bool VerticalFlip { get; init; }
        public bool HorizontalFlip { get; init; }
    }
}

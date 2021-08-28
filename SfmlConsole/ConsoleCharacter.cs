using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfmlConsole
{
    public record ConsoleCharacter
    {
        public string TilesetName { get; init; }
        public char? Character { get; init; }
    }
}

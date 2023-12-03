using System;

namespace FinalEmblem.Core
{
    [Flags]
    public enum Compass
    {
        N = 1,
        NE = 2,
        E = 4,
        SE = 8,
        S = 16,
        SW = 32,
        W = 64,
        NW = 128
    }

}

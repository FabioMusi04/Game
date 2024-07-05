using System.Drawing;
using System.Collections.Generic;
using GameProject.Services.Game;

namespace GameProject.Utils
{
    public class Constants
    {
        public static class UI 
        {
            public static class Button
            {
                public const int BUTTON_WIDTH_DEFAULT = 96;
                public const int BUTTON_HEIGHT_DEFAULT = 32;
                public const int BUTTON_HEIGHT = (int)(BUTTON_HEIGHT_DEFAULT * GameSetup.SCALE);
                public const int BUTTON_WIDTH = (int)(BUTTON_WIDTH_DEFAULT * GameSetup.SCALE);
                public const int BUTTON_SPRITES_DEFAULT_ROW = 9;
                public const int BUTTON_SPRITES_DEFAULT_COLUMN = 4;
            }
        }
        public static class Directions
        {
            public const int DOWN = 0;
            public const int LEFT = 1;
            public const int RIGHT = 2;
            public const int UP = 3;
        }
        public static class PlayerConstants
        {
            public const int IDLE_DOWN = 0;
            public const int IDLE_RIGHT = 1;
            public const int IDLE_UP = 2;
            public const int IDLE_LEFT = 1;
            public const int RUNNING_DOWN = 3;
            public const int RUNNING_RIGHT = 4;
            public const int RUNNING_UP = 5;
            public const int RUNNING_LEFT = 4;
            public const int ATTACKING_DOWN = 6;
            public const int ATTACKING_RIGHT = 7;
            public const int ATTACKING_UP = 8;
            public const int ATTACKING_LEFT = 7;
            public const int DYING_RIGHT = 9;
            public const int DYING_LEFT = 9;
            public static int GetSpriteAmount(int playerAction)
            {
                return playerAction switch
                {
                    IDLE_DOWN or IDLE_RIGHT or IDLE_UP or IDLE_LEFT => 6,
                    RUNNING_DOWN or RUNNING_RIGHT or RUNNING_UP or RUNNING_LEFT => 6,
                    ATTACKING_DOWN or ATTACKING_RIGHT or ATTACKING_UP or ATTACKING_LEFT => 4,
                    DYING_RIGHT or DYING_LEFT => 3,
                    _ => 0,
                };
            }
        }

        public static class MappingColorsToSprites
        {
             public static readonly Dictionary<string, Color> SpriteColors = new()
             {
                    { "Grass", Color.FromArgb(168, 230, 29) },
                    { "Water", Color.FromArgb(77, 109, 243) },
                    { "Player", Color.FromArgb(180, 180, 180) }
                };

        }
    }
}
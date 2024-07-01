namespace GameProject.Utils
{
    public class Constants
    {
        public static class Directions
        {
            public const int DOWN = 0;
            public const int LEFT = 1;
            public const int RIGHT = 2;
            public const int UP = 3;
        }
        public static class PlayerConstants
        {
            public const int RUNNING_DOWN = 0;
            public const int RUNNING_LEFT = 1;
            public const int RUNNING_RIGHT = 2;
            public const int RUNNING_UP = 3;

            public static int GetSpriteAmount(int playerAction)
            {
                return playerAction switch
                {
                    RUNNING_DOWN => 4,
                    RUNNING_LEFT => 4,
                    RUNNING_RIGHT => 4,
                    RUNNING_UP => 4,
                    _ => 1,
                };
            }
        }
    }
}
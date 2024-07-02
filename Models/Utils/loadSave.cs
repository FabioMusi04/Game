using System.Drawing;
using GameProject.Services.Game;

namespace GameProject.Utils
{
    public class LoadSave
    {
        public const string PLAYER_ATLAS = "../../../Res/NPC/hero.png";
        public const string LEVEL_ATLAS = "../../../Res/NPC/hero.png";
        public const string LEVEL_1_DATA = "../../../Res/NPC/hero.png";
        public static Bitmap GetSpriteAtlas(string path)
        {
            Bitmap img = new(path);
            return img;
        }

        public static int[,] GetLevelData()
        {
            int[,] levelData = new int[GameSetup.TILES_IN_HEIGHT, GameSetup.TILES_IN_WIDTH];
            Bitmap img = GetSpriteAtlas(LEVEL_1_DATA);
            for (int i = 0; i < GameSetup.TILES_IN_HEIGHT; i++)
            {
                for (int j = 0; j < GameSetup.TILES_IN_WIDTH; j++)
                {
                    Color pixel = img.GetPixel(j, i);
                    int value = pixel.R;
                    if (value >= GameSetup.TILES_IN_WIDTH) {
                        value = 0;
                    }
                    levelData[i, j] = value;
                }
            }
            return levelData;
        }

        public static void SaveGame()
        {
            // Save the game
        }

        public static void LoadGame()
        {
            // Load the game
        }
    }
}
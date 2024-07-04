using System.Drawing;
using GameProject.Services.Game;
using System.Windows.Forms;
using System;
using System.Reflection.Metadata;
using System.Collections.Generic;



namespace GameProject.Utils
{
    public class LoadSave
    {
        public static string PLAYER_ATLAS = Application.StartupPath.ToString() + @"..\..\..\Res\NPC\hero.png";
        //public static string LEVEL_ATLAS = Application.StartupPath.ToString() + @"..\..\..\Res\NPC\hero.png";
        public static string LEVEL_1_DATA = Application.StartupPath.ToString() + @"..\..\..\Res\Maps\Map1.png";
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
                    KeyValuePair<string, Color>? sprite = IsReferredToSprite(pixel);
                    if(sprite != null)
                    {
                        levelData[i, j] = sprite.Value.Key switch
                        {
                            "Grass" => 0,
                            "Water" => 1,
                            "Player" => 2,
                            _ => 0,
                        };
                    }
                }
            }
            return levelData;
        }

        public static KeyValuePair<string, Color>? IsReferredToSprite(Color color)
        {
            foreach (KeyValuePair<string, Color> entry in Constants.MappingColorsToSprites.SpriteColors)
            {
                if (entry.Value == color)
                {   
                    if(color == Color.FromArgb(180, 180, 180))
                    {
                        Console.WriteLine("Player");
                    }
                    return entry;
                }
            }
            return null;
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
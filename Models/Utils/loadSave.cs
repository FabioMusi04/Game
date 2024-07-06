using System.Drawing;
using GameProject.Services.Game;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;


namespace GameProject.Utils
{
    public class LoadSave
    {
        public static string PLAYER_ATLAS = Application.StartupPath.ToString() + @"..\..\..\Res\NPC\hero.png";
        public static string LEVEL_ATLAS = Application.StartupPath.ToString() + @"..\..\..\Res\Tilesets\Tiles.png";
        public static string LEVEL_1_DATA = Application.StartupPath.ToString() + @"..\..\..\Res\Maps\Map1.png";
        public static string MENU_BUTTONS = Application.StartupPath.ToString() + @"..\..\..\Res\UI\CurveFlatBtns.png";
        public static string MENU_BACKGROUND = Application.StartupPath.ToString() + @"..\..\..\Res\UI\MenuBackground.png";

        public static Bitmap GetSpriteAtlas(string path)
        {
            Bitmap img = new(path);
            return img;
        }
        public static int[,] GetLevelData()
        {
            int[,] levelData = new int[GameSetup.TILES_IN_HEIGHT, GameSetup.TILES_IN_WIDTH];
            Bitmap img = GetSpriteAtlas(LEVEL_1_DATA);

            BitmapData bmpData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, img.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(img.PixelFormat) / 8;
            int height = img.Height;
            int width = img.Width;
            byte[] pixelData = new byte[bmpData.Stride * height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixelData, 0, pixelData.Length);
            img.UnlockBits(bmpData);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int position = (y * bmpData.Stride) + (x * bytesPerPixel);
                    Color pixel = Color.FromArgb(pixelData[position + 2], pixelData[position + 1], pixelData[position]);
                    KeyValuePair<string, Color>? sprite = IsReferredToSprite(pixel);
                    if (sprite != null)
                    {
                        levelData[y, x] = sprite.Value.Key switch
                        {
                            "Grass" => 73,
                            "Water" => 551,
                            "Player" => -1,
                            _ => -1,
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
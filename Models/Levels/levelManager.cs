using GameProject.Services.Game;
using GameProject.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace GameProject.Services.Levels
{
    public class LevelManager
    {
        private readonly GameSetup _game;
        private List<Bitmap> _levelSprite;
        private Bitmap _levelBitmap;

        private Level _level;

        public LevelManager(GameSetup game)
        {
            this._game = game;
            ImportOutsideSprites();
            int[,] levelData = LoadSave.GetLevelData();
            this._level = new Level(levelData);
        }

        private void ImportOutsideSprites()
        {
            this._levelSprite = new List<Bitmap>();

            Task.Run(() =>
            {
                Bitmap img = LoadSave.GetSpriteAtlas(LoadSave.LEVEL_ATLAS);
                for (int i = 0; i < 30; i++)
                {
                    for (int j = 0; j < 19; j++)
                    {
                        this._levelSprite.Add(img.Clone(new Rectangle(j * 16, i * 16, 16, 16), img.PixelFormat));
                    }
                }
                img.Dispose();
            });
        }

         private void CreateLevelBitmap()
    {
        int width = GameSetup.TILES_IN_WIDTH * GameSetup.TILE_SIZE;
        int height = GameSetup.TILES_IN_HEIGHT * GameSetup.TILE_SIZE;
        _levelBitmap = new Bitmap(width, height);
    }

    private void DrawLevelBitmap()
    {
        if (_levelSprite == null || _level == null || _levelBitmap == null)
            return;

        Rectangle rect = new Rectangle(0, 0, _levelBitmap.Width, _levelBitmap.Height);
        BitmapData levelData = _levelBitmap.LockBits(rect, ImageLockMode.WriteOnly, _levelBitmap.PixelFormat);

        int bytesPerPixel = Image.GetPixelFormatSize(_levelBitmap.PixelFormat) / 8;
        int stride = levelData.Stride;
        byte[] levelBuffer = new byte[stride * _levelBitmap.Height];

        for (int i = 0; i < GameSetup.TILES_IN_HEIGHT; i++)
        {
            for (int j = 0; j < GameSetup.TILES_IN_WIDTH; j++)
            {
                int spriteIndex = _level.GetSpriteIndex(j, i);
                if (spriteIndex != -1)
                {
                    Bitmap sprite = _levelSprite[spriteIndex];
                    Rectangle spriteRect = new(0, 0, sprite.Width, sprite.Height);
                    BitmapData spriteData = sprite.LockBits(spriteRect, ImageLockMode.ReadOnly, sprite.PixelFormat);

                    int spriteStride = spriteData.Stride;
                    byte[] spriteBuffer = new byte[spriteStride * sprite.Height];
                    Marshal.Copy(spriteData.Scan0, spriteBuffer, 0, spriteBuffer.Length);

                    for (int y = 0; y < sprite.Height; y++)
                    {
                        int levelOffset = ((i * GameSetup.TILE_SIZE + y) * stride) + (j * GameSetup.TILE_SIZE * bytesPerPixel);
                        int spriteOffset = y * spriteStride;
                        Array.Copy(spriteBuffer, spriteOffset, levelBuffer, levelOffset, spriteStride);
                    }

                    sprite.UnlockBits(spriteData);
                }
            }
        }

        Marshal.Copy(levelBuffer, 0, levelData.Scan0, levelBuffer.Length);
        _levelBitmap.UnlockBits(levelData);
    }

    public void Draw(Graphics g)
    {
        if (_levelBitmap == null)
            CreateLevelBitmap();

        DrawLevelBitmap();
        g.DrawImage(_levelBitmap, 0, 0);
    }

        public Level GetCurrentLevelData()
        {
            return this._level;
        }

        public void Update()
        {
            // Update the level
        }
    }
}
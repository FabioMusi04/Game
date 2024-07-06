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

    using (Graphics g = Graphics.FromImage(_levelBitmap))
    {
        g.Clear(Color.Transparent); // Clear the bitmap first

        for (int i = 0; i < GameSetup.TILES_IN_HEIGHT; i++)
        {
            for (int j = 0; j < GameSetup.TILES_IN_WIDTH; j++)
            {
                int spriteIndex = _level.GetSpriteIndex(j, i);
                if (spriteIndex != -1)
                {
                    Bitmap sprite = _levelSprite[spriteIndex];
                    int x = (int)(j * GameSetup.TILES_DEAULT_SIZE * GameSetup.SCALE);
                    int y = (int)(i * GameSetup.TILES_DEAULT_SIZE * GameSetup.SCALE);
                    int width = (int)(sprite.Width * GameSetup.SCALE);
                    int height = (int)(sprite.Height * GameSetup.SCALE);

                    g.DrawImage(sprite, x, y, width, height);
                }
            }
        }
    }
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
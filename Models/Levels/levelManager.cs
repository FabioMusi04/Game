using GameProject.Services.Game;
using GameProject.Utils;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System;

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

        private void DrawLevelBitmap(int _xLvlOffset, int _yLvlOffset)
        {
            if (_levelSprite == null || _level == null || _levelBitmap == null)
                return;

            using Graphics g = Graphics.FromImage(_levelBitmap);
            g.Clear(Color.Transparent);

            for (int i = 0; i < _level.GetLevelData().GetLength(0) ; i++)
            {
                for (int j = 0; j < _level.GetLevelData().GetLength(1); j++)
                {
                    int spriteIndex = _level.GetSpriteIndex(j, i);
                    if (spriteIndex != -1)
                    {
                        Bitmap sprite = _levelSprite[spriteIndex];
                        int x = j * GameSetup.TILE_SIZE - _xLvlOffset;
                        int y = i * GameSetup.TILE_SIZE - _yLvlOffset;
                        int width = GameSetup.TILE_SIZE;
                        int height = GameSetup.TILE_SIZE;

                        g.DrawImage(sprite, x, y, width, height);
                    }
                }
            }
        }



        public void Draw(Graphics g, int _xLvlOffset, int _yLvlOffset)
        {
            if (_levelBitmap == null)
                CreateLevelBitmap();

            DrawLevelBitmap(_xLvlOffset, _yLvlOffset);
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
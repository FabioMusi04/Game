using GameProject.Services.Game;
using GameProject.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GameProject.Services.Levels
{
    public class LevelManager
    {
        private readonly GameSetup _game;
        private List<Bitmap> _levelSprite;

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
            this._levelSprite = new List<Bitmap>(570);
            Bitmap img = LoadSave.GetSpriteAtlas(LoadSave.LEVEL_ATLAS);
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    this._levelSprite.Add(img.Clone(new Rectangle(j * 16, i * 16, 16, 16), img.PixelFormat));
                }
            }
            img.Dispose();
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < GameSetup.TILES_IN_HEIGHT; i++)
            {
                for (int j = 0; j < GameSetup.TILES_IN_WIDTH; j++)
                {
                    int spriteIndex =  this._level.GetSpriteIndex(j, i);
                    if (spriteIndex != -1)
                        g.DrawImage(_levelSprite[spriteIndex], j * GameSetup.TILE_SIZE, i * GameSetup.TILE_SIZE);
                }
            }
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
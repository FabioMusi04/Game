using GameProject.Services.Game;
using GameProject.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameProject.Services.Levels
{
    public class LevelManager
    {
        private readonly GameSetup _game;
        private Bitmap[] _levelSprite;

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
            this._levelSprite = new Bitmap[16];
            Bitmap img = LoadSave.GetSpriteAtlas(LoadSave.LEVEL_ATLAS);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int index = i * 4 + j;
                    this._levelSprite[index] = img.Clone(new Rectangle(j * 32, i * 48, 32, 48), img.PixelFormat);
                }
            }
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < GameSetup.TILES_IN_HEIGHT; i++)
            {
                for (int j = 0; j < GameSetup.TILES_IN_WIDTH; j++)
                {
                    int spriteIndex = _level.GetSpriteIndex(j, i);
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
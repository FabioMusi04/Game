using System.Drawing;
using System.Windows.Forms;
using GameProject.Services.Entities;
using GameProject.Services.Levels;
using GameProject.Services.Game;
using GameProject.Utils;
using System;

namespace GameProject.Services.States
{
  public class Playing : State, StateMethods
  {
    private Player _player;
    private LevelManager _levelManager;

    private int _xLvlOffset, _yLvlOffset;
    private int _leftBorder = (int) (0.2 * GameSetup.GAME_WIDTH);
    private int _rightBorder = (int) (0.8 * GameSetup.GAME_WIDTH);
    private int _topBorder = (int) (0.2 * GameSetup.GAME_HEIGHT);
    private int _bottomBorder = (int) (0.8 * GameSetup.GAME_HEIGHT);
    private static int _lvlTilesWidth = LoadSave.GetLevelData().GetLength(0);
    private static int _lvlTilesHeight = LoadSave.GetLevelData().GetLength(1);
    private static int _maxTilesOffsetX = _lvlTilesWidth - GameSetup.TILES_IN_WIDTH;
    private static int _maxTilesOffsetY = _lvlTilesHeight - GameSetup.TILES_IN_HEIGHT;
    private static int _maxLvlOffsetX = _maxTilesOffsetX * GameSetup.TILE_SIZE;
    private static int _maxLvlOffsetY = _maxTilesOffsetY * GameSetup.TILE_SIZE;
    public Playing(GameSetup game) : base(game)
    {
      InitClasses();
    }

    private void InitClasses()
    {
      this._levelManager = new LevelManager(this._game);
      this._player = new Player(200, 200, (int)(48 * GameSetup.SCALE), (int)(48 * GameSetup.SCALE));
      this._player.LoadLevelData(this._levelManager.GetCurrentLevelData().GetLevelData());
    }
    public Player GetPlayer()
    {
      return this._player;
    }

    public void Update()
    {
      this._levelManager.Update();
      this._player.Update();
      CheckCloseToBorder();
    }

    public void Draw(Graphics g)
    {
      this._levelManager.Draw(g, _xLvlOffset, _yLvlOffset);
      this._player.Render(g, _xLvlOffset, _yLvlOffset);
    }

    public void MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        this._player.Attack = true;
      }
    }

    public void MouseMove(object sender, MouseEventArgs e)
    {
      throw new System.NotImplementedException();
    }

    public void MousePress(object sender, MouseEventArgs e)
    {
      throw new System.NotImplementedException();
    }

    public void MouseRelease(object sender, MouseEventArgs e)
    {
      throw new System.NotImplementedException();
    }

    public void KeyPress(object sender, KeyPressEventArgs e)
    {
      switch (char.ToLower(e.KeyChar))
      {
        case 'w':
          this._player.Up = true;
          break;
        case 'a':
          this._player.Left = true;
          break;
        case 's':
          this._player.Down = true;
          break;
        case 'd':
          this._player.Right = true;
          break;
        case (char)8:
          GameStateManager.GameState = GameState.PAUSED;
          break;
        case (char)27:
          GameStateManager.GameState = GameState.PAUSED;
          break;
      }
    }

    public void KeyUp(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.W:
          this._player.Up = false;
          break;
        case Keys.A:
          this._player.Left = false;
          break;
        case Keys.S:
          this._player.Down = false;
          break;
        case Keys.D:
          this._player.Right = false;
          break;
      }
    }

    private void CheckCloseToBorder(){
      int playerX = (int)this._player.GetBounds().X;
      int playerY = (int)this._player.GetBounds().Y;

      int diffX = playerX - _xLvlOffset;
      int diffY = playerY - _yLvlOffset;

      if (diffX > _rightBorder)
      {
        _xLvlOffset += diffX - _rightBorder;
      }
      else if (diffX < _leftBorder)
      {
        _xLvlOffset -= _leftBorder - diffX;
      }
      if (diffY > _bottomBorder)
      {
        _yLvlOffset += diffY - _bottomBorder;
      }
      else if (diffY < _topBorder)
      {
        _yLvlOffset -= _topBorder - diffY;
      }

      if (_xLvlOffset < 0)
      {
        _xLvlOffset = 0;
      }
      else if (_xLvlOffset > _maxLvlOffsetX)
      {
        _xLvlOffset = _maxLvlOffsetX;
      }
    }
  }
}
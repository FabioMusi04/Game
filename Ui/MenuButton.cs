using System.Collections.Generic;
using System.Drawing;
using GameProject.Services.States;
using GameProject.Utils;
using System.Threading.Tasks;
using System;

namespace GameProject.Ui
{
  public class MenuButton
  {
    private int _x, _y, _index;
    private bool _mouseOver, _mousePress;
    private int xOffsetCenter = Constants.UI.Button.BUTTON_WIDTH / 2;
    private GameState _state;
    private List<Bitmap> _sprites;
    public bool MouseOver { get => _mouseOver; set => _mouseOver = value; }
    public bool MousePress { get => _mousePress; set => _mousePress = value; }
    private Rectangle _bounds;

    public MenuButton(int x, int y, GameState state)
    {
      _x = x;
      _y = y;
      _state = state;
      LoadImgs();
      InitBounds();
    }

    private void LoadImgs()
    {
      _sprites = new List<Bitmap>();

      using Bitmap img = LoadSave.GetSpriteAtlas(LoadSave.MENU_BUTTONS);
      for (int i = 0; i < Constants.UI.Button.BUTTON_SPRITES_DEFAULT_ROW; i++)
      {
        for (int j = 0; j < Constants.UI.Button.BUTTON_SPRITES_DEFAULT_COLUMN; j++)
        {
          _sprites.Add(img.Clone(new Rectangle(j * Constants.UI.Button.BUTTON_WIDTH_DEFAULT, i * Constants.UI.Button.BUTTON_HEIGHT_DEFAULT, Constants.UI.Button.BUTTON_WIDTH_DEFAULT, Constants.UI.Button.BUTTON_HEIGHT_DEFAULT), img.PixelFormat));
        }
      }
    }

    private void InitBounds()
    {
      _bounds = new Rectangle(_x - xOffsetCenter, _y, Constants.UI.Button.BUTTON_WIDTH, Constants.UI.Button.BUTTON_HEIGHT);
    }

    public void Draw(Graphics g)
    {
      g.DrawImage(_sprites[_index], _x - xOffsetCenter, _y, Constants.UI.Button.BUTTON_WIDTH, Constants.UI.Button.BUTTON_HEIGHT);
    }

    public Rectangle GetBounds()
    {
      return _bounds;
    }

    public void Update()
    {
      _index = 24;
      if (MouseOver)
      {
        _index = 25;
      }
      else if (MousePress)
      {
        _index = 26;
      }
    }

    public void ApplyGameState()
    {
      GameStateManager.GameState = _state;
    }
    public void Reset()
    {
      _mouseOver = false;
      _mousePress = false;
    }
  }
}
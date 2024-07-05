using System;
using System.Drawing;
using System.Windows.Forms;
using GameProject.Services.Game;
using GameProject.Ui;

namespace GameProject.Services.States
{
  public class Menu : State, StateMethods
  {
    private MenuButton[] _playButtons = new MenuButton[3];
    public Menu(GameSetup game) : base(game)
    {
      LoadButtons();
    }
    private void LoadButtons()
    {
      _playButtons[0] = new MenuButton(GameSetup.GAME_WIDTH / 2, (int)(150 * GameSetup.SCALE), GameState.PLAYING);
      _playButtons[1] = new MenuButton(GameSetup.GAME_WIDTH / 2, (int)(220 * GameSetup.SCALE), GameState.PAUSED);
      _playButtons[2] = new MenuButton(GameSetup.GAME_WIDTH / 2, (int)(290 * GameSetup.SCALE), GameState.QUIT);
    }
    public void Update()
    {
      foreach (MenuButton button in _playButtons)
      {
        button.Update();
      }
    }

    public void Draw(Graphics g)
    {
      foreach (MenuButton button in _playButtons)
      {
        button.Draw(g);
      }
    }

    public void MouseClick(object sender, MouseEventArgs e)
    {
      // Handle mouse click event here
    }

    public void MouseMove(object sender, MouseEventArgs e)
    {
      foreach (MenuButton button in _playButtons)
      {
        if (IsIn(e, button))
        {
          button.MouseOver = true;
        }
        else
        {
          button.MouseOver = false;
        }
      }
    }

    public void MousePress(object sender, MouseEventArgs e)
    {
      foreach (MenuButton button in _playButtons)
      {
        if (IsIn(e, button))
        {
          button.MousePress = true;
          break;
        }
      }
    }

    public void MouseRelease(object sender, MouseEventArgs e)
    {
      foreach (MenuButton button in _playButtons)
      {
        if (IsIn(e, button))
        {
          if (button.MousePress) {
            button.ApplyGameState();
          }
        }
      }
      ResetButtons();
    }

    private void ResetButtons()
    {
      foreach (MenuButton button in _playButtons)
      {
        button.Reset();
      }
    }

    public void KeyPress(object sender, KeyPressEventArgs e)
    {
       if (e.KeyChar == (char)13)
       {
          GameStateManager.GameState = GameState.PLAYING;
       }
    }

    public void KeyUp(object sender, KeyEventArgs e)
    {
      // Handle key up event here
    }
  }
}